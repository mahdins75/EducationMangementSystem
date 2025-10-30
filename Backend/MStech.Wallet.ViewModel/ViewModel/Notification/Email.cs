using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;

namespace Mstech.ViewModel.DTO;
public class EmailViewModel : BaseEntity<int>
{
    public string EmailTitle { get; set; }
    public string EmailMessage { get; set; }
    public string Destination { get; set; }
}

