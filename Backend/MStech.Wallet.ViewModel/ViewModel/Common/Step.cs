using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Mstech.ViewModel.DTO
{
    public class StepViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderBy { get; set; }
        public string StepTypeName { get; set; }
    }

    public class ManageStepViewModel
    {
        public ManageStepViewModel()
        {
            StepTypeList = new List<SelectListItem>();
        }
        public int? Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Name { get; set; }

        [Display(Name = "ترتیب")]
        [Required(ErrorMessage = "وارد کردن ترتیب اجباری است")]
        public int OrderBy { get; set; }

        [Display(Name = "نوع")]
        [Required(ErrorMessage = "وارد کردن نوع اجباری است")]
        public int StepTypeId { get; set; }

        public IEnumerable<SelectListItem> StepTypeList { get; set; }

    }

    public class StepDataTableViewModel : JqueryDataTable
    {
        public StepFilterViewModel Filter { get; set; }
        public List<StepViewModel> Data { get; set; }

        public StepDataTableViewModel()
        {
            Filter = new StepFilterViewModel();
            Data = new List<StepViewModel>();
        }
    }

    public class StepFilterViewModel
    {
        public StepFilterViewModel()
        {
            StepTypeList = new List<SelectListItem>();
        }
        public IEnumerable<SelectListItem> StepTypeList { get; set; }

    }
}