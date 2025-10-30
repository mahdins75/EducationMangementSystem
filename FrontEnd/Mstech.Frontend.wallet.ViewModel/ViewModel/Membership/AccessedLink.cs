using System.ComponentModel.DataAnnotations;

namespace Mstech.Frontend.Wallet.ViewModel.DTO
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
        public bool? IsChecked { get; set; }

        public AccessedLinkViewModel? Parent { get; set; }
        public ICollection<AccessedLinkViewModel> Children
        {
            get; set;

        }

        public class AccessedLinkFilterViewModel
        {
            [Display(Name = "Title")]
            public string Title { get; set; }
        }

    }
}