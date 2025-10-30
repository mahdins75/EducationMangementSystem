using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace Implementation.RoleUserService
{
    public class RoleUserService : BaseService<RoleUser>
    {
        public RoleUserService(IUnitOfWork _unitOfWork, IRepository<RoleUserViewModel> _repository) : base(_unitOfWork)
        {

        }

        public async Task<ResponseViewModel<List<string>>> GetAllUserRoles(string userId)
        {
            var userRoles = await this.GetAllAsIqueriable().Include(m => m.Role).Where(m => m.UserId == userId).Select(m => new RoleViewModel()
            {
                Id = m.Role.Id,
                Name = m.Role.Name,
                Children = m.Role.Children.Select(s => new RoleViewModel() { Id = s.Id, Name = s.Name }).ToList()
            }).ToListAsync();
            var roleNameList = new List<string>();
            foreach (var item in userRoles)
            {
                roleNameList.Add(item.Name);
                if (item.Children.Any())
                {
                    roleNameList.AddRange(GetUserRolesRecursive(item.Children));
                }
            }

            return new ResponseViewModel<List<string>>()
            {
                Entity = roleNameList,
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<List<RoleUserViewModel>>> GetAllUserRoleEntity(string userId)
        {
            var userRoles = await this.GetAllAsIqueriable().Include(m => m.Role).Where(m => m.UserId == userId).Select(m => new RoleUserViewModel()
            {
                Id = m.Id,
                UserId = m.UserId,
                RoleId = m.RoleId
            }).ToListAsync();

            return new ResponseViewModel<List<RoleUserViewModel>>()
            {
                Entity = userRoles,
                Message = "Success",
                IsSuccess = true
            };
        }

        public List<string> GetUserRolesRecursive(ICollection<RoleViewModel> model)
        {
            var result = new List<string>();
            foreach (var item in model)
            {
                result.Add(item.Name);
                if (item.Children.Any())
                {
                    result.AddRange(GetUserRolesRecursive(item.Children));
                }
            }
            return result;
        }

        public async Task<ResponseViewModel<RoleUserViewModel>> GetmodelById(int id)
        {
            var RoleUser = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == id);

            return new ResponseViewModel<RoleUserViewModel>()
            {
                Entity = new RoleUserViewModel()
                {
                    Id = RoleUser.Id,
                    UserId = RoleUser.UserId,
                    RoleId = RoleUser.RoleId
                },
                Message = "Success",
                IsSuccess = true
            };
        }


        public async Task<bool> InsertRoleToUser(UserRoleViewModel model)
        {
            try
            {
                var oldEnties = await this.GetAllAsIqueriable()
                    .Where(x => x.UserId == model.UserId)
                    .ToListAsync();

                foreach (var item in oldEnties)
                {
                    this.Delete(item);
                }

                var result = this.Insert(new RoleUser()
                {
                    RoleId = model.RoleIds.FirstOrDefault(),
                    UserId = model.UserId,             
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<ResponseViewModel<RoleUserViewModel>> Insertmodel(RoleUserViewModel model)
        {
            var result = this.Insert(model.Adapt<RoleUser>());
            return new ResponseViewModel<RoleUserViewModel>()
            {
                Entity = new RoleUserViewModel()
                {
                    Id = result.Id,
                    RoleId = result.RoleId,
                    UserId = result.UserId
                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<RoleUserViewModel>> Updatemodel(RoleUserViewModel model)
        {
            var editItem = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == model.Id);
            if (editItem == null)
            {
                return new ResponseViewModel<RoleUserViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            editItem.RoleId = model.RoleId;
            editItem.UserId = model.UserId;
            var result = this.Update(editItem);
            return new ResponseViewModel<RoleUserViewModel>()
            {
                Entity = new RoleUserViewModel()
                {
                    Id = editItem.Id,
                    RoleId = editItem.RoleId,
                    UserId = editItem.UserId
                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<RoleUserViewModel>> Deletemodel(int id)
        {
            var model = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return new ResponseViewModel<RoleUserViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            var result = this.Update(model);
            return new ResponseViewModel<RoleUserViewModel>()
            {
                Entity = new RoleUserViewModel()
                {
                    Id = model.Id,
                    RoleId = model.RoleId,
                    UserId = model.UserId
                },
                Message = "Success",
                IsSuccess = true
            };
        }
    }

}