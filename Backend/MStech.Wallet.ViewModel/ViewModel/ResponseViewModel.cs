namespace Mstech.ViewModel.DTO;

public class ResponseViewModel<Tkey>
{

    public Tkey Entity { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string ErrorCode { get; set; }
    public object ErrorMessageList { get; set; }
    public int QueryCount { get; set; }

}
