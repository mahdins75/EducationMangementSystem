using DataBase.Repository;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using MStech.Wallet.DataBase.Etity.Wallet;
using Mstech.ViewModel.DTO;
using ViewModel.Infrastructure;
using Implementation.CityService;

namespace Implementation.DiscountCodeServiceArea
{
    public class DiscountCodeBankService : BaseService<DiscountCodeBank>
    {
        public DiscountCodeBankService(IUnitOfWork _unitOfWork, IRepository<DiscountCode> _repository) : base(_unitOfWork)
        {
        }

        public async Task<ResponseViewModel<List<DiscountCodeBankViewModel>>> GetAll(DiscountCodeBankViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }
            if (!string.IsNullOrEmpty(model.ItemId))
            {
                query = query.Where(m => m.ItemId == model.ItemId);
            }

            if (!string.IsNullOrEmpty(model.OwnerId))
            {
                query = query.Where(m => m.OwnerId == model.OwnerId);
            }

            if (!string.IsNullOrEmpty(model.ClientIdForApi))
            {
                query = query.Where(m => m.ClientIdForApi == model.ClientIdForApi);
            }

            if (!string.IsNullOrEmpty(model.DiscountCodeText))
            {
                query = query.Where(m => m.DiscountCodeText.ToLower() == model.DiscountCodeText.ToLower());
            }

            var result = new ResponseViewModel<List<DiscountCodeBankViewModel>>();
            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new DiscountCodeBankViewModel()
                {
                    Id = m.Id,
                    DiscountCodeText = m.DiscountCodeText,
                    Title = m.Title,
                    DiscountAmount = m.DiscountAmount,
                    ExpireDate = m.ExpireDate,
                    UnitId = m.UnitId,
                    ItemId = m.ItemId,
                    DiscountCodeBankSpendType = m.DiscountCodeBankSpendType,
                    DiscountCodeBankSpendTypeDisplayName = AssistService.GetEnumDescription(m.DiscountCodeBankSpendType),
                    OwnerId = m.OwnerId,
                    PersianExpireDate = AssistService.ConvertDateToPersianDate(m.ExpireDate)
                }).Skip(model.Skip).Take(model.PageSize).ToList();

                result.IsSuccess = true;
                result.QueryCount = query.Count();

            }
            else
            {
                result.Entity = query.Select(m => new DiscountCodeBankViewModel()
                {
                    Id = m.Id,
                    DiscountCodeText = m.DiscountCodeText,
                    Title = m.Title,
                    DiscountAmount = m.DiscountAmount,
                    ExpireDate = m.ExpireDate,
                    DiscountCodeBankSpendType = m.DiscountCodeBankSpendType,
                    DiscountCodeBankSpendTypeDisplayName = AssistService.GetEnumDescription(m.DiscountCodeBankSpendType),
                    UnitId = m.UnitId,
                    ItemId = m.ItemId,
                    OwnerId = m.OwnerId,
                    PersianExpireDate = AssistService.ConvertDateToPersianDate(m.ExpireDate)

                }).ToList();
                result.IsSuccess = true;
            }

            return result;
        }

        public async Task<Result<DiscountCodeBankViewModel>> GetCount(DiscountCodeBankViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Owner)
               .Where(x => !x.IsDeleted && x.ClientIdForApi == model.ClientIdForApi && x.OwnerId == model.OwnerId);

            var result = new Result<DiscountCodeBankViewModel>();

            result.Count = query.Count();

            return result;



        }

        public async Task<ResponseViewModel<DiscountCodeBankViewModel>> CreateDiscountCodeAsync(DiscountCodeBankViewModel model)
        {
            DiscountCodeBank discountcode = new DiscountCodeBank
            {
                Id = model.Id,
                DiscountCodeText = model.DiscountCodeText,
                Title = model.Title,
                DiscountAmount = model.DiscountAmount,
                ExpireDate = model.ExpireDate,
                ClientIdForApi = model.ClientIdForApi,
                OwnerId = model.OwnerId,
                ItemId = model.ItemId,
                UnitId = model.UnitId,
                DiscountCodeBankSpendType = model.DiscountCodeBankSpendType
            };
            var result = this.Insert(discountcode);
            model.Id = discountcode.Id;
            return new ResponseViewModel<DiscountCodeBankViewModel>()
            {
                Message = "",
                Entity = model,
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<DiscountCodeBankViewModel>> EditDiscountCodeAsync(DiscountCodeBankViewModel model)
        {
            var discountcode = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);

            discountcode.Id = model.Id;
            discountcode.DiscountCodeText = model.DiscountCodeText;
            discountcode.Title = model.Title;
            discountcode.DiscountAmount = model.DiscountAmount;
            discountcode.ExpireDate = model.ExpireDate;
            discountcode.ItemId = model.ItemId;
            discountcode.UnitId = model.UnitId;
            discountcode.OwnerId = model.OwnerId;
            discountcode.DiscountCodeBankSpendType = model.DiscountCodeBankSpendType;

            var result = this.Update(discountcode);

            return new ResponseViewModel<DiscountCodeBankViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<DiscountCodeBankViewModel> FindAsync(int id)
        {
            var discountcode = await this.GetAllAsIqueriable().Select(m => new DiscountCodeBankViewModel()
            {
                Id = m.Id,
                DiscountCodeText = m.DiscountCodeText,
                Title = m.Title,
                DiscountAmount = m.DiscountAmount,
                ExpireDate = m.ExpireDate,

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


        //// client api section 
        ///


    }

}