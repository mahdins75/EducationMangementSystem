namespace Mstech.FrontEnd.Wallet.ViewModel;

public class ResponseViewModel<Tkey>
{

    public Tkey Entity { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string ErrorCode { get; set; }
    public object ErrorMEssageList { get; set; }
    public int QueryCount { get; set; }

}
