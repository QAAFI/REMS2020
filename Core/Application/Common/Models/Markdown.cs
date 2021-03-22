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

        private StringBuilder validater = new StringBuilder();

        public void Clear() => builder.Clear();               

        /// <summary>
        /// Begins tracking an item in the validater
        /// </summary>
        public bool ValidateItem<T>(T value, string name)
        {
            bool valid = !value?.Equals(default(T)) ?? false;
            
            if (!valid)
                validater.AppendLine("* Could not find value for " + name);

            return valid;
        }

        /// <summary>
        /// Commits all items tracked in the validater to the markdown with the specified heading
        /// </summary>
        public void CommitValidation(string heading, bool commit)
        {
            if (commit)
            {
                builder.AppendLine("#### \t**" + heading + "**");
                builder.AppendLine(validater.ToString());
            }

            validater.Clear();
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
