using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

using Rems.Application.Treatments.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        private async Task<Operations> BuildOperations(int treatmentId)
        {
            var model = new Operations()
            {
                Operation = new List<Operation>()
            };

            var irrigations = await _mediator.Send(new IrrigationsQuery() { TreatmentId = treatmentId });
            var iOperations = irrigations.Select(i => new Operation()
                {
                    Action = $"[Irrigation].Apply({i.Amount});",
                    Date = i.Date.ToString("yyyy-MM-dd")
                });

            var fertilizations = await _mediator.Send(new FertilizationsQuery() { TreatmentId = treatmentId });
            var fOperations = fertilizations.Select(f => new Operation()
                {
                    Action = $"[Fertiliser].Apply({f.Amount}, Fertiliser.Types.Urea, {f.Depth});",
                    Date = ((DateTime)f.Date).ToString("yyyy-MM-dd")
                });

            model.Operation?.AddRange(iOperations);
            model.Operation?.AddRange(fOperations);

            return model;
        }
    }
}
