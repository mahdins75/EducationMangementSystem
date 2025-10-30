using MStech.Accounting.DataBase.Enums;

namespace Mstech.ViewModel.DTO
{
    public class DiscountCodeBankViewModel
    {
        public int Id { get; set; }

        public string DiscountCodeText { get; set; }

        public string? Title { get; set; }

        public string? ItemId { get; set; }

        public decimal DiscountAmount { get; set; }

        public string ClientIdForApi { get; set; }

        public DiscountCodeBankSpendType? DiscountCodeBankSpendType { get; set; }
        public string? DiscountCodeBankSpendTypeDisplayName { get; set; }

        public string? OwnerId { get; set; }

        public string? UnitId { get; set; }

        public string? OwnerUserName { get; set; }

        public DateTime ExpireDate { get; set; }

        public string? PersianExpireDate { get; set; }

        public UserViewModel Owner { get; set; }


        //// pagination paremeters 
        ///

        public bool IsPagination { get; set; } = false;
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }

    }
}
