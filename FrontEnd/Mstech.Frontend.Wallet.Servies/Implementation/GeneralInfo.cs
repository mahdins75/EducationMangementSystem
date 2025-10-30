using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.Frontend.Wallet.Service.Implementation
{
    public class GeneralInfo
    {
        //private readonly string ApiBaseAddress = "http://localhost:5145";
        private readonly string ApiBaseAddress = "https://localhost:7081";
        //private readonly string ApiBaseAddress = "https://test102.mitalearn.com";
        //private readonly string ApiBaseAddress = "https://wallet.trabase.ir";
        public string GetApiBaseAddress()
        {
            return ApiBaseAddress;
        }
    }
}
