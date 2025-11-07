using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class UpdateStudentActivityViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "دانشجو")]
    public string? StudentId { get; set; }  // Changed from int to string to match the backend User Id type
    
    [Display(Name = "نام کامل دانشجو")]
    public string? StudentFullName { get; set; } = string.Empty;  // Added student full name field
    
    [Display(Name = "فعالیت")]
    public string? ActivityType { get; set; } = string.Empty;
    
    [Display(Name = "عنوان فعالیت")]
    public string? ActivityTitle { get; set; } = string.Empty;
    
    [Display(Name = "شرح فعالیت")]
    public string? Description { get; set; } = string.Empty;
    
    [Display(Name = "تاریخ فعالیت")]
    public DateTime ActivityDateTime { get; set; }
    
    [Display(Name = "نمره")]
    public decimal Score { get; set; }
    
    [Display(Name = "وضعیت")]
    public string? Status { get; set; } = string.Empty;
    
    [Display(Name = "کلاس")]
    public int ClassId { get; set; }
    
    [Display(Name = "توضیحات")]
    public string? AdditionalNotes { get; set; } = string.Empty;
}