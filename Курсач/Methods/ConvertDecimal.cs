using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Methods
{
    public static class ConvertDecimal
    {
        public static string RemoveZeroes(decimal number)
        {
            return Convert.ToString(decimal.Round(decimal.Parse(Convert.ToString(number).Replace(".", ",")), 2)).Replace(",", ".") + '$';
        }
    }
}
