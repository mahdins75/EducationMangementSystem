using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class InstitutionClassViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "نام")]
    public string Name { get; set; }
    
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    
    [Display(Name = "شناسه موسسه")]
    public int InstitutionId { get; set; }
    
    [Display(Name = "نام کلاس")]
    public string? ClassName { get; set; }
    
    [Display(Name = "کد کلاس")]
    public string? ClassCode { get; set; }
    
    [Display(Name = "حداکثر دانشجو")]
    public int MaxStudents { get; set; }
    
    [Display(Name = "تاریخ شروع")]
    public DateTime StartDate { get; set; }
    
    [Display(Name = "تاریخ پایان")]
    public DateTime EndDate { get; set; }
    
    [Display(Name = "وضعیت")]
    public bool IsActive { get; set; }
    
    [Display(Name = "نام موسسه")]
    public string? InstitutionName { get; set; }
    
    [Display(Name = "شناسه معلم")]
    public string? TeacherId { get; set; }
    
    [Display(Name = "نام کامل معلم")]
    public string? TeacherFullName { get; set; }
    
    [Display(Name = "نام کاربری معلم")]
    public string? TeacherUserName { get; set; }
    
    [Display(Name = "تعداد دانشجویان")]
    public int StudentsCount { get; set; }
    
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
    [Display(Name = "جستجو بر اساس نام")]
    public string? SearchName { get; set; }
    
    [Display(Name = "جستجو بر اساس کد کلاس")]
    public string? SearchClassCode { get; set; }
    
    [Display(Name = "فیلتر موسسه")]
    public int? InstitutionIdFilter { get; set; }
    
    [Display(Name = "فیلتر وضعیت")]
    public bool? IsActiveFilter { get; set; }
}