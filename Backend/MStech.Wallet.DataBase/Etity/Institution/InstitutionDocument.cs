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
    public class InstitutionDocument : BaseEntity<int>
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation properties
        public User? Owner { get; set; }
        public string? OwnerId { get; set; }

        public InstitutionClass  InstitutionClass { get; set; }
        public int  InstitutionClassId { get; set; }
    }
}