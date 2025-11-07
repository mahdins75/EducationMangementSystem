using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class StudentActivitySubmissionViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "شناسه فعالیت")]
    public int ActivityId { get; set; }
    
    [Display(Name = "شناسه دانشجو")]
    public string? StudentId { get; set; }
    
    [Display(Name = "نام کامل دانشجو")]
    public string? StudentFullName { get; set; }
    
    [Display(Name = "نام کاربری دانشجو")]
    public string? StudentUserName { get; set; }
    
    [Display(Name = "محتوای تکلیف")]
    public string? SubmissionContent { get; set; }
    
    [Display(Name = "تاریخ تحویل")]
    public DateTime SubmissionDate { get; set; }
    
    [Display(Name = "نمره")]
    public int? Score { get; set; }
    
    [Display(Name = "توضیحات")]
    public string? Comments { get; set; }
    
    [Display(Name = "نمره داده شده")]
    public bool IsGraded { get; set; }
    
    /// <summary>
    /// pagination parameters
    /// </summary>
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
    
    /// <summary>
    /// search parameters
    /// </summary>
    [Display(Name = "جستجو بر اساس نام دانشجو")]
    public string? SearchStudentName { get; set; }
    
    [Display(Name = "فیلتر فعالیت")]
    public int? ActivityIdFilter { get; set; }
    
    [Display(Name = "فیلتر نمره داده شده")]
    public bool? IsGradedFilter { get; set; }
}