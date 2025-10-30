using DataBase.Repository;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using MStech.Wallet.DataBase.Etity.Wallet;
using Mstech.ViewModel.DTO;
using Implementation.WalletServiceArea;
using Implementation.TransactionServiceArea;
using Implementation.ReferralCodeServiceArea;
using Implementation.WalletClientArea;
using MStech.Wallet.ViewModel.ViewModel.Common;
using Mapster;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Implementation.UserService;

namespace Implementation.DiscountCodeServiceArea
{
    public class DashboardService
    {
        private readonly WalletService walletService;

        private readonly TransactionService transactionService;

        private readonly ReferralCodeService referralCodeService;

        private readonly WalletClientService walletClientService;

        private readonly UserService.UserService userService;
        public DashboardService(WalletService _walletService, TransactionService _transactionService, ReferralCodeService _referralCodeService, WalletClientService _walletClientService, UserService.UserService userService)
        {

            this.walletService = _walletService;

            this.transactionService = _transactionService;

            this.referralCodeService = _referralCodeService;

            this.walletClientService = _walletClientService;

            this.userService = userService;
        }

        public async Task<ResponseViewModel<DashboarAggrigation>> GetAll(string userName)
        {
            var user = await userService.FindByUserNameAsync(userName);

            var result = new ResponseViewModel<DashboarAggrigation>();
            result.Entity = new DashboarAggrigation();


            result.Entity.WalletInfos = new List<WalletInfoViewModel>();
            result.Entity.ClientAccessLinks = new List<ClientAccessLinkViewModel>();
            result.Entity.ReferralCodes = new List<MStech.Wallet.ViewModel.ViewModel.Common.ReferralCodeViewModel>();
            result.Entity.WalletInfos = new List<WalletInfoViewModel>();


            result.Entity.WalletInfos = new List<WalletInfoViewModel>();

            ///WalletInfos
            ///  

            var wallets = await this.walletService.GetAll(new WalletViewModel() { UserId = user.Id });

            result.Entity.WalletInfos.AddRange(wallets.Entity.Select(m => new WalletInfoViewModel()
            {
                ClientName = m.ClientName,
                Balence = m.Balance,
                Id = m.Id

            }).ToList());

            foreach (var item in result.Entity.WalletInfos)
            {
                var temp = await transactionService.GetAll(new TransactionViewModel() { WalletId = item.Id, PageSize = 1, IsPagination = true });

                if (temp.Entity.Any())
                {
                    item.BalanceStatus = temp.Entity.FirstOrDefault().TransactionTypeTitle ?? "-";
                    item.LastTransactionValue = temp.Entity.FirstOrDefault().Amount;
                }
            }

            /// end walletInfoes
            /// 

            ///ClientAccessLinks
            ///
            result.Entity.ClientAccessLinks = new List<ClientAccessLinkViewModel>();

            var clientAccessLinks = await this.walletClientService.GetAll(new WalletClientViewModel() { UserId = user.Id });

            result.Entity.ClientAccessLinks = clientAccessLinks.Entity.Select(m => new ClientAccessLinkViewModel()
            {
                CLientName = m.Title,
                Link = m.BaseUrl
            }).ToList();

            ////
            ///

            ///ClientAccessLinks
            ///

            result.Entity.DashboardWaleltsBalence = new DashboardWaleltsBalenceViewModel();

            var walletsBalence = await this.walletService.GetAll(new WalletViewModel() { UserId = user.Id });

            var totalBalance = 0m;

            foreach (var item in walletsBalence.Entity)
            {
                totalBalance += item.Balance;
            }

            result.Entity.DashboardWaleltsBalence.Balance = totalBalance;

            ////
            ///

            ///ClientAccessLinks
            ///
            result.Entity.ReferralCodes = new List<MStech.Wallet.ViewModel.ViewModel.Common.ReferralCodeViewModel>();

            var referralCodes = await this.referralCodeService.GetAll(new MStech.Wallet.DataBase.Etity.Wallet.ReferralCodeViewModel() { UserId = user.Id });

            result.Entity.ReferralCodes.AddRange(referralCodes.Entity.Select(m => new MStech.Wallet.ViewModel.ViewModel.Common.ReferralCodeViewModel()
            {
                ClientName = m.WalletTitle,
                ReferralCode = m.ReferralCodeText,
                WalletType = m.Wallet.WalletTypeTitle,
            }));

            ////
            ///
            result.IsSuccess = true;
            return result;
        }




    }

}