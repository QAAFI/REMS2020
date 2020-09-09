using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Models.Core.ApsimFile;
using Rems.Infrastructure.ApsimX;
using Rems.Infrastructure.Excel;

namespace WindowsClient
{
    public class ClientLogic
    {
        private readonly IMediator mediator;

        public ClientLogic(IServiceProvider provider)
        {
            mediator = provider.GetRequiredService<IMediator>();
        }

        public void TryDataImport(string file)
        {
            var importer = new ExcelImporter(mediator);
            var data = importer.ReadDataSet(file);
            importer.InsertDataSet(data);
        }

        public async Task<bool> TryDataExport(string file)
        {
            try
            {
                IApsimX apsim = new ApsimX(mediator);
                var sims = await apsim.CreateModels();
                File.WriteAllText(file, FileFormat.WriteToString(sims));

                //MessageBox.Show($"Export Complete.");
                return true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<T> TryQueryREMS<T>(IRequest<T> request, string message = null)
        {
            Application.UseWaitCursor = true;
            //Application.DoEvents();

            List<Exception> errors = new List<Exception>();

            try
            {
                var task = mediator.Send(request);                
                
                Application.UseWaitCursor = false;
                return await task;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                errors.Add(error);
            }

            var builder = new StringBuilder();

            foreach (var error in errors)
            {
                builder.AppendLine(error.Message + "at\n" + error.StackTrace + "\n");
            }

            MessageBox.Show(builder.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.UseWaitCursor = false;

            return await Task.Run(() => default(T));
        }

    }
}
