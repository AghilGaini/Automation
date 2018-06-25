using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class MethodExtension
    {
        public static long ToLong(this object s)
        {
            try
            {
                return Convert.ToInt64(s);
            }
            catch 
            {
                return 0;
            }
        }
    }
}
