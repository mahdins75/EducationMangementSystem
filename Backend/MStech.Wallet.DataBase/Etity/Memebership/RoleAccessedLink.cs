using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class RoleAccessedLink : BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int AccessedLinkId { get; set; }

        public Role Role { get; set; }
        public AccessedLink AccessedLink { get; set; }

    }
}