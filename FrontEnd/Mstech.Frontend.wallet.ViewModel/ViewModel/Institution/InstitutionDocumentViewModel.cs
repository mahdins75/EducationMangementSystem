using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO;

public class InstitutionDocumentViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "وضعیت فعالیت")]
    public bool IsActive { get; set; }
    
    [Display(Name = "مالک")]
    public string? OwnerId { get; set; }
    
    [Display(Name = "نام مالک")]
    public string? OwnerName { get; set; } = string.Empty;
    
    [Display(Name = "نام کاربری مالک")]
    public string? OwnerUserName { get; set; } = string.Empty;
    
    [Display(Name = "کلاس")]
    public int InstitutionClassId { get; set; }
    
    [Display(Name = "نام کلاس")]
    public string? InstitutionClassName { get; set; } = string.Empty;
    
    public bool IsPagination { get; set; } = false;
    public int PageSize { get; set; }
    public int Skip { get; set; }
    public int PageIndex { get; set; }
    public int TotalCount { get; set; }
}

public class CreateInstitutionDocumentViewModel
{
    [Display(Name = "وضعیت فعالیت")]
    public bool IsActive { get; set; } = true;
    
    [Display(Name = "مالک")]
    public string? OwnerId { get; set; }
    
    [Display(Name = "کلاس")]
    public int InstitutionClassId { get; set; }
}

public class UpdateInstitutionDocumentViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "وضعیت فعالیت")]
    public bool IsActive { get; set; }
    
    [Display(Name = "مالک")]
    public string? OwnerId { get; set; }
    
    [Display(Name = "کلاس")]
    public int InstitutionClassId { get; set; }
}