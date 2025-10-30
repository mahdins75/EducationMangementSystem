using Microsoft.JSInterop;
using Mstech.Frontend.Wallet.ViewModel.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SweetAlertService
{
    private readonly IJSRuntime _jsRuntime;

    public SweetAlertService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public async Task ShowToast(string message, string type)
    {
        await _jsRuntime.InvokeVoidAsync("showToast", message, type);
    }
    public async Task ShowAlert(string title, string text, string icon)
    {
        AlertOptions options = new AlertOptions()
        {
            title = title,
            text = text,
            icon = icon,
            confirmButtonText = "OK"

        };
        await _jsRuntime.InvokeVoidAsync("Swal.fire", options);
    }

    public async Task<bool> ShowConfirmation(string title, string text)
    {
        AlertOptions options = new AlertOptions()
        {
            title = title,
            text = text,
            icon = "warning",
            showCancelButton = true,
            confirmButtonText = "OK",
            cancelButtonText = "Cancel"

        };
        var modalResult = await _jsRuntime.InvokeAsync<object>("Swal.fire", options);


        var result = JsonConvert.DeserializeObject<AlertResult>(modalResult.ToString());

        return result.isConfirmed ? true : false;

    }
}
