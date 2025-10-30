using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Implementation.AccessedLinkService;
using System.Data;
using DNTPersianUtils.Core;

namespace Implementation.RoleService
{
    public class RoleService : BaseService<Role>
    {

        private readonly RoleAccessedLinkService _roleLinkService;

        public RoleService(IUnitOfWork _unitOfWork, IRepository<Role> _repository, RoleAccessedLinkService roleLinkService) : base(_unitOfWork)
        {
            _roleLinkService = roleLinkService;
        }


        public async Task<ResponseViewModel<List<RoleViewModel>>> GetAllRoles(RoleViewModel model)
        {
            var result = new ResponseViewModel<List<RoleViewModel>>();
            string orderBy = string.Empty;
            string orderDir = string.Empty;

            var query = this.GetAllAsIqueriable()
                .Include(x => x.RoleUsers)
                .Include(x => x.RoleAccessedLinks)
                .ThenInclude(m => m.AccessedLink)
                .Where(x => 1 == 1);

            if (model.IsPagination)
            {
                result.Entity = query
               .Skip(model.Skip)
               .Take(model.PageSize)
               .ToList()
               .Select(x => new RoleViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   ParentId = x.ParentId,
                   PersianName = x.PersianName,
                   Description = x.Description,


               }).ToList();
                result.QueryCount = query.Count();
            }
            else
            {
                result.Entity = query
             .ToList().Select(x => new RoleViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 ParentId = x.ParentId,
                 PersianName = x.PersianName,
                 Description = x.Description,
             }).ToList();
            }
            return result;
        }

        public async Task<Role> GetRoleByIdAsync(int id)
            => await this.GetAllAsIqueriable()
                .Include(x => x.RoleAccessedLinks)
                .SingleOrDefaultAsync(x => x.Id == id);


        public async Task<ResponseViewModel<RoleViewModel>> CreateAsync(RoleViewModel model)
        {
            try
            {
                string validationMessage = await ValidationRole(model.Name, model.PersianName);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return new ResponseViewModel<RoleViewModel>()
                    {
                        Message = validationMessage,
                        IsSuccess = false
                    };
                }

                var role = new Role()
                {
                    PersianName = model.PersianName,
                    Name = model.Name,
                };

                var result = this.Insert(role);

                return new ResponseViewModel<RoleViewModel>()
                {
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResponseViewModel<RoleViewModel>()
                {
                    Message = "عملیات افزودن نقش با خطا مواجه شد.",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResponseViewModel<RoleViewModel>> EditAsync(RoleViewModel model)
        {
            try
            {
                string validationMessage = await ValidationRole(model.Name, model.PersianName, model.Id);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return new ResponseViewModel<RoleViewModel>()
                    {
                        Message = validationMessage,
                        IsSuccess = false
                    };
                }

                var role = await GetRoleByIdAsync(model.Id);

                role.Name = model.Name;
                role.PersianName = model.PersianName;

                var result = this.Update(role);

                //remove Old Link
                await _roleLinkService.HardDeleteByRoleIdAsync(result.Id);

                //Add Link
                await _roleLinkService.CreateByRoleIdAsync(result.Id, model.Links);

                return new ResponseViewModel<RoleViewModel>()
                {
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ResponseViewModel<RoleViewModel>()
                {
                    Message = "عملیات ویرایش نقش با خطا مواجه شد.",
                    IsSuccess = false
                };
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var role = await GetRoleByIdAsync(id);
                this.Delete(role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Private
        private IQueryable<Role> SetFilter(IQueryable<Role> query, RoleFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(r => r.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.PersianName))
            {
                query = query.Where(r => r.PersianName.Contains(filter.PersianName));
            }

            //if (filter.Status.HasValue)
            //{
            //    query = query.Where(x => x.Status == filter.Status.Value);
            //}

            return query;
        }

        private IQueryable<Role> SetOrder(IQueryable<Role> query, string sortField, bool sortDir)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                query = !sortDir ? query.OrderBy(r => r.Id) : query.OrderByDescending(r => r.Id);
            }
            else
            {
                switch (sortField)
                {
                    case "Id":
                        query = sortDir ? query.OrderBy(r => r.Id) : query.OrderByDescending(r => r.Id);
                        break;
                    case "PersianName":
                        query = sortDir ? query.OrderBy(r => r.PersianName) : query.OrderByDescending(r => r.PersianName);
                        break;
                    case "Name":
                        query = sortDir ? query.OrderBy(r => r.Name) : query.OrderByDescending(r => r.Name);
                        break;
                }
            }

            return query;
        }

        private async Task<string> ValidationRole(string name, string persianName, int? roleId = null)
        {
            var query = this.GetAllAsIqueriable();

            if (roleId.HasValue)
                query = query.Where(x => x.Id != roleId.Value);

            if (await query.AnyAsync(x => x.Name.Trim() == name.Trim()))
            {
                return "عنوان نقش تکراری است.";
            }

            if (await query.AnyAsync(x => x.PersianName.Trim() == persianName.Trim()))
            {
                return "نام فارسی نقش تکراری است.";
            }

            return string.Empty;
        }
        #endregion
    }
}

