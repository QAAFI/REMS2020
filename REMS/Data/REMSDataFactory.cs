using System;
using System.Collections.Generic;
using System.Text;

namespace REMS
{
    public static class REMSDataFactory
    {
        public static IREMSDatabase Create()
        {
            return new REMSDatabase();
        }
    }
}
