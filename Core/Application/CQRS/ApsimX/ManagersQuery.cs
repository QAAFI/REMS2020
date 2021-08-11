using Models;
using Models.Core;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Experiment = Rems.Domain.Entities.Experiment;
using System.Text.RegularExpressions;

namespace Rems.Application.CQRS
{
    public class ManagersQuery : ContextQuery<Folder>
    {
        private static IFileManager _manager;

        public int ExperimentId { get; set; }

        public class Handler : BaseHandler<ManagersQuery>
        { 
            public Handler(IRemsDbContextFactory factory, IFileManager manager) : base(factory)
            {
                _manager = manager;
            }
        }

        protected override Folder Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            var managers = new List<IModel>
            {
                GetSowing(exp),
                new Manager { Name = "Harvesting", Code = _manager.GetContent("Harvest") }
            };

            // Setup designs regex to check for specific factors
            var options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            var factors = exp.Treatments.SelectMany(t => t.Designs.Select(d => d.Level.Factor.Name)).Distinct();         
            
            // Check if fertilisation is a factor
            if (factors.Any(factor => Regex.IsMatch(factor, @"n\s*rates", options)))
                managers.Add(new Manager { Name = "Fertilisation", Code = _manager.GetContent("Fertilisation") });
            
            return new Folder { Name = "Managers", Children = managers };
        }

        private Manager GetSowing(Experiment exp)
        {
            var sowing = new Manager { Name = "Sowing", Code = _manager.GetContent("Sowing") };

            sowing.Parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Date", exp.Sowing?.Date.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("Density", exp.Sowing?.Population.ToString()),
                new KeyValuePair<string, string>("Depth", exp.Sowing?.Depth.ToString()),
                new KeyValuePair<string, string>("Cultivar", exp.Sowing?.Cultivar?.Replace('/', 'x')),
                new KeyValuePair<string, string>("RowSpacing", exp.Sowing?.RowSpace.ToString())
            };
            var date = exp.Sowing?.Date;
            return sowing;
        }
    }
}