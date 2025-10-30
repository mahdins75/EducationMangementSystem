using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class RoleUser : BaseEntity<int>
    {
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }

    }
}