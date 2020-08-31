using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MediatR;

using Rems.Application.Met.Queries;

using Rems.Infrastructure;

namespace Rems.Infrastructure.Met
{
    public static class MetWriter
    {
        public static async void GenerateMetFile(IMediator mediator, int id)
        {           
            var met = await mediator.Send(new MetStationQuery() { ExperimentId = id });

            string file = met + ".met";

            if (File.Exists(file)) return;
            
            using (var stream = new FileStream(file, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                var builder = await mediator.Send(new MetFileDataQuery() { ExperimentId = id });
                writer.Write(builder.ToString());
                writer.Close();
            }
        }

    }
}
