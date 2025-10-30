using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Implementation.CityService;
using MStech.Wallet.DataBase.Etity.Wallet;
using Implementation.WalletServiceArea;
using Mstech.Accounting.Data;

namespace Implementation.TransactionServiceArea
{
    public class TransactionRequestService : BaseService<TransactionRequest>
    {

        private readonly WalletService walletService;
        private readonly ApplicationDbContext db;
        public TransactionRequestService(IUnitOfWork _unitOfWork, IRepository<Transaction> _repository, WalletService walletService, ApplicationDbContext db) : base(_unitOfWork)
        {
            this.walletService = walletService;
            this.db = db;
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetAll(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<TransactionViewModel>>();

            if (!string.IsNullOrEmpty(model.WalletOwnerUserName) && model.WalletId <= 0)
            {
                var wallet = await walletService.GetAll(new WalletViewModel() { UserName = model.WalletOwnerUserName, WalletType = model.WalletType.HasValue ? model.WalletType.Value : 0 });
                if (wallet.Success && wallet.Entity != null)
                {
                    query = query.Where(m => wallet.Entity.Where(e => e.Id == m.WalletId).Any());
                }
            }


            if (model.IsPagination)
            {
                result.QueryCount = query.Count();

                result.Entity = query.Select(m => new TransactionViewModel()
                {
                    Id = m.Id,
                    TransactionType = m.TransactionType,
                    TransactionTypeTitle = AssistService.GetEnumDescription(m.TransactionType),
                    Amount = m.Amount,
                    JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                    TransactionDateTime = m.TransactionDateTime,
                    TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                    ParentId = m.ParentId,
                    WalletId = m.WalletId,
                    InvoiceId = m.InvioceId,
                    WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                    TransactionSource = model.TransactionSource,
                    IsWalletValue = m.IsWalletValue

                }).Skip(model.Skip).Take(model.PageSize).ToList();
            }
            else
            {
                result.Entity = query.Select(m => new TransactionViewModel()
                {
                    Id = m.Id,
                    TransactionType = m.TransactionType,
                    TransactionTypeTitle = AssistService.GetEnumDescription(m.TransactionType),
                    Amount = m.Amount,
                    JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                    TransactionDateTime = m.TransactionDateTime,
                    TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                    ParentId = m.ParentId,
                    WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                    TransactionSource = model.TransactionSource,
                    WalletId = m.WalletId,
                    InvoiceId = m.InvioceId,
                    IsWalletValue = m.IsWalletValue

                }).ToList();
            }



            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<TransactionRequestViewModel>> CreateTransactionAsync(TransactionRequestViewModel model)
        {

            TransactionRequest transaction = new TransactionRequest
            {
                Id = model.Id,
                TransactionType = model.TransactionType,
                Amount = model.Amount,
                JsonTransActionDataFromClient = !string.IsNullOrEmpty(model.JsonTransActionDataFromClient) ? model.JsonTransActionDataFromClient : "",
                TransactionDateTime = model.TransactionDateTime,
                ParentId = model.ParentId,
                WalletId = model.WalletId,
                WalletTransactionCalculationType = model.WalletTransactionCalculationType,
                TransactionSource = model.TransactionSource,
                NotMentionedInReport = model.NotMentionedInReport
            };

            var result = this.Insert(transaction);
            return new ResponseViewModel<TransactionRequestViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<TransactionViewModel> FindAsync(int id)
        {
            var Transaction = await this.GetAllAsIqueriable().Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                TransactionType = m.TransactionType,
                Amount = m.Amount,
                JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                TransactionDateTime = m.TransactionDateTime,
                ParentId = m.ParentId,
                WalletId = m.WalletId,
                WalletTransactionCalculationType = m.WalletTransactionCalculationType,

            }).SingleOrDefaultAsync(x => x.Id == id);
            return Transaction;
        }

        public async Task<TransactionRequestViewModel> ConfirmTransactionRequestAsync(TransactionRequestViewModel model)
        {
            var wallet = await db.Set<Wallet>().Where(m => m.Id == model.WalletId && m.User.UserName == model.WalletOwnerUserName).FirstOrDefaultAsync();

            var transactionRequest = this.GetAll().Where(m => m.Id == model.Id && m.WalletId == wallet.Id).FirstOrDefault();

            if (transactionRequest != null)
            {
                if (transactionRequest.ConfirmationCode == model.ConfirmationCode)
                {
                    transactionRequest.IsConfirmed = true;
                }
            }
            db.SaveChangesAsync();

            return model;
        }
        public async Task<ResponseViewModel<decimal>> SumOfAllRequestAmountsAynce()
        {
            var amount = db.Set<TransactionRequest>().Where(m => m.IsConfirmed == false).Sum(m => m.Amount);

            return new ResponseViewModel<decimal>() { IsSuccess = true, Entity = amount };
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                var Transaction = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                Transaction.IsDeleted = true;

                this.Delete(Transaction);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseViewModel<List<TransactionRequestViewModel>>> GetAllForClient(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<TransactionRequestViewModel>>();

            if (model.InvoiceId > 0)
            {
                query = query.Where(m => m.InvioceId == model.InvoiceId);
            }
            result.Entity = query.Select(m => new TransactionRequestViewModel()
            {
                Id = m.Id,
                TransactionType = m.TransactionType,
                TransactionTypeTitle = AssistService.GetEnumDescription(m.TransactionType),
                Amount = m.Amount,
                JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                TransactionDateTime = m.TransactionDateTime,
                TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                ParentId = m.ParentId,
                WalletId = m.WalletId,
                WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                IsWalletValue = m.IsWalletValue,
            }).ToList();
            return result;
        }

        public async Task<ResponseViewModel<TransactionViewModel>> CreateTransactionForClientAsync(TransactionViewModel model)
        {
            TransactionRequest Transaction = new TransactionRequest
            {
                Id = model.Id,
                TransactionType = model.TransactionType,
                Amount = model.Amount,
                JsonTransActionDataFromClient = model.JsonTransActionDataFromClient,
                TransactionDateTime = model.TransactionDateTime,
                ParentId = model.ParentId,
                WalletTransactionCalculationType = model.WalletTransactionCalculationType,
                TransactionSource = model.TransactionSource,
                WalletId = model.WalletId,

            };

            var result = this.Insert(Transaction);
            return new ResponseViewModel<TransactionViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<TransactionViewModel>> EditTransactionForClientAsync(TransactionViewModel model)
        {
            var Transaction = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);
            Transaction.Id = model.Id;
            Transaction.TransactionType = model.TransactionType;
            Transaction.Amount = model.Amount;
            Transaction.JsonTransActionDataFromClient = model.JsonTransActionDataFromClient;
            Transaction.TransactionDateTime = model.TransactionDateTime;
            Transaction.ParentId = model.ParentId;
            Transaction.WalletId = model.WalletId;
            Transaction.WalletTransactionCalculationType = model.WalletTransactionCalculationType;
            Transaction.TransactionSource = model.TransactionSource;

            var result = this.Update(Transaction);

            return new ResponseViewModel<TransactionViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<TransactionViewModel> FindForClientAsync(int id)
        {
            var Transaction = await this.GetAllAsIqueriable().Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                TransactionType = m.TransactionType,
                Amount = m.Amount,
                JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                TransactionDateTime = m.TransactionDateTime,
                ParentId = m.ParentId,
                WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                TransactionSource = m.TransactionSource,

                WalletId = m.WalletId,

            }).SingleOrDefaultAsync(x => x.Id == id);
            return Transaction;
        }

        public async Task<bool> DeleteTransactionForClientAsync(int id)
        {
            try
            {
                var Transaction = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                Transaction.IsDeleted = true;

                this.Delete(Transaction);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}