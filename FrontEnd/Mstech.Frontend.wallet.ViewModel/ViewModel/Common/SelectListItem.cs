using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;
public class SelectListItem
{
    public int Value { get; set; }
    public string Title { get; set; }
}
public class statusDropDownDataListItem
{
    public int Value { get; set; }
    public string Title { get; set; }
}
public class SelectListItemStringValue
{
    public string Value { get; set; }
    public string Title { get; set; }
}