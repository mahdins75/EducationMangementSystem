using DataBase.Repository;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Implementation.CityService;
using MStech.Wallet.DataBase.Etity.Wallet;
using Implementation.WalletServiceArea;
using MStech.Wallet.ViewModel.ViewModel.Wallet;
using Mstech.Wallet.Common;
using Mstech.Accounting.Data;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DNTPersianUtils.Core;
namespace Implementation.TransactionServiceArea
{
    public class TransactionService : BaseService<Transaction>
    {
        private readonly WalletService walletService;
        private readonly string connectionString;


        public TransactionService(IUnitOfWork _unitOfWork, IRepository<Transaction> _repository, WalletService walletService, ApplicationDbContext db, IConfiguration configuration) : base(_unitOfWork)
        {
            this.walletService = walletService;
            this.connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetAll(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallet).ThenInclude(m => m.Client).Include(m => m.TransactionPartyWalle)
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<TransactionViewModel>>();

            if (!string.IsNullOrEmpty(model.WalletOwnerUserName) && model.WalletId <= 0)
            {
                var wallet = await walletService.GetAll(new WalletViewModel()
                {
                    UserName = model.WalletOwnerUserName,
                    WalletType = model.WalletType.HasValue ? model.WalletType.Value : 0
                });
                if (wallet.Success && wallet.Entity != null)
                {
                    query = query.Where(m => wallet.Entity.Where(e => e.Id == m.WalletId).Any());
                }
            }

            if (model.WalletId == null)
            {
                query = query.Where(m => m.Wallet.WalletType == MStech.Accounting.DataBase.Enums.WalletType.Purchase);
            }

            if (!string.IsNullOrEmpty(model.ClientApiId))
            {
                query = query.Where(m => m.Wallet.Client.ClientIdForApi == model.ClientApiId);
            }

            if (model.InvoiceId.HasValue)
            {
                query = query.Where(m => m.InvioceId == model.InvoiceId);
            }

            if (model.WalletId > 0)
            {
                query = query.Where(m => m.WalletId == model.WalletId);
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
                    TransactionSource = m.TransactionSource,
                    JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                    TransactionDateTime = m.TransactionDateTime,
                    TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                    DueDateTime = m.DueDateTime,
                    PersianTextDueDateTime = m.DueDateTime.HasValue ? AssistService.ConvertDateToPersianDate(m.DueDateTime.Value) : "",
                    ParentId = m.ParentId,
                    WalletId = m.WalletId,
                    WalletOwnerUserName = m.Wallet.User.UserName,
                    TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
                    TransactionPartyWalletId = m.TransactionPartyWalleId,
                    InvoiceId = m.InvioceId,
                    WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                    ExternalPaymentUrl = m.Wallet.Client.BaseUrl,
                    IsWalletValue = m.IsWalletValue,
                    Wallet = new WalletViewModel()
                    {
                        Client = new WalletClientViewModel()
                        {
                            BaseUrl = m.Wallet.Client.BaseUrl
                        }
                    }
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
                    DueDateTime = m.DueDateTime,
                    PersianTextDueDateTime = m.DueDateTime.HasValue ? AssistService.ConvertDateToPersianDate(m.DueDateTime.Value) : "",
                    ParentId = m.ParentId,
                    WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                    TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
                    TransactionSource = model.TransactionSource,
                    WalletId = m.WalletId,
                    InvoiceId = m.InvioceId,
                    ExternalPaymentUrl = m.Wallet.Client.BaseUrl,
                    IsWalletValue = m.IsWalletValue,
                    Wallet = new WalletViewModel()
                    {
                        Client = new WalletClientViewModel()
                        {
                            BaseUrl = m.Wallet.Client.BaseUrl
                        }
                    }
                }).ToList();
            }


            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<List<TransactionInvoiceViewModel>>> GetAllInvoices(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted && x.Wallet.Client.ClientIdForApi == model.ClientApiId);

            var result = new ResponseViewModel<List<TransactionInvoiceViewModel>>();

            result.Entity = query.Select(m => new TransactionInvoiceViewModel()
            {
                InvoiceId = m.InvioceId,
                InvoiceTitle = m.InvioceTitle
            }).ToList();


            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<List<TransactionInvoiceViewModel>>> GetAllOfMyInvoices(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted && x.Wallet.Client.ClientIdForApi == model.ClientApiId && x.Wallet.User.UserName == model.WalletOwnerUserName);

            var result = new ResponseViewModel<List<TransactionInvoiceViewModel>>();

