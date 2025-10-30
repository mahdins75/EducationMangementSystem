using Entity.Etity.FileManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mstech.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mstech.Accounting.Data;

namespace Implementation.FileManager
{
    public class FileManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileManagerService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> AddFilePhysicaly(FileStorageViewModel model)
        {
            byte[] fileData;
            using (var memoryStream = new MemoryStream())
            {
                await model.IFormFile.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();
            }

            var file = new Entity.Etity.FileManager.FileStorage
            {
                FileName = model.FileName,
                FileData = fileData,
                Extension = model.Extension,
                EntityId = model.EntityId,
                EntityName = model.EntityName
            };
            if (file == null || model.IFormFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            if (!IsImage(model.IFormFile))
            {
                throw new ArgumentException("Invalid file format. Only image files are allowed.");
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

            string filePath = Path.Combine(folderPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.IFormFile.CopyTo(fileStream);
            }

            string relativePath = $"/{uniqueFileName}";
            file.FilePhysicalurl = relativePath;
            file.FileName = uniqueFileName;
            _context.Set<Entity.Etity.FileManager.FileStorage>().Add(file);
            return relativePath;
        }
        private bool IsImage(IFormFile file)
        {
            // Check if the file has a valid image extension
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
        public async Task<int> DeleteImage(string uniqueFileName, string EntityName)
        {
            if (string.IsNullOrEmpty(uniqueFileName))
            {
                throw new ArgumentException("Invalid unique filename");
            }
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

            string filePath = Path.Combine(folderPath, uniqueFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new FileNotFoundException("File not found", filePath);
            }
            var file = _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityName == EntityName && m.FileName == uniqueFileName).FirstOrDefault();
            if (file == null)
            {
                return 0;
            }

            _context.Set<Entity.Etity.FileManager.FileStorage>().Remove(file);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddFile(FileStorageViewModel model)
        {
            byte[] fileData;
            using (var memoryStream = new MemoryStream())
            {
                await model.IFormFile.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();
            }

            var file = new Entity.Etity.FileManager.FileStorage
            {
                FileName = model.FileName,
                FileData = fileData,
                Extension = model.Extension,
                EntityId = model.EntityId,
                EntityName = model.EntityName
            };

            _context.Set<Entity.Etity.FileManager.FileStorage>().Add(file);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFile(FileStorageViewModel model)
        {
            var file = _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityName == model.EntityName && m.EntityId == model.EntityId).FirstOrDefault();
            if (file == null)
            {
                return 0;
            }

            _context.Set<Entity.Etity.FileManager.FileStorage>().Remove(file);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteFileRange(FileStorageViewModel model)
        {
            var files = _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityName == model.EntityName && m.EntityId == model.EntityId).ToList();
            if (files == null)
            {
                return 0;
            }

            _context.Set<Entity.Etity.FileManager.FileStorage>().RemoveRange(files);
            return await _context.SaveChangesAsync();
        }
        public bool Any(FileStorageViewModel model)
        {
            return _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityId == model.EntityId && m.EntityName == model.EntityName).Any();
        }
        public FileStorageViewModel ReadFile(FileStorageViewModel rquest)
        {
            var file = _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityName == rquest.EntityName && (!string.IsNullOrEmpty(rquest.PropertyName) ? m.PropertyName == rquest.PropertyName : true) && m.EntityId == rquest.EntityId).FirstOrDefault();
            if (file == null)
            {
                return null;
            }

            var model = new FileStorageViewModel
            {
                Id = file.Id,
                FileName = file.FileName,
                Extension = file.Extension,
                IFormFile = null,
                FileData = file.FileData,
                Base64 = Convert.ToBase64String(file.FileData),
                EntityId = file.EntityId,
                EntityName = file.EntityName
            };

            return model;
        }
        public byte[] ReadFileAsByteArray(FileStorageViewModel rquest)
        {
            var file = _context.Set<Entity.Etity.FileManager.FileStorage>().Where(m => m.EntityName == rquest.EntityName && m.EntityId == rquest.EntityId).FirstOrDefault();
            if (file == null)
            {
                return null;
            }
            return file.FileData;
        }
    }
}
