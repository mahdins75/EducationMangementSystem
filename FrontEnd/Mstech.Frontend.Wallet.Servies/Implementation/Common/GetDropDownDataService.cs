using Mstech.Frontend.Wallet.Service.Interface;
using Mstech.Frontend.Wallet.Service.Interface.Membership;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;
using Blazored.LocalStorage;
using Mstech.Frontend.Wallet.Service.Interface.Common;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Reflection;

namespace Mstech.Frontend.Wallet.Service.Implementation.Membership
{
    public class GetDropDownDataService : IGetDropDownDataService
    {
        private readonly IUserService userService;
        private readonly IApiService apiService;
        private readonly GeneralInfo generalInfo;

        public GetDropDownDataService(IUserService userService, IApiService apiService, GeneralInfo generalInfo)
        {
            this.userService = userService;
            this.apiService = apiService;
            this.generalInfo = generalInfo;
        }

        public async Task<ResponseViewModel<List<SelectListItemStringValue>>> GetDropDownUsers(UserViewModel model)
        {
            var url = generalInfo.GetApiBaseAddress() + $@"/api/Common/GetDropDownData/GetDropDownUsers?Id={model.Id}";

            var response = await apiService.GetAsync<List<SelectListItemStringValue>>(url);

            return response;

        }

        public async Task<ResponseViewModel<List<SelectListItem>>> GetEnumFropDown<TEnum>() where TEnum : Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            var selectList = enumValues
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e),
                    Title = GetEnumDescription(e)  // Get the description of the enum
                })
                .ToList();


            return new ResponseViewModel<List<SelectListItem>>() { Entity = selectList };

        }
        public static string GetEnumDescription<TEnum>(TEnum value) where TEnum : Enum
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
