using Microsoft.AspNetCore.Identity;
using MStech.Wallet.DataBase.Etity.Client;
using MStech.Wallet.DataBase.Etity.Wallet;

namespace Mstech.Entity.Etity
{
    public class User : IdentityUser
    {
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsActiveOutsideWallet { get; set; }
        public bool IsDeleted { get; set; }
        public int? PositionId { get; set; }
        public bool IsHRMember { get; set; }
        public string? CurrentConfirmationCode { get; set; }
        public DateTime? ConfirmationCodeExpireDate { get; set; }
        public Constant? Position { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
        public ICollection<WalletClient> Clients { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
        
        public ICollection<ReferralCode> ReferralCodes { get; set; }
        public ICollection<DiscountCode> DiscountCodes { get; set; }
        public ICollection<DiscountCodeBank> DiscountCodeBanks { get; set; }
    }
}