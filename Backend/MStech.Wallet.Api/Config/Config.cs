using Common.Models;
using DataBase.Repository;
using Implementation.AccessedLinkService;
using Implementation.AccessLinkeRoles;
using Implementation.CityService;
using Implementation.ConstantService;
using Implementation.DiscountCodeServiceArea;
using Implementation.FileManager;
using Implementation.FileService;
using Implementation.Notification;
using Implementation.ProvinceService;
using Implementation.ReferralCodeServiceArea;
using Implementation.RoleService;
using Implementation.RoleUserService;
using Implementation.TokenManagement;
using Implementation.TransactionServiceArea;
using Implementation.UserService;
using Implementation.WalletClientArea;
using Implementation.WalletServiceArea;
using Microsoft.AspNetCore.Authorization;
using Mstech.Accounting.Data;

namespace Mstech.ADV.Config
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<CityService>();
            services.AddScoped<ProvinceService>();
            services.AddScoped<EmailService>();
            services.AddScoped<ModirPayamakService>();
            services.AddScoped<FileManagerService>();
            services.AddScoped<RoleUserService>();
            services.AddScoped<UserService>();
            services.AddScoped<ConstantService>();
            services.AddScoped<AccessedLinkService>();
            services.AddScoped<RoleAccessedLinkService>();
            services.AddScoped<RoleService>();
            services.AddSingleton<TokenService>();
            services.AddScoped<ControllerActionDiscoveryService>();
            services.AddScoped<ControllerActionDiscoveryHostedService>();
            services.AddScoped<AccessLinkRolesService>();
            services.AddScoped<FileService>();
            services.AddScoped<WalletClientService>();
            services.AddScoped<WalletService>();
            services.AddScoped<TransactionService>();
            services.AddScoped<ReferralCodeService>();
            services.AddScoped<DiscountCodeService>();
            services.AddScoped<DashboardService>();
            services.AddScoped<DiscountCodeBankService>();
            services.AddScoped<TransactionRequestService>();

            return services;
        }
    }
}