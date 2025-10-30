using System.ComponentModel.DataAnnotations;

namespace Mstech.ViewModel.DTO
{
    public class AccessedLinkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public bool IsInMenue { get; set; }
        public int? ParentId { get; set; }

        public AccessedLinkViewModel? Parent { get; set; }
        public ICollection<AccessedLinkViewModel> Children { get; set; }
        public ICollection<RoleAccessedLinkViewModel>? RoleAccessedLinks { get; set; }



    }

    public class AccessedLinkDataTableViewModel : JqueryDataTable
    {
        public AccessedLinkFilterViewModel Filter { get; set; }
        public List<AccessedLinkViewModel> Data { get; set; }

        public AccessedLinkDataTableViewModel()
        {
            Filter = new AccessedLinkFilterViewModel();
            Data = new List<AccessedLinkViewModel>();
        }
    }
    public class AccessedLinkFilterViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        //public SelectList Workflows { get; set; }
    }

}