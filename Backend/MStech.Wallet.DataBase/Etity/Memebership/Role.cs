using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }
        public string PersianName { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public Role? Parent { get; set; }
        public ICollection<RoleUser> RoleUsers { get; set; }
        public ICollection<Role>? Children { get; set; }
        public ICollection<RoleAccessedLink> RoleAccessedLinks { get; set; }

    }
}