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

        public void Clear() => builder.Clear();

        public T ValidateItem<T>(T item, string name)
        {
            if (Equals(item, default(T)))           
                AddLine("* Could not find a value for item " + name);

            return item;
        }

        public void AddHeading(string heading)
            => builder.AppendLine("# " + heading);

        public void AddSubHeading(string subheading, byte depth = 1)
        {
            string hash = "#";
            for (byte i = 0; i < depth; i++)
                hash += '#';
            hash += ' ';

            builder.AppendLine(hash + subheading);
        }

        public void AddLine(string line)
            => builder.AppendLine(line);

        public void AddList(IEnumerable<string> items)
            => items.ForEach(i => builder.AppendLine("* " + i));
    }
}
