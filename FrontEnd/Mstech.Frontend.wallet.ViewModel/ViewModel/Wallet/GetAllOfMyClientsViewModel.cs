using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class GetAllOfMyClientsViewModel
{
    [Display(Name ="شناسه سایت")]
    public string ClientId { get; set; }

    [Display(Name ="عنوان سایت")]
    public string Title { get; set; }
}

