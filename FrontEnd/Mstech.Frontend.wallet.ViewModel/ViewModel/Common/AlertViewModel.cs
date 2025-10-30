using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.Frontend.Wallet.ViewModel.Common
{
    public class AlertOptions
    {
        public string title { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool showCancelButton { get; set; }
        public string confirmButtonText { get; set; }
        public string cancelButtonText { get; set; }
    }
        public class AlertResult
    {
        public bool isConfirmed { get; set; }
        public bool isDenied { get; set; }
        public bool isDismissed { get; set; }
        public bool value { get; set; }
    }
}
