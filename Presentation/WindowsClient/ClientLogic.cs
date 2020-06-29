using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Application.DB.Commands;
using Rems.Application.DB.Queries;
using Rems.Application.Entities.Commands;
using Rems.Application.Tables.Queries;
using Rems.Infrastructure;
using Rems.Infrastructure.ApsimX;
using Rems.Infrastructure.Excel;

using Microsoft.Extensions.DependencyInjection;

namespace WindowsClient
{
    public class ClientLogic
    {
        private IRemsDbContext context;

        private readonly IMediator mediator;

        private readonly Settings settings = Settings.Instance;

        public event EventHandler ListViewOutdated;

        public ClientLogic(IServiceProvider provider)
        {
            mediator = provider.GetRequiredService<IMediator>();
        }

        private void LoadSettings()
        {
            settings.Load();

            // If the settings couldn't be loaded
            if (!settings.Loaded)
            {
                // Track the tables
                var tables = Settings.Instance["TABLES"];
                foreach (var table in context.Names)
                {
                    tables.AddMapping(table);
                }

                // Track the traits
                var traits = Settings.Instance["TRAITS"];

                // TEMPORARY MAPPINGS

                // Unknown
                traits.AddMapping("CN2Bare", "cn2_bare");
                traits.AddMapping("CNCov", "cn_cov");
                traits.AddMapping("CNRed", "cn_red");
                traits.AddMapping("SummerCona", "cona");
                traits.AddMapping("DiffusConst", "diffus_const");
                traits.AddMapping("DiffusSlope", "diffus_slope");
                //traits.AddMapping("", "enr_a_coeff");
                //traits.AddMapping("", "enr_b_coeff");
                traits.AddMapping("MaxT", "maxt");
                traits.AddMapping("MinT", "mint");
                //traits.AddMapping("", "nh4ppm");
                //traits.AddMapping("", "no3ppm");
                traits.AddMapping("OC", "oc");
                traits.AddMapping("Radn", "radn");
                traits.AddMapping("Rain", "RAIN");
                //traits.AddMapping("", "root_cn");
                //traits.AddMapping("", "root_wt");
                traits.AddMapping("Salb", "salb");               
                traits.AddMapping("SW", "sw");                
                //traits.AddMapping("", "u");
                //traits.AddMapping("", "ureappm");              
                
                // Chemical
                traits.AddMapping("NO3N", "NO3N");
                traits.AddMapping("NH4N", "NH4N");
                traits.AddMapping("PH", "PH");

                // Organic
                traits.AddMapping("Carbon", "Carbon");
                traits.AddMapping("SoilCNRatio", "soil_cn");
                traits.AddMapping("FBiom", "fbiom");
                traits.AddMapping("FInert", "finert");
                traits.AddMapping("FOM", "FOM");

                // Physical
                traits.AddMapping("BD", "BD");
                traits.AddMapping("AirDry", "air_dry");
                traits.AddMapping("LL15", "ll15");
                traits.AddMapping("DUL", "dul");
                traits.AddMapping("SAT", "sat");
                traits.AddMapping("KS", "KS");

                // SoilCrop                
                traits.AddMapping("LL", "ll");
                traits.AddMapping("KL", "kl");
                traits.AddMapping("XF", "xf");

                // SoilWater
                traits.AddMapping("SWCON", "swcon");
                traits.AddMapping("KLAT", "KLAT");


                // Track the entities
                foreach (var map in context.Mappings)
                {
                    settings.TrackProperty(map);
                }

                settings.Loaded = true;
            }
        }

        /// <summary>
        /// Populates the listview with the tables in the database
        /// </summary>
        public async Task<object[]> GetListItems()
        {
            return (await mediator.Send(new GetTableListQuery())).ToArray();
        }

        public async Task<DataTable> TryGetDataTable(string table)
        {
            try
            {           
                return await mediator.Send(new GetDataTableQuery() { TableName = table });
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return null;
            }
        }

        public Control[] GetProperties(string table)
        {
            var controls = new List<Control>();
            if (!settings.IsMapped(table)) return controls.ToArray();

            int count = 0;
            foreach (var item in Settings.Instance[table].Reverse())
            {
                var property = new PropertyControl()
                {
                    Table = table,
                    Property = item.Key,
                    Value = item.Value,
                    Dock = DockStyle.Top
                };

                property.PropertyChanged += PropertyChangedEvent;
                controls.Add(property);

                count++;
            }

            return controls.ToArray();
        }

        public void PropertyChangedEvent(object sender, PropertyChangedEventArgs args)
        {
            if (Settings.Instance[args.TableName][args.PropertyName] == args.NewValue) return;

            try
            {
                Settings.Instance[args.TableName][args.PropertyName] = args.NewValue;
            }
            catch
            {
                ErrorMessage("That value is currently mapped to another property.", "Invalid name.");
                ((PropertyControl)sender).Value = Settings.Instance[args.TableName][args.PropertyName];
            }
        }

        public async Task<bool> TryCreateDatabase(string file)
        {
            try
            {
                context = await mediator.Send(new CreateDBCommand() { FileName = file });
                LoadSettings();
                ListViewOutdated?.Invoke(null, EventArgs.Empty);
                return true;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return false;
            }
        }

        public async Task<bool> TryOpenDatabase(string file)
        {
            try
            {
                context = await mediator.Send(new OpenDBCommand() { FileName = file });
                LoadSettings();
                ListViewOutdated?.Invoke(null, EventArgs.Empty);
                return true;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return false;
            }
        }

        public async Task<bool> TrySaveDatabase()
        {
            try
            {
                Settings.Instance.Save();
                await mediator.Send(new SaveDBCommand());
                return true;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return false;
            }
        }

        public async Task<bool> TryCloseDatabase()
        {
            try
            {
                await mediator.Send(new CloseDBCommand());
                return true;
            }
            catch
            {
                ErrorMessage("The database did not close correctly.");
                return false;
            }
        }

        public async Task<bool> TryDataImport(string path)
        {
            try
            {
                await mediator.Send(new BulkInsertCommand() { Data = ExcelImporter.ReadRawData(path), TableMap = Settings.Instance["TABLES"] });

                ListViewOutdated?.Invoke(null, EventArgs.Empty);

                MessageBox.Show($"Import Complete.\n");
                return true;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return false;
            }
        }

        public async Task<bool> TryDataExport(string file)
        {
            try
            {
                IApsimX apsim = new ApsimX(mediator);
                await apsim.CreateApsimModel(Path.GetDirectoryName(file));
                apsim.SaveApsimFile(file);

                MessageBox.Show($"Export Complete.");
                return true;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return false;
            }
        }

        public async Task<string[]> TryGetTraitNamesById(string table)
        {
            try
            {
                return await mediator.Send(new GetTraitNamesByIdQuery() { TraitIds = table });
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return new string[0];
            }
        }

        public async Task<string[]> TryGetGraphableItems(string table)
        {
            try
            {
                return await mediator.Send(new GetGraphableItemsQuery() { TableName = table });
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return new string[0];
            }
        }

        public async Task<IEnumerable<Tuple<object, object>>> TryGetGraphData(string[] names)
        {
            // TODO: using a string array because RemsClient.ProcessAction is currently limited
            // to methods with single parameters, need to fix

            try
            {
                var query = new GetGraphDataQuery()
                {
                    TableName = names[0],
                    TraitName = names[1],
                    XColumn = names[2],
                    YColumn = names[3]
                };

                return await mediator.Send(query);
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
                return null;
            }
        }

        private void ErrorMessage(string message, string caption = "Oops! Something went wrong.")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
