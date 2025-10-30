using DataBase.Repository;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using MStech.Wallet.DataBase.Etity.Wallet;
using Mstech.ViewModel.DTO;

namespace Implementation.DiscountCodeServiceArea
{
    public class DiscountCodeService : BaseService<DiscountCode>
    {
        public DiscountCodeService(IUnitOfWork _unitOfWork, IRepository<DiscountCode> _repository) : base(_unitOfWork)
        {
        }

        public async Task<ResponseViewModel<List<DiscountCodeViewModel>>> GetAll(DiscountCodeViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(model.UserId))
            {
                query = query.Where(m => m.UserId == model.UserId);
            }


            var result = new ResponseViewModel<List<DiscountCodeViewModel>>();
            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new DiscountCodeViewModel()
                {
                    Id = m.Id,
                    WalletId = m.WalletId,
                    UserId = m.UserId,
                    UserName = m.User.UserName,
                    UserFullName = m.User != null ? m.User.FullName : null,

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.QueryCount = query.Count();

            }
            else
            {
                result.Entity = query.Select(m => new DiscountCodeViewModel()
                {
                    Id = m.Id,
                    WalletId = m.WalletId,
                    UserId = m.UserId,
                    UserName = m.User.UserName,
                    UserFullName = m.User != null ? m.User.FullName : null,

                }).ToList();
            }
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponseViewModel<DiscountCodeViewModel>> CreateDiscountCodeAsync(DiscountCodeViewModel model)
        {
            DiscountCode discountcode = new DiscountCode
            {
                Id = model.Id,
                WalletId = model.WalletId,
                UserId = model.UserId,
                DiscountCodeBankId = model.DiscountCodeBankId
            };

            var result = this.Insert(discountcode);
            return new ResponseViewModel<DiscountCodeViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<DiscountCodeViewModel>> EditDiscountCodeAsync(DiscountCodeViewModel model)
        {
            var discountcode = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);
            discountcode.Id = model.Id;
            discountcode.WalletId = model.WalletId;
            discountcode.UserId = model.UserId;
            discountcode.DiscountCodeBankId = model.DiscountCodeBankId;


            var result = this.Update(discountcode);

            return new ResponseViewModel<DiscountCodeViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<DiscountCodeViewModel> FindAsync(int id)
        {
            var discountcode = await this.GetAllAsIqueriable().Select(m => new DiscountCodeViewModel()
            {
                Id = m.Id,
                WalletId = m.WalletId,
                UserId = m.UserId,

            }).SingleOrDefaultAsync(x => x.Id == id);
            return discountcode;
        }

        public async Task<bool> DeleteDiscountCodeAsync(int id)
        {
            try
            {
                var DiscountCode = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                DiscountCode.IsDeleted = true;

                this.Delete(DiscountCode);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}