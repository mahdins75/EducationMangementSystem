using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Etity.FileManager
{
    public class FileStorage : BaseEntity<int>
    {
        public string FileName { get; set; }
        public string? FilePhysicalurl { get; set; }

        public byte[] FileData { get; set; }
        public string? Extension { get; set; }
        public string? EntityName { get; set; }
        public string? PropertyName { get; set; }
        public string? EntityId { get; set; }
    }
}
