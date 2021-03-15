using Rems.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common
{
    public class Markdown
    {
        public string Text => builder.ToString();

        private StringBuilder builder = new StringBuilder();

        public void AddHeading(string heading)
            => builder.AppendLine("# " + heading);

        public void AddSubHeading(string subheading)
            => builder.AppendLine("## " + subheading);

        public void AddLine(string line)
            => builder.AppendLine(line);

        public void AddList(IEnumerable<string> items)
            => items.ForEach(i => builder.AppendLine("* " + i));
    }
}
