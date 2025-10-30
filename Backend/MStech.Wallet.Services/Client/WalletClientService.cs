using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using MStech.Wallet.DataBase.Etity.Client;
using Mstech.Accounting.Data;

namespace Implementation.WalletClientArea
{
    public class WalletClientService : BaseService<WalletClient>
    {


        private readonly ApplicationDbContext db;
        public WalletClientService(IUnitOfWork _unitOfWork, IRepository<WalletClient> _repositorys, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.db = db;
        }

        public async Task<Result<List<WalletClientViewModel>>> GetAll(WalletClientViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallets).Include(m => m.Owner)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<WalletClientViewModel>>();
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.ClientIdForApi))
            {
                query = query.Where(m => m.ClientIdForApi == model.ClientIdForApi);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(m => m.Wallets.Where(x => x.User.UserName == model.UserName).Any());
            }

            if (!string.IsNullOrEmpty(model.OwnerUserName))
            {
                query = query.Where(m => m.Owner.UserName == model.OwnerUserName);
            }
            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(m => m.Wallets.Where(x => x.UserId == model.UserId).Any());
            }


            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new WalletClientViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ActivationStatus = m.ActivationStatus,
                    OwnerId = m.OwnerId,
                    OwnerUserName = m.Owner.UserName,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    ActivationStatusTitle = m.ActivationStatus.ToString(),
                    WalletsCount = m.Wallets.Any() ? m.Wallets.Count() : 0

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new WalletClientViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ActivationStatus = m.ActivationStatus,
                    OwnerId = m.OwnerId,
                    BaseUrl = m.BaseUrl,
                    OwnerUserName = m.Owner.UserName,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    ActivationStatusTitle = m.ActivationStatus.ToString(),
                    WalletsCount = m.Wallets.Any() ? m.Wallets.Count() : 0

                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }

        }
        public async Task<Result<List<WalletClientViewModel>>> GetAllWithApiId(WalletClientViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallets).Include(m => m.Owner)
                .Where(x => !x.IsDeleted);

            var result = new Result<List<WalletClientViewModel>>();
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (!string.IsNullOrEmpty(model.ClientIdForApi))
            {
                query = query.Where(m => m.ClientIdForApi == model.ClientIdForApi);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(m => m.Wallets.Where(x => x.User.UserName == model.UserName).Any());
            }
            if (!string.IsNullOrEmpty(model.OwnerUserName))
            {
                query = query.Where(m => m.Owner.UserName == model.OwnerUserName);
            }
            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(m => m.Wallets.Where(x => x.UserId == model.UserId).Any());
            }


            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new WalletClientViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ActivationStatus = m.ActivationStatus,
                    OwnerId = m.OwnerId,
                    OwnerUserName = m.Owner.UserName,
                    ClientIdForApi = m.ClientIdForApi,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    ActivationStatusTitle = m.ActivationStatus.ToString(),
                    WalletsCount = m.Wallets.Any() ? m.Wallets.Count() : 0

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new WalletClientViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ActivationStatus = m.ActivationStatus,
                    OwnerId = m.OwnerId,
                    BaseUrl = m.BaseUrl,
                    OwnerUserName = m.Owner.UserName,
                    ClientIdForApi = m.ClientIdForApi,
                    OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                    ActivationStatusTitle = m.ActivationStatus.ToString(),
                    WalletsCount = m.Wallets.Any() ? m.Wallets.Count() : 0

                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }

        }
        public async Task<Result<WalletClientViewModel>> CreateWalletClientAsync(WalletClientViewModel model)
        {
            WalletClient WalletClient = new WalletClient
            {
                Id = model.Id,
                Title = model.Title,
                ActivationStatus = model.ActivationStatus,
                OwnerId = model.OwnerId,
                BaseUrl = string.Empty,
                ClientIdForApi = Guid.NewGuid().ToString()

            };

            var result = this.Insert(WalletClient);

            model.Id = WalletClient.Id;

            return new Result<WalletClientViewModel>()
            {
                Message = "",
                Success = true,
                Entity = model
            };
        }

        public async Task<Result<WalletClientViewModel>> EditWalletClientAsync(WalletClientViewModel model)
        {
            var WalletClient = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);
            WalletClient.Id = model.Id;
            WalletClient.Title = model.Title;
            WalletClient.ActivationStatus = model.ActivationStatus;
            WalletClient.OwnerId = model.OwnerId;


            var result = this.Update(WalletClient);

            return new Result<WalletClientViewModel>()
            {
                Message = "",
                Success = true
            };
        }

        public async Task<WalletClientViewModel> FindAsync(int id)
        {
            var WalletClient = await this.GetAllAsIqueriable().Select(m => new WalletClientViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                ActivationStatus = m.ActivationStatus,
                OwnerId = m.OwnerId,
                OwnerFullName = m.Owner != null ? m.Owner.FullName : null,
                ActivationStatusTitle = m.ActivationStatus.ToString()

            }).SingleOrDefaultAsync(x => x.Id == id);
            return WalletClient;
        }

        public async Task<bool> DeleteWalletClientAsync(int id)
        {
            try
            {
                var WalletClient = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                WalletClient.IsDeleted = true;

                this.Update(WalletClient);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}