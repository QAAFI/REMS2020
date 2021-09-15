using Rems.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rems.Application.Common
{
    public static class Utilities
    {
        public static int GenerateHash(params int[] values)
        {
            int hash = 11;
            
            unchecked
            {
                foreach (int i in values)
                {
                    int temp = hash * 13 + i;
                    hash = (temp << 16) | (temp >> 16);
                }
            }

            return hash;
        }

        public static IEnumerable<Type> GetFormatTypes(string format)
            => Assembly.Load("Rems.Domain")
                .GetTypes()
                .Where(t => t.HasFormat(format))
                .OrderBy(t => t.DependencyLevel());
    }
}
