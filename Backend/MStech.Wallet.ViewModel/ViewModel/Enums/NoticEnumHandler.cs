using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstech.ADV.ViewModel.ViewModel.Enums
{
    public enum NoticEnumHandlerEnum
    {
        [Display(Name = "Email")]
        Email = 1,
        [Display(Name = "SMS")]
        SMS = 2

    }
}