            result.Entity = query.Select(m => new TransactionInvoiceViewModel()
            {
                InvoiceId = m.InvioceId,
                InvoiceTitle = m.InvioceTitle
            }).AsEnumerable()
     .DistinctBy(m => m.InvoiceId).ToList();

            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<List<GetAllOfMyClientsViewModel>>> GetAllOfMyClients(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallet).ThenInclude(m => m.Client)
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<GetAllOfMyClientsViewModel>>();

            if (!string.IsNullOrEmpty(model.WalletOwnerUserName))
            {
                var wallet = await walletService.GetAll(new WalletViewModel()
                {
                    UserName = model.WalletOwnerUserName,
                    WalletType = model.WalletType.HasValue ? model.WalletType.Value : 0
                });
                if (wallet.Success && wallet.Entity != null)
                {
                    var walletIdList = wallet.Entity.Select(m => m.Id).ToList();
                    query = query.Where(m => walletIdList.Where(e => e == m.WalletId).Any());
                }
            }

            result.Entity = query.Select(m => new GetAllOfMyClientsViewModel()
            {
                Title = m.Wallet != null ? m.Wallet.Client.Title : "",
                ClientId = m.Wallet != null ? m.Wallet.Client.ClientIdForApi : ""
            }).ToList();

            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<List<GetAllOfMyUsersViewModel>>> GetAllOfMyUsers(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallet).ThenInclude(m => m.User)
                .Where(x => !x.IsDeleted && x.Wallet.Client.ClientIdForApi == model.ClientApiId);

            var result = new ResponseViewModel<List<GetAllOfMyUsersViewModel>>();

            result.Entity = query
     .Select(m => new GetAllOfMyUsersViewModel()
     {
         UserName = m.Wallet.User != null ? m.Wallet.User.UserName : "",
         UserFulName = m.Wallet != null ? m.Wallet.User.FullName : ""
     })
     .AsEnumerable() // Switch to in-memory evaluation
     .DistinctBy(m => m.UserName) // Perform DistinctBy in memory
     .ToList();
            result.IsSuccess = true;

