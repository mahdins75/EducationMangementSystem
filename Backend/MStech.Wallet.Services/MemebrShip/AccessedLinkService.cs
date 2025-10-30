using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Implementation.AccessedLinkService
{
    public class AccessedLinkService : BaseService<AccessedLink>
    {
        public AccessedLinkService(IUnitOfWork _unitOfWork, IRepository<AccessedLinkViewModel> _repository) : base(_unitOfWork)
        {

        }

        public async Task<List<AccessedLinkViewModel>> GetAccessedLinkList()
        {
            var query = this.GetAllAsIqueriable().Include(m => m.RoleAccessedLinks).Where(m => !m.IsDeleted);

            return await query.Select(x => new AccessedLinkViewModel()
            {
                Action = x.Action,
                Id = x.Id,
                Controller = x.Controller,
                Area = x.Area,
                Title = x.Title,
                Icon = x.Icon,
                ParentId = x.ParentId,
                Order = x.Order,
                RoleAccessedLinks = x.RoleAccessedLinks.Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    AccessedLinkId = m.AccessedLinkId,
                    RoleId = m.RoleId,
                    Role = new RoleViewModel() { Id = m.Role.Id, Name = m.Role.Name }
                }).ToList()
            }).ToListAsync();
        }

        public async Task<AccessedLinkDataTableViewModel> LoadDataTable(AccessedLinkDataTableViewModel dataTable)
        {
            string orderBy = string.Empty;
            string orderDir = string.Empty;
            if (dataTable.Order.Any())
            {
                orderBy = dataTable.Columns[dataTable.Order[0].Column].Name;
                orderDir = dataTable.Order[0].Dir.ToLower();
            }

            var query = this.GetAllAsIqueriable();

            dataTable.RecordsTotal = await query.LongCountAsync();
            //query = SetFilter(query, dataTable.Filter);
            //query = SetOrder(query, orderBy, orderDir == "asc");
            dataTable.RecordsFiltered = await query.LongCountAsync();

            dataTable.Data = query
                .Skip(dataTable.Start)
                .Take(dataTable.Length)
                .ToList()
                .Select(x => new AccessedLinkViewModel
                {
                    Title = x.Title,
                    Action = x.Action,
                    Area = x.Area,
                    Icon = x.Icon,
                    Controller = x.Controller,
                    IsInMenue = x.IsInMenue
                }).ToList();

            return dataTable;
        }

        public async Task<ResponseViewModel<AccessedLinkViewModel>> GetmodelById(int id)
        {
            var accessedLink = this.GetAllAsIqueriable().Include(m => m.RoleAccessedLinks).ThenInclude(m => m.Role).FirstOrDefault(m => m.Id == id);

            return new ResponseViewModel<AccessedLinkViewModel>()
            {
                Entity = new AccessedLinkViewModel()
                {
                    Action = accessedLink.Action,
                    Id = accessedLink.Id,
                    Controller = accessedLink.Controller,
                    Area = accessedLink.Area,
                    Icon = accessedLink.Icon,
                    RoleAccessedLinks = accessedLink.RoleAccessedLinks.Select(m => new RoleAccessedLinkViewModel()
                    {
                        Id = m.Id,
                        AccessedLinkId = m.AccessedLinkId,
                        RoleId = m.RoleId,
                        Role = new RoleViewModel() { Id = m.Role.Id, Name = m.Role.Name }
                    }).ToList()
                },
                Message = "Success",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<AccessedLinkViewModel>> Insertmodel(AccessedLinkViewModel model)
        {
            var result = this.Insert(model.Adapt<AccessedLink>());
            return new ResponseViewModel<AccessedLinkViewModel>()
            {
                Entity = new AccessedLinkViewModel()
                {
                    Action = result.Action,
                    Id = result.Id,
                    Controller = result.Controller,
                    Area = result.Area,
                    Icon = result.Icon
                },
                Message = "Success",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<AccessedLinkViewModel>> Updatemodel(AccessedLinkViewModel model)
        {
            var editItem = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == model.Id);
            if (editItem == null)
            {
                return new ResponseViewModel<AccessedLinkViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            editItem.Action = model.Action;
            editItem.Controller = model.Controller;
            editItem.Area = model.Area;
            editItem.Icon = model.Icon;
            var result = this.Update(editItem);
            return new ResponseViewModel<AccessedLinkViewModel>()
            {
                Entity = new AccessedLinkViewModel()
                {
                    Action = editItem.Action,
                    Id = editItem.Id,
                    Controller = editItem.Controller,
                    Area = editItem.Area,
                    Icon = result.Icon
                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<AccessedLinkViewModel>> Deletemodel(int id)
        {
            var model = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return new ResponseViewModel<AccessedLinkViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            var result = this.Update(model);
            return new ResponseViewModel<AccessedLinkViewModel>()
            {
                Entity = new AccessedLinkViewModel()
                {
                    Action = model.Action,
                    Id = model.Id,
                    Controller = model.Controller,
                    Area = model.Area,
                    Icon = result.Icon

                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<AccessedLink> FindAsync(int id)
            => await this.GetAllAsIqueriable()
            .Include(m => m.Parent)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

}