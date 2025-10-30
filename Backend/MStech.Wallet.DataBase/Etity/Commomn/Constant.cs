using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class Constant : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string EntityName { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public Constant? Paernt { get; set; }

        public ICollection<User> Users { get; set; }

    }
}