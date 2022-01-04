using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjalte.InventorApiExtensions.Common
{
    public static class Guard
    {
        public static void StringArgumentIsNotNullOrEmpty(string stringToTest, string exceptionText)
        {
            if (string.IsNullOrEmpty(stringToTest))
            {
                string msg = string.Format(exceptionText, stringToTest);
                throw new ArgumentException(msg);
            }
        }
    }
}
