using DataBase.Repository;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using MStech.Wallet.DataBase.Etity.Wallet;
using Mstech.ViewModel.DTO;
using Implementation.CityService;

namespace Implementation.ReferralCodeServiceArea
{
    public class ReferralCodeService : BaseService<ReferralCode>
    {
        public ReferralCodeService(IUnitOfWork _unitOfWork, IRepository<ReferralCode> _repository) : base(_unitOfWork)
        {
        }

        public async Task<ResponseViewModel<List<ReferralCodeViewModel>>> GetAll(ReferralCodeViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallet).ThenInclude(m => m.Client)
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(m => m.UserId == model.UserId);
            }


            var result = new ResponseViewModel<List<ReferralCodeViewModel>>();
            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new ReferralCodeViewModel()
                {
                    Id = m.Id,
                    WalletId = m.WalletId,
                    UserId = m.UserId,
                    ReferralCodeText = model.ReferralCodeText,
                    UserFullName = m.User != null ? m.User.FullName : null,
                    Wallet = new WalletViewModel() { Id = m.Wallet.Id, WalletTypeTitle = AssistService.GetEnumDescription(m.Wallet.WalletType), Client = new WalletClientViewModel() { Title = m.Wallet.Client.Title } }

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.QueryCount = query.Count();

            }
            else
            {
                result.Entity = query.Select(m => new ReferralCodeViewModel()
                {
                    Id = m.Id,
                    WalletId = m.WalletId,
                    UserId = m.UserId,
                    ReferralCodeText = model.ReferralCodeText,
                    UserFullName = m.User != null ? m.User.FullName : null,

                }).ToList();
            }

            return result;
        }

        public async Task<ResponseViewModel<ReferralCodeViewModel>> CreateReferralCodeAsync(ReferralCodeViewModel model)
        {
            ReferralCode referralcode = new ReferralCode
            {
                Id = model.Id,
                WalletId = model.WalletId,
                UserId = model.UserId,
                ReferralCodeText = model.ReferralCodeText,

            };

            var result = this.Insert(referralcode);
            return new ResponseViewModel<ReferralCodeViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<ReferralCodeViewModel>> EditReferralCodeAsync(ReferralCodeViewModel model)
        {
            var referralcode = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);
            referralcode.Id = model.Id;
            referralcode.WalletId = model.WalletId;
            referralcode.UserId = model.UserId;
            referralcode.ReferralCodeText = model.ReferralCodeText;


            var result = this.Update(referralcode);

            return new ResponseViewModel<ReferralCodeViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<ReferralCodeViewModel> FindAsync(int id)
        {
            var referralcode = await this.GetAllAsIqueriable().Select(m => new ReferralCodeViewModel()
            {
                Id = m.Id,
                WalletId = m.WalletId,
                UserId = m.UserId,
                ReferralCodeText = m.ReferralCodeText,

            }).SingleOrDefaultAsync(x => x.Id == id);
            return referralcode;
        }

        public async Task<bool> DeleteReferralCodeAsync(int id)
        {
            try
            {
                var ReferralCode = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                ReferralCode.IsDeleted = true;

                this.Delete(ReferralCode);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }

}