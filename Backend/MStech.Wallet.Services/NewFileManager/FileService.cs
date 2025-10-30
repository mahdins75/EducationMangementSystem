using DataBase.Repository;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using Entity.Etity.FileManager;
using Mstech.ViewModel.DTO;
using Common.Gaurd;

namespace Implementation.FileService
{
    public class FileService : BaseService<FileStorage>
    {
        public FileService(IUnitOfWork _unitOfWork, IRepository<FileStorage> _repository) : base(_unitOfWork)
        {

        }

        public async Task<int> AddAsync(CreateFileViewModel model)
        {
            if (model.FilePhysicalurl.IsNullOrEmpty()) // save db file
            {
                if (model.FileData == null || model.FileData.Length < 1)
                    return 0; //file data is empty

                var existFile = await GetFileAsync(model.EntityId, model.EntityName, model.PropertyName);

                if (existFile != null) //update mode
                {
                    existFile.FileName = model.FileName;
                    existFile.FileData = model.FileData;
                    existFile.Extension = model.Extension;
                    existFile.ModifyDate = DateTime.Now;

                    var result = this.Update(existFile);
                    return result.Id;
                }
                else //create mode
                {
                    var file = new FileStorage
                    {
                        FileName = model.FileName,
                        FileData = model.FileData,
                        Extension = model.Extension,
                        EntityId = model.EntityId,
                        EntityName = model.EntityName,
                        PropertyName = model.PropertyName,
                        CreateDate = DateTime.Now,
                    };

                    var result = this.Insert(file);
                    return result.Id;
                }
            }
            else // save physical file
            {
                return 1;
            }
        }

        #region Private
        public async Task<FileStorage> GetFileAsync(string entityId, string entityName, string propertyName)
        {
            var result = await this.GetAllAsIqueriable()
                .Where(x => x.EntityId == entityId)
                .Where(x => x.EntityName == entityName)
                .Where(x => x.PropertyName == propertyName)
                .SingleOrDefaultAsync();

            return result;
        }

        #endregion
    }


}