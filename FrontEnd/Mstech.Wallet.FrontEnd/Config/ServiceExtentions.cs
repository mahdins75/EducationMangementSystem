using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Implementation;
using Mstech.Frontend.Wallet.Service.Implementation.Membership;
using Mstech.Frontend.Wallet.Service.Implementation.Notification;
using Mstech.Frontend.Wallet.Service.Implementation.TransactionRequest;
using Mstech.Frontend.Wallet.Service.Implementation.Wallet;
using Mstech.Frontend.Wallet.Service.Implementation.WalletClient;
using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Common;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.Frontend.Wallet.Service.Interface.Notification;
using Mstech.Frontend.Wallet.Service.Interface.TransactionRequest;
using Mstech.Frontend.Wallet.Service.Interface.WalletClient;
using Radzen;

namespace Mstech.Wallet.FrontEnd.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, string clientApi)
        {
            services.AddScoped<IApiService>(provider =>
            {

                var httpClient = provider.GetRequiredService<HttpClient>();
                var localStorage = provider.GetRequiredService<ILocalStorageService>();

                return new ApiService(httpClient, localStorage, clientApi);
            });

            services.AddScoped<IConfigInfoService, ConfigInfoService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IWalletClientService, WalletClientService>();

            services.AddScoped<IGetDropDownDataService, GetDropDownDataService>();

            services.AddScoped<IWalletService, WalletService>();

            services.AddScoped<ISideNavService, SideNavService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IRoleAccessedLinkService, RoleAccessedLinkService>();

            services.AddScoped<IAccessedLinkService, AccessedLinkService>();

            services.AddScoped<IDashboardService, DashboardService>();

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<IDynamicTitleService, DynamicTitleService>();

            services.AddScoped<IPaymentRequest, PaymentRequest>();

            services.AddScoped<ITransactionRequestService, TransactionRequestService>();

            services.AddScoped<ISMSService, SMSService>();

            services.AddScoped<DialogService>();

            services.AddScoped<NotificationService>();

            services.AddScoped<TooltipService>();

            services.AddScoped<ContextMenuService>();

            services.AddScoped<SweetAlertService>();

            services.AddScoped<GeneralInfo>();


            // Register other services here as needed
        }
    }
}