            return result;
        }

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetReportForInvoice(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Wallet).ThenInclude(m => m.User)
                .Where(x => !x.IsDeleted && x.InvioceId == model.InvoiceId && !x.NotMentionedInReport.Value);

            var result = new ResponseViewModel<List<TransactionViewModel>>();

            result.Entity = query.Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                TransactionType = m.TransactionType,
                TransactionTypeTitle = AssistService.GetEnumDescription(m.TransactionType),
                Amount = m.Amount,
                JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                TransactionDateTime = m.TransactionDateTime,
                TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                DueDateTime = m.DueDateTime,
                PersianTextDueDateTime = m.DueDateTime.HasValue ? AssistService.ConvertDateToPersianDate(m.DueDateTime.Value) : "",
                ParentId = m.ParentId,
                WalletId = m.WalletId,
                InvoiceId = m.InvioceId,
                WalletOwnerUserName = m.Wallet.User.UserName,
                WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
                TransactionSource = m.TransactionSource,
                WalletOwnerId = m.Wallet.User.Id,
            }).ToList();
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResponseViewModel<TransactionViewModel>> CreateTransactionAsync(TransactionViewModel model)
        {
            Transaction Transaction = new Transaction
            {
                Id = model.Id,
                TransactionType = model.TransactionType,
                Amount = model.Amount,
                JsonTransActionDataFromClient = !string.IsNullOrEmpty(model.JsonTransActionDataFromClient)
                    ? model.JsonTransActionDataFromClient
                    : "",
                TransactionDateTime = model.TransactionDateTime,
                ParentId = model.ParentId,
                WalletId = model.WalletId,
                InvioceTitle = model.InvoiceTitle,
                InvioceId = model.InvoiceId,
                TransactionPartyWalleId = model.TransactionPartyWalletId,
                WalletTransactionCalculationType = model.WalletTransactionCalculationType,
                TransactionSource = model.TransactionSource,
                NotMentionedInReport = model.NotMentionedInReport,
                CreateDate = DateTime.Now
            };


            await CalculateLatestBalance(Transaction);

            return new ResponseViewModel<TransactionViewModel>()
            {
                Message = "صدور سند دستی با موفقیت انجام شد",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<bool>> CalculateLatestBalance(Transaction model)
        {
            var prevTransaction = this.GetAll()
                .Where(m => m.WalletId == model.WalletId)
                .OrderByDescending(m => m.CreateDate)
                .FirstOrDefault();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseSqlServer(connectionString)
           .Options;

            //await SetProcessingTrueWithRetry(options, model.Id);

            if (prevTransaction == null)
            {
                model.LatestBalance = model.WalletTransactionCalculationType ==
                    MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                    ? model.Amount
                    : model.Amount * -1;
            }
            else
            {
                var prevValue = prevTransaction.WalletTransactionCalculationType ==
                    MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                    ? prevTransaction.Amount
                    : prevTransaction.Amount * -1;

                var currentValue = model.WalletTransactionCalculationType ==
                    MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                    ? model.Amount
                    : model.Amount * -1;

                model.LatestBalance = prevTransaction.LatestBalance + currentValue;
            }
            var wallet = await walletService.GetAllAsIqueriable().Where(m => m.Id == model.WalletId).FirstOrDefaultAsync();

            if (wallet != null)
            {
                wallet.Balance = model.LatestBalance;
                walletService.Update(wallet);

            }
            var result = this.Insert(model);




            //await SetProcessingFalseWithRetry(options, model.Id);

            return new ResponseViewModel<bool>();

        }

        public async Task<ResponseViewModel<bool>> CalculateLatestBalanceInRange(List<Transaction> models)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseSqlServer(connectionString)
          .Options;

            foreach (var item in models)
            {
                //await SetProcessingTrueWithRetry(options, item.Id);

                var prevTransaction = this.GetAll()
                .Where(m => m.WalletId == item.WalletId)
                .OrderByDescending(m => m.CreateDate)
                .FirstOrDefault();

                if (prevTransaction == null)
                {
                    item.LatestBalance = item.WalletTransactionCalculationType ==
                        MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                        ? item.Amount
                        : item.Amount * -1;
                }
                else
                {
                    var prevValue = prevTransaction.WalletTransactionCalculationType ==
                        MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                        ? prevTransaction.Amount
                        : prevTransaction.Amount * -1;

                    var currentValue = item.WalletTransactionCalculationType ==
                        MStech.Accounting.DataBase.Enums.WalletTransactionCalculationType.Deposit
                        ? item.Amount
                        : item.Amount * -1;

                    item.LatestBalance = prevTransaction.LatestBalance + currentValue;
                }

                var wallet = await walletService.GetAllAsIqueriable().Where(m => m.Id == item.WalletId).FirstOrDefaultAsync();

                if (wallet != null)
                {
                    wallet.Balance = item.LatestBalance;
                    walletService.Update(wallet);

                }
                this.Insert(item);
                //await SetProcessingFalseWithRetry(options, item.Id);

            }


            return new ResponseViewModel<bool>();

        }

        public async Task<ResponseViewModel<TransactionViewModel>> Transfer(TransferTransactionViewModel model)
        {
            Transaction SenderTransaction = new Transaction
            {
                TransactionType = model.TransactionType,
                Amount = model.Amount,
                JsonTransActionDataFromClient = "",
                TransactionDateTime = DateTime.Now,
                ParentId = null,
                WalletId = model.SenderWalletId,
                InvioceId = null,
                WalletTransactionCalculationType = model.WalletTransactionCalculationType,
                TransactionSource = "انتقال کیف پول به دیگر کیف پول",
                IsWalletValue = true
            };

            Transaction ReciverTransaction = new Transaction
            {
                TransactionType = model.TransactionType,
                Amount = model.Amount,
                JsonTransActionDataFromClient = "",
                TransactionDateTime = DateTime.Now,
                ParentId = null,
                WalletId = model.SenderWalletId,
                InvioceId = null,
                WalletTransactionCalculationType = model.WalletTransactionCalculationType,
                TransactionSource = "انتقال کیف پول به دیگر کیف پول",
                IsWalletValue = true
            };

            var senderResult = this.Insert(SenderTransaction);
            var reciverResult = this.Insert(ReciverTransaction);

            return new ResponseViewModel<TransactionViewModel>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<bool>> CreateRangeTransactionAsync(List<TransactionViewModel> model)
        {
            var transactionsList = new List<Transaction>();

            foreach (var item in model)
            {
                Transaction Transaction = new Transaction
                {
                    Id = item.Id,
                    TransactionType = item.TransactionType,
                    Amount = item.Amount,
                    JsonTransActionDataFromClient = item.JsonTransActionDataFromClient,
                    TransactionDateTime = item.TransactionDateTime,
                    ParentId = item.ParentId,
                    WalletId = item.WalletId,
                    IsWalletValue = item.IsWalletValue.Value,
                    InvioceId = item.InvoiceId,
                    TransactionPartyWalleId = item.TransactionPartyWalletId,
                    WalletTransactionCalculationType = item.WalletTransactionCalculationType,
                    NotMentionedInReport = item.NotMentionedInReport,
                    TransactionSource = item.TransactionSource,
                    CreateDate = DateTime.Now

                };
                var wallet = await walletService.GetAllAsIqueriable().Where(m => m.Id == item.Id).FirstOrDefaultAsync();

                transactionsList.Add(Transaction);

            }
            await CalculateLatestBalanceInRange(transactionsList);
            return new ResponseViewModel<bool>()
            {
                Message = "",
                IsSuccess = true
            };
        }

        public static async Task SetProcessingTrueWithRetry(DbContextOptions<ApplicationDbContext> options, int id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                while (true)
                {
                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        // Attempt to set Processing to true only if it's currently false
                        int rowsUpdated = await context.Database.ExecuteSqlRawAsync(
                            "UPDATE [dbo].[Transaction]  SET   [Processing] = 1 WHERE Id=@id AND [Processing]=0",
                            new Microsoft.Data.SqlClient.SqlParameter("@Id", id));

                        if (rowsUpdated > 0)
                        {
                            // Successfully set Processing to true
                            await transaction.CommitAsync();
                            Console.WriteLine($"Processing set to true for Id {id}");
                            return;
                        }
                        else
                        {
                            // Processing was already true, roll back and wait
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Processing was already true for Id {id}, waiting...");
                            await Task.Delay(1000); // Wait for 1 second before retrying
                        }
                    }
                }
            }
        }

        public static async Task SetProcessingFalseWithRetry(DbContextOptions<ApplicationDbContext> options, int id)
        {
            using (var context = new ApplicationDbContext(options))
            {
                while (true)
                {
                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        // Attempt to set Processing to true only if it's currently false
                        int rowsUpdated = await context.Database.ExecuteSqlRawAsync(
                            "UPDATE [dbo].[Transaction]  SET   [Processing] = 0 WHERE Id=@id ",
                            new Microsoft.Data.SqlClient.SqlParameter("@Id", id));

                        if (rowsUpdated > 0)
                        {
                            // Successfully set Processing to true
                            await transaction.CommitAsync();
                            Console.WriteLine($"Processing set to true for Id {id}");
                            return;
                        }
                        else
                        {
                            // Processing was already true, roll back and wait
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Processing was already true for Id {id}, waiting...");
                            return;
                        }
                    }
                }
            }
        }

        public async Task<ResponseViewModel<TransactionViewModel>> EditTransactionAsync(TransactionViewModel model)
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

            var result = this.Update(Transaction);

            return new ResponseViewModel<TransactionViewModel>()
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
                TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
            }).SingleOrDefaultAsync(x => x.Id == id);
            return Transaction;
        }

        public async Task<ResponseViewModel<string>> GetBalance(int walletId)
        {
            var Transaction = await this.GetAllAsIqueriable().OrderByDescending(m => m.CreateDate).FirstOrDefaultAsync(x => x.WalletId == walletId);

            if (Transaction == null)
            {
                return new ResponseViewModel<string>() { IsSuccess = true, Entity = "0" };
            }
            else
            {
                return new ResponseViewModel<string>() { IsSuccess = true, Entity = Transaction.LatestBalance.ToNoDecimalString() };
            }
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

        public async Task<ResponseViewModel<List<TransactionViewModel>>> GetAllForClient(TransactionViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<TransactionViewModel>>();

            if (string.IsNullOrEmpty(model.WalletOwnerUserName))
            {
                query = query.Where(m => m.Wallet.User.UserName == model.WalletOwnerUserName);
            }

            if (model.InvoiceId > 0)
            {
                query = query.Where(m => m.InvioceId == model.InvoiceId);
            }

            result.Entity = query.Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                TransactionType = m.TransactionType,
                TransactionTypeTitle = AssistService.GetEnumDescription(m.TransactionType),
                Amount = m.Amount,
                JsonTransActionDataFromClient = m.JsonTransActionDataFromClient,
                TransactionDateTime = m.TransactionDateTime,
                TransactionDateTimeText = AssistService.ConvertDateToPersianDate(m.TransactionDateTime),
                DueDateTime = m.DueDateTime,
                PersianTextDueDateTime = m.DueDateTime.HasValue ? AssistService.ConvertDateToPersianDate(m.DueDateTime.Value) : "",
                ParentId = m.ParentId,
                WalletId = m.WalletId,
                WalletTransactionCalculationType = m.WalletTransactionCalculationType,
                TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
                IsWalletValue = m.IsWalletValue,
            }).ToList();
            return result;
        }

        public async Task<ResponseViewModel<TransactionViewModel>> CreateTransactionForClientAsync(TransactionViewModel model)
        {
            Transaction Transaction = new Transaction
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
                InvioceTitle = model.InvoiceTitle,
                InvioceId = model.InvoiceId
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
                TransactionPartyUserName = m.TransactionPartyWalle.User.UserName + "-" + m.TransactionPartyWalle.User.FullName,
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