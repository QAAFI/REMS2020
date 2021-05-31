using System;

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
    }
}
