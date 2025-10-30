using Entity.Base;
using Microsoft.AspNetCore.Http;

namespace Mstech.ViewModel.DTO
{
    public class FileStorageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? FilePhysicalurl { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string EntityId { get; set; }
        public IFormFile IFormFile { get; set; }
        public byte[] FileData { get; set; }
        public string Extension { get; set; }
        public string Base64 { get; set; }
    }

    public class CreateFileViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? FilePhysicalurl { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string EntityId { get; set; }
        public byte[] FileData { get; set; }
        public string Extension => System.IO.Path.GetExtension(FileName);
    }
}