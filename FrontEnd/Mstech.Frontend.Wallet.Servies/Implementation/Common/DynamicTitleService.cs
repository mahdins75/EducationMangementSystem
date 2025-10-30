using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Mstech.Frontend.Wallet.Service.Implementation;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using System.Collections.Generic;
using System.Reflection;

namespace Mstech.Frontend.Wallet.Service.Interface.Common
{
    public class DynamicTitleService : IDynamicTitleService
    {
        private Dictionary<string, string> pageTitles = new Dictionary<string, string>
    {
        { "User", "کاربران" },
        { "Dashboard", "داشبورد" },
        { "WalletClient", "پروژه های زیر مجموعه" },
        { "Role", "سطح دسترسی" },
        { "Wallet", "کیف پول" },
        { "AllWallets", "کیف پول های سامانه" },
        { "WalletTransactions", "تراکنش های کیف پول" },
        { "TransactionRequest", "درخواست خروج مبلغ از سیستم " },
    };

        public string PageTitle { get; private set; } = "Default Title";

        public async Task<string> GetPageTitle(string Title)
        {
            if (pageTitles.ContainsKey(Title))
            {
                PageTitle = pageTitles[Title];
            }
            else
            {
                PageTitle = "Default Title";
            }
            return PageTitle;
        }
    }
}
