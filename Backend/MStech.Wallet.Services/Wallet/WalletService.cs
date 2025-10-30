using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.Wallet;
using Implementation.CityService;
using MStech.Accounting.DataBase.Enums;
using Implementation.WalletClientArea;
using DNTPersianUtils.Core;

namespace Implementation.WalletServiceArea
{
    public class WalletService : BaseService<Wallet>
    {
        private readonly WalletClientService walletClientService;

        public WalletService(IUnitOfWork _unitOfWork, IRepository<Wallet> _repository,
            WalletClientService walletClientService) : base(_unitOfWork)
        {
            this.walletClientService = walletClientService;
        }

        public async Task<Result<List<WalletViewModel>>> GetAll(WalletViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (model.ClientId > 0)
            {
                query = query.Where(m => m.ClientId == model.ClientId);
            }

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(m => m.UserId == model.UserId);
            }

            if (!string.IsNullOrEmpty(model.ClientOwnerUserName))
            {
                query = query.Where(m => m.Client.Owner.UserName == model.ClientOwnerUserName);
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(m => m.User.UserName == model.UserName);
            }

            if (Enum.IsDefined(typeof(WalletType), model.WalletType))
            {
                query = query.Where(m => m.WalletType == model.WalletType);
            }

            if (model.Client != null && !string.IsNullOrEmpty(model.Client.ClientIdForApi))
            {
                query = query.Where(m => m.Client.ClientIdForApi == model.Client.ClientIdForApi);
            }

            var result = new Result<List<WalletViewModel>>();
            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new WalletViewModel()
                {
                    Id = m.Id,
                    ActivationStatus = m.ActivationStatus,
                    Balance = m.Balance,
                    ActivationStatusTitle = AssistService.GetEnumDescription(m.ActivationStatus),
                    WalletType = m.WalletType,
                    WalletTypeTitle = AssistService.GetEnumDescription(m.WalletType),
                    ParentId = m.ParentId,
                    UserId = m.UserId,
                    ClientId = m.ClientId,
                    ClientName = m.Client != null ? m.Client.Title : null,
                    UserName = m.User != null ? m.User.UserName : "",
                    UserFullName = m.User != null ? m.User.FullName : "",
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
            }
            else
            {
                result.Entity = query.Select(m => new WalletViewModel()
                {
                    Id = m.Id,
                    ActivationStatus = m.ActivationStatus,
                    Balance = m.Balance,
                    ActivationStatusTitle = AssistService.GetEnumDescription(m.ActivationStatus),
                    WalletType = m.WalletType,
                    WalletTypeTitle = AssistService.GetEnumDescription(m.WalletType),
                    ParentId = m.ParentId,
                    UserId = m.UserId,
                    ClientId = m.ClientId,
                    ClientName = m.Client != null ? m.Client.Title : null,
                }).ToList();
            }

            result.Success = true;
            return result;
        }

        public async Task<Result<WalletViewModel>> CreateWalletAsync(WalletViewModel model)
        {
            Wallet wallet = new Wallet
            {
                Id = model.Id,
                ActivationStatus = model.ActivationStatus,
                Balance = model.Balance,
                WalletType = model.WalletType,
                ParentId = model.ParentId,
                UserId = model.UserId,
                ClientId = model.ClientId,
            };

            var result = this.Insert(wallet);
            model.Id = wallet.Id;

            return new Result<WalletViewModel>()
            {
                Message = "",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<WalletViewModel>> EditWalletAsync(WalletViewModel model)
        {
            var wallet = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);
            wallet.Id = model.Id;
            wallet.ActivationStatus = model.ActivationStatus;
            wallet.Balance = model.Balance;
            wallet.WalletType = model.WalletType;
            wallet.ParentId = model.ParentId;
            wallet.UserId = model.UserId;
            wallet.ClientId = model.ClientId;
            wallet.UserId = model.UserId;

            var result = this.Update(wallet);

            return new Result<WalletViewModel>()
            {
                Message = "",
                Success = true
            };
        }

        public async Task<WalletViewModel> FindAsync(int id)
        {
            var wallet = await this.GetAllAsIqueriable().Select(m => new WalletViewModel()
            {
                Id = m.Id,
                ActivationStatus = m.ActivationStatus,
                Balance = m.Balance,
                ActivationStatusTitle = AssistService.GetEnumDescription(m.ActivationStatus),
                WalletType = m.WalletType,
                WalletTypeTitle = AssistService.GetEnumDescription(m.WalletType),
                ParentId = m.ParentId,
                UserId = m.UserId,
                ClientId = m.ClientId,
                ClientName = m.Client != null ? m.Client.Title : null,
            }).SingleOrDefaultAsync(x => x.Id == id);
            return wallet;
        }

        public async Task<bool> DeleteWalletAsync(int id)
        {
            try
            {
                var Wallet = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                Wallet.IsDeleted = true;

                this.Delete(Wallet);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateNewWalletForNewUser(string userId, string ClientId)
        {
            var client = await walletClientService.GetAll(new WalletClientViewModel() { ClientIdForApi = ClientId });

            var walletExists = await this.GetAll(new WalletViewModel()
            { UserId = userId, ClientId = client.Entity.FirstOrDefault().Id });

            if (walletExists.Entity.Any())
            {
                return true;
            }


            var comissionWallet = new Wallet();
            comissionWallet = new Wallet()
            {
                UserId = userId,
                ParentId = null,
                WalletType = WalletType.Commission,
                ActivationStatus = WalletStatus.Active,
                ClientId = client.Entity.FirstOrDefault().Id
            };

            var PurchesWallet = new Wallet();
            PurchesWallet = new Wallet()
            {
                UserId = userId,
                ParentId = null,
                WalletType = WalletType.Purchase,
                ActivationStatus = WalletStatus.Active,
                ClientId = client.Entity.FirstOrDefault().Id
            };

            var walletList = new List<Wallet>();


            walletList.Add(comissionWallet);
            walletList.Add(PurchesWallet);

            this.InsertRange(walletList);

            return true;
        }
    }
}