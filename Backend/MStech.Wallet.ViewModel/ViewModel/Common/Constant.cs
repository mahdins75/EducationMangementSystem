using Mstech.ViewModel.DTO;

namespace Mstech.Entity.Etity
{
    public class ConstantViewmodel
    {
        public string? Name { get; set; }
        public string? CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string? EntityName { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public ConstantViewmodel? Paernt { get; set; }
        public ICollection<UserViewModel> Users { get; set; }
    }
}