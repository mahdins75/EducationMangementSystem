using Entity.Base;
using Microsoft.Identity.Client;
using Mstech.Entity.Etity;
using MStech.Accounting.DataBase.Enums;

namespace Mstech.ViewModel.DTO;
public class WalletClientViewModel : BaseEntity<int>
{
    public string Title { get; set; }
    public ClientStatus ActivationStatus { get; set; }
    public string? ActivationStatusTitle { get; set; }
    public string OwnerId { get; set; }
    public string OwnerFullName { get; set; }
    public string? OwnerUserName{ get; set; }
    public int WalletsCount { get; set; }
    public string ClientIdForApi { get; set; }
    public string? UserName { get; set; }
    public string BaseUrl { get; set; }
    /// <summary>
    /// pagination parameters
    /// </summary>
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    public ICollection<WalletViewModel> Wallets { get; set; }
    public User Owner { get; set; }


    ////search parameters
    ///
    public string? UserId { get; set; }
}

