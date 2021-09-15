using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class LookupTable
    {
        readonly SortedList<double, double> Values = new();

        public LookupTable(double[] keys, double[] values)
        {
            if (keys.Length != values.Length)
                throw new Exception("Keys and values must have the same length");

            for (int i = 0; i < keys.Length; i++)
                Values.Add(keys[i], values[i]);
        }

        public double LookUp(double y)
        {
            if (Values.TryGetValue(y, out double existing))
                return existing;

            int index = Values.Keys.ToList().BinarySearch(y);

            if (index == 0 || index == ~Values.Keys.Count)
                throw new Exception("Value outside the range of the table.");

            if (index < 0)
                index = ~index;

            var y1 = Values.Keys[index - 1];
            var y2 = Values.Keys[index];
            var x1 = Values[y1];
            var x2 = Values[y2];

            var run = x2 - x1;
            if (run == 0.0)
                return x1;

            var m = (y2 - y1) / run;
            var c = y2 - (m * x2);
            var x = (y - c) / m;
            return x;
        }
    }
}
