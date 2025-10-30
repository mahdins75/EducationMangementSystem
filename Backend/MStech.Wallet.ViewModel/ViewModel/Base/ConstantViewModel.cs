using System.ComponentModel.DataAnnotations;

namespace Mstech.ViewModel.DTO
{
    public class ConstantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ManageConstantViewModel
    {
        public int? Id { get; set; }

        public string EntityName { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن عنوان اجباری است")]
        public string Name { get; set; }

    }

    public class ConstantDataTableViewModel : JqueryDataTable
    {
        public ConstantFilterViewModel Filter { get; set; }
        public List<ConstantViewModel> Data { get; set; }

        public ConstantDataTableViewModel()
        {
            Filter = new ConstantFilterViewModel();
            Data = new List<ConstantViewModel>();
        }
    }

    public class ConstantFilterViewModel
    {
        public string EntityName { get; set; }

        [Display(Name = "عنوان")]
        public string? Name { get; set; }
    }
}