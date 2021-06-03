using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common.Models
{
    public sealed class ProgressReporter : Progress<int>
    {
        private int current;

        public ProgressReporter(Action<int> handler) : base(handler)
        { }

        public void Increment(int value)
        {
            current += value;
            OnReport(current);
        }

        public void Reset()
        {
            current = 0;
            OnReport(current);
        }
    }
}
