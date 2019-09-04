using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        public string Connection { get; }

        public static List<string> TableNames
        {
            get
            {
                return typeof(ADAMSContext)                    
                    .GetProperties()                    
                    .Where(p => Attribute.IsDefined(p, typeof(Table)))
                    .Select(p => p.Name)
                    .ToList();
            }
        }

        public ADAMSContext(string connection)
        {
            Connection = connection;
        }

        public ADAMSContext(DbContextOptions<ADAMSContext> options)
            : base(options)
        {

        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            BuildChemicalApplication(modelBuilder);
            BuildCrop(modelBuilder);
            BuildDesign(modelBuilder);
            BuildExperiment(modelBuilder);
            BuildExperimentInfo(modelBuilder);
            BuildFactor(modelBuilder);
            BuildFertilization(modelBuilder);
            BuildFertilizer(modelBuilder);
            BuildFertilizerInfo(modelBuilder);
            BuildField(modelBuilder);
            BuildHarvest(modelBuilder);
            BuildIrrigation(modelBuilder);
            BuildIrrigationInfo(modelBuilder);
            BuildLevel(modelBuilder);
            BuildMetData(modelBuilder);
            BuildMetInfo(modelBuilder);
            BuildMetStation(modelBuilder);
            BuildMethod(modelBuilder);
            BuildPlot(modelBuilder);
            BuildPlotData(modelBuilder);
            BuildRegion(modelBuilder);
            BuildResearcher(modelBuilder);
            BuildResearcherList(modelBuilder);
            BuildSite(modelBuilder);
            BuildSoil(modelBuilder);
            BuildSoilData(modelBuilder);
            BuildSoilLayer(modelBuilder);
            BuildSoilLayerData(modelBuilder);
            BuildSoilLayerTrait(modelBuilder);
            BuildSoilTrait(modelBuilder);
            BuildStats(modelBuilder);
            BuildTillage(modelBuilder);
            BuildTillageInfo(modelBuilder);
            BuildTrait(modelBuilder);
            BuildTreatment(modelBuilder);
            BuildUnit(modelBuilder);            
        }

        public void ImportDataSet(DataSet data)
        {
            var tables = typeof(ADAMSContext)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(Table)));            

            foreach (DataTable table in data.Tables)
            {
                var setInfo = tables
                    .FirstOrDefault(p => 
                    (p.GetCustomAttribute(typeof(Table)) as Table).Name == table.TableName);

                //TODO: Alert the user if no table is found
                if (setInfo == null) continue;                

                AddTable(table, setInfo);
            }

            SaveChanges();
        }

        private void AddTable(DataTable table, PropertyInfo info)
        {
            // Find the column properties
            var relation = (info.GetCustomAttribute(typeof(Table)) as Table).Relation;
            var columns = relation.GetProperties().Where(p => p.IsDefined(typeof(Column)));            

            // Intersect the names of the columns in the table and the relation
            var relationNames = columns.Select(p => p.Name).ToArray();
            var tableNames = table.Columns.OfType<DataColumn>().Select(c => c.ColumnName);
            var intersect = relationNames.Intersect(tableNames).ToArray();

            // Create a subtable based on the intersection
            var subtable = table.DefaultView.ToTable("", true, intersect);            

            // Find the DbSet to add the data to
            dynamic set = info.GetValue(this);

            // Create a blank entity to store data in
            dynamic entity = Activator.CreateInstance(relation);
            
            // Iterate over the rows
            foreach (DataRow row in subtable.Rows)
            {   
                foreach(DataColumn column in subtable.Columns)
                {
                    var property = columns.FirstOrDefault(p => p.Name == column.ColumnName);
                    if (property == null) continue;

                    Type type = row[column].GetType();
                    if (type != property.PropertyType)
                    {
                        int value = (int) (double)row[column];
                        property.SetValue(entity, value);
                    }                    
                    else
                    {
                        property.SetValue(entity, row[column]);
                    }
                    
                }

                set.Add(entity);
            }
            
        }
    }
}
