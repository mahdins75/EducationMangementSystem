using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class NavItemViewModel
{
    public string Text { get; set; }
    public string IconClass { get; set; }
    public string Href { get; set; }
    public List<NavItemViewModel> SubItems { get; set; } = new();
}
