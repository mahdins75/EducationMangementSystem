using Entity.Base;
using Mstech.Entity.Etity;
using MStech.Wallet.DataBase.Etity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStech.Wallet.DataBase.Etity.Institution
{
    public class Institution : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation properties
        public ICollection<InstitutionClass> Classes { get; set; }
        public User? Owner { get; set; }
        public string? OwnerId { get; set; }
    }
}