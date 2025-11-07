using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class InstitutionViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "نام موسسه")]
    public string Name { get; set; }
    
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    
    [Display(Name = "آدرس")]
    public string? Address { get; set; }
    
    [Display(Name = "تلفن")]
    public string? Phone { get; set; }
    
    [Display(Name = "ایمیل")]
    public string? Email { get; set; }
    
    [Display(Name = "وب سایت")]
    public string? Website { get; set; }
    
    [Display(Name = "وضعیت")]
    public bool IsActive { get; set; }
    
    [Display(Name = "شناسه مالک")]
    public string? OwnerId { get; set; }
    
    [Display(Name = "نام کامل مالک")]
    public string? OwnerFullName { get; set; }
    
    [Display(Name = "نام کاربری مالک")]
    public string? OwnerUserName { get; set; }
    
    [Display(Name = "تعداد کلاس ها")]
    public int ClassesCount { get; set; }
    
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
    
    [Display(Name = "جستجو بر اساس ایمیل")]
    public string? SearchEmail { get; set; }
    
    [Display(Name = "فیلتر وضعیت")]
    public bool? IsActiveFilter { get; set; }
}