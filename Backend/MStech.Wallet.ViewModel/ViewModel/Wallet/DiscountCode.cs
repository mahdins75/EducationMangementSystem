namespace Mstech.ViewModel.DTO;
public class DiscountCodeViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }

    public string? UserFullName { get; set; }

    public string? DiscountCodeText { get; set; }

    public int WalletId { get; set; }
    public string WalletTitle { get; set; }
    public int DiscountCodeBankId { get; set; }
    public UserViewModel User { get; set; }

    //// pagination paremeters 
    ///

    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    /////
    ///
    public string UserName { get; set; }
}
