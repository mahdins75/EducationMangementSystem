using Mstech.Frontend.Wallet.Service.Interface.WalletClient;
using Mstech.Frontend.Wallet.ViewModel.DTO;
using Mstech.FrontEnd.Wallet.ViewModel;

namespace Mstech.Frontend.Wallet.Service.Implementation.WalletClient
{
    public class PaymentRequest:IPaymentRequest
    {
        
        public async Task<ResponseViewModel<string>> CreateGatewayRequest(TransactionViewModel model)
        {
            var response = new ResponseViewModel<string>();
            try
            {
                // Simulate an asynchronous operation
                await Task.Delay(1000);

                // Here you would typically call a service or perform some logic
                //@@create payment link here 
                // For demonstration, we are just returning a success message
                response.Entity = "Payment request processed successfully.";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}