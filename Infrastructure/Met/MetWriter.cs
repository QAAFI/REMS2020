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
        public static async void GenerateMetFile(IMediator mediator, int id, string path)
        {           
            var met = await mediator.Send(new MetStationQuery() { Id = id });

            string file = path + "\\" + met.Name + ".met";

            using var stream = new FileStream(file, FileMode.Create);
            using var writer = new StreamWriter(stream);

            writer.Write("[weather.met.weather]\n");
            writer.Write($"!experiment number = 1\n");
            writer.Write($"!experiment = \n");
            writer.Write($"!station name = {met.Name}\n");
            writer.Write($"latitude = {met.Latitude} (DECIMAL DEGREES)\n");
            writer.Write($"longitude = {met.Longitude} (DECIMAL DEGREES)\n");
            writer.Write($"tav = {met.TemperatureAverage} (oC)\n");
            writer.Write($"amp = {met.Amp} (oC)\n\n");

            writer.Write($"{"Year",-7}{"Day",3}{"maxt",8}{"mint",8}{"radn",8}{"Rain",8}\n");
            writer.Write($"{" () ",-7}{" ()",3}{" ()",8}{" ()",8}{" ()",8}{" ()",8}\n");

            var data = await mediator.Send(new MetFileDataQuery() { Id = id, Map = Settings.Instance["TRAITS"] });

            foreach (var row in data)
            {                  
                writer.Write($"{row.Year,-7}" +
                             $"{row.Day,3}" +
                             $"{Math.Round(row.TMax, 2), 8}" +
                             $"{Math.Round(row.TMin, 2), 8}" +
                             $"{Math.Round(row.Radn, 2), 8}" +
                             $"{Math.Round(row.Rain, 2), 8}\n");
            }
            writer.Close();
        }

    }
}
