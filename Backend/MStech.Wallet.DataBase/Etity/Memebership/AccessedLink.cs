using System.Reflection.Metadata;
using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class AccessedLink : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string? Icon { get; set; }
        public bool IsInMenue { get; set; }
        public int? ParentId { get; set; }
        public int Order { get; set; }

        public AccessedLink? Parent { get; set; }
        public ICollection<AccessedLink> Children { get; set; }
        public ICollection<RoleAccessedLink> RoleAccessedLinks { get; set; }

    }
}