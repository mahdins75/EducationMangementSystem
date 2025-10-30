using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.Wallet.Components
{
    
     public static class DecimalExtensions
    {
        public static string ToNoDecimalString(this decimal value)
        {
            return Math.Round(value, 0).ToString("F0");
        }
    }
}
