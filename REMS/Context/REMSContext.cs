using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace REMS.Context
{
    using Entities;

    public partial class REMSContext : DbContext
    {
        public string ConnectionString { get; }

        public Queries Query;

        public List<IEntity> Entities = new List<IEntity>()
        {
            new ChemicalApplication(),
            new Crop(),
            new Design(),
            new ExperimentInfo(),
            new Experiment(),
            new Factor(),
            new FertilizationInfo(),
            new Fertilization(),
            new Fertilizer(),
            new Field(),
            new Harvest(),
            new IrrigationInfo(),
            new Irrigation(),
            new Level(),
            new MetData(),
            new MetInfo(),
            new MetStation(),
            new Method(),
            new PlotData(),
            new Plot(),
            new Region(),
            new ResearcherList(),
            new Researcher(),
            new Site(),
            new SoilData(),
            new SoilLayerData(),
            new SoilLayerTrait(),
            new SoilLayer(),
            new SoilTrait(),
            new Soil(),
            new Stat(),
            new TillageInfo(),
            new Tillage(),
            new Trait(),
            new Treatment(),
            new Unit()
        };

        public REMSContext(string file) : base()
        {
            ConnectionString = $"Data Source={file};";
            Query = new Queries(this);
        }

        public REMSContext(string file, DbContextOptions<REMSContext> options) : base(options)
        {
            ConnectionString = $"Data Source={file};";
            Query = new Queries(this);
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging(true);
                optionsBuilder.EnableDetailedErrors(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IEntity entity in Entities) entity.BuildModel(modelBuilder);
        }


    }
}
