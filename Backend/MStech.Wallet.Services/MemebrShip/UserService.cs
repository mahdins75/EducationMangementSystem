using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using ViewModel.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Implementation.UserService
{
    public class UserService : BaseService<User>
    {
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork _unitOfWork, IRepository<User> _repository, UserManager<User> userManager) : base(_unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<UserDataTableViewModel> LoadDataTable(UserDataTableViewModel dataTable)
        {
            string orderBy = string.Empty;
            string orderDir = string.Empty;
            if (dataTable.Order.Any())
            {
                orderBy = dataTable.Columns[dataTable.Order[0].Column].Name;
                orderDir = dataTable.Order[0].Dir.ToLower();
            }

            var query = this.GetAllAsIqueriable()
                .Include(x => x.Position)
                .Include(m => m.RoleUsers).ThenInclude(m => m.Role)
                .Where(x => !x.IsDeleted);

            //dataTable.RecordsTotal = await query.LongCountAsync();
            query = SetFilter(query, dataTable.Filter);
            query = SetOrder(query, orderBy, orderDir == "asc");
            dataTable.RecordsTotal = dataTable.RecordsFiltered = await query.LongCountAsync();

            dataTable.Data = query
                .Skip(dataTable.Start)
                .Take(dataTable.Length)
                .ToList()
                .Select(x => new UserDataViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Phone = x.Phone,
                    IsActive = x.IsActive,
                    PositionName = x.Position != null ? x.Position.Name : "-",
                    RoleName = x.RoleUsers.Any(r => r.UserId == x.Id) ? x.RoleUsers.FirstOrDefault(r => r.UserId == x.Id).Role.PersianName : "",
                    RoleId = x.RoleUsers.Any(r => r.UserId == x.Id) ? x.RoleUsers.FirstOrDefault(r => r.UserId == x.Id).Role.Id : null
                }).ToList();

            return dataTable;
        }

        public async Task<Result<List<UserViewModel>>> GetAll(UserViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Position)
                .Where(x => !x.IsDeleted);

            var user = query.SingleOrDefault(x => x.UserName == model.UserName);
            if (!string.IsNullOrEmpty(model.Id))
            {
                query = query.Where(x => x.Id == model.Id);
            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(x => x.UserName == model.UserName);
            }
            var result = new Result<List<UserViewModel>>();
            if (model.IsPagination)
            {

                result.Entity = query.Select(m => new UserViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Name = m.Name,
                    LastName = m.LastName,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    IsActive = m.IsActive,
                    PositionName = m.Position != null ? m.Position.Name : "-",

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;

            }
            else
            {
                result.Entity = query.Select(m => new UserViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Name = m.Name,
                    LastName = m.LastName,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    IsActive = m.IsActive,
                    PositionName = m.Position != null ? m.Position.Name : "-",


                }).ToList();
                result.Success = true;
            }

            return result;
        }
        public async Task<Result<List<UserViewModel>>> GetAllOfClientsUser(UserViewModel model)
        {
            var query = this.GetAllAsIqueriable()
                .Where(x => !x.IsDeleted);

            if (model.WaleltClientId > 0)
            {
                query = query.Where(m => m.Wallets.Where(w => w.ClientId == model.WaleltClientId).Any());
            }

            var result = new Result<List<UserViewModel>>();

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new UserViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Name = m.Name,
                    LastName = m.LastName,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    IsActive = m.IsActive,
                    PositionName = m.Position != null ? m.Position.Name : "-",

                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new UserViewModel()
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Name = m.Name,
                    LastName = m.LastName,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    IsActive = m.IsActive,
                    PositionName = m.Position != null ? m.Position.Name : "-",

                }).ToList();
                result.Count = query.Count();
                result.Success = true;
                return result;
            }

        }
        public async Task<Result<User>> CreateUserAsync(CreateUserViewModel model)
        {
            User user = new User
            {
                UserName = model.UserName,
                Email = model.UserName,
                EmailConfirmed = true,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                LastName = model.LastName,
                FullName = model.Name + " " + model.LastName,
                PositionId = model.PositionId,
                IsActive = model.IsActive,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var item in result.Errors)
                {
                    errorMessage += item.Description;
                }
                return new Result<User>()
                {
                    Message = errorMessage,
                    Success = false
                };
            }

            return new Result<User>()
            {
                Message = "",
                Success = true
            };
        }

        public async Task<Result<User>> EditUserAsync(EditUserViewModel model)
        {
            var user = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);


            //user.UserName = model.UserName;
            //user.Email = model.UserName;
            //user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.FullName = model.Name + " " + model.LastName;
            user.PositionId = model.PositionId;
            user.IsActive = model.IsActive;
            user.IsDeleted = false;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var item in result.Errors)
                {
                    errorMessage += item.Description;
                }
                return new Result<User>()
                {
                    Message = errorMessage,
                    Success = false
                };
            }

            return new Result<User>()
            {
                Message = "",
                Success = true
            };
        }
        public async Task<Result<User>> EditUserProfileAsync(EditUserViewModel model)
        {
            var user = await this.GetAllAsIqueriable()
                .SingleOrDefaultAsync(x => x.Id == model.Id);


            //user.UserName = model.UserName;
            //user.Email = model.UserName;
            //user.PhoneNumber = model.PhoneNumber;
            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.FullName = model.Name + " " + model.LastName;
            user.PositionId = model.PositionId;
            user.IsActive = model.IsActive;
            user.IsDeleted = false;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {

                string errorMessage = string.Empty;
                foreach (var item in result.Errors)
                {
                    errorMessage += item.Description;
                }
                return new Result<User>()
                {
                    Message = errorMessage,
                    Success = false
                };
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, resetPassToken, model.Password);
                }
            }

            return new Result<User>()
            {
                Message = "",
                Success = true
            };
        }

        public async Task<User> FindAsync(string id)
        {
            var user = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }
        public async Task<User> FindByUserNameAsync(string userName)
        {
            var user = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.UserName == userName);
            return user;
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                var user = await this.GetAllAsIqueriable().SingleOrDefaultAsync(x => x.Id == id);

                user.IsDeleted = true;

                await _userManager.UpdateAsync(user);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> EditProfileAsync(EditProfileViewModel model)
        {
            var user = await FindAsync(model.Id);

            user.Name = model.Name;
            user.LastName = model.LastName;
            user.FullName = model.Name + " " + model.LastName;
            user.Email = model.Email;
            user.Phone = model.Phone;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return 0;
            }

            return 1;
        }

        public async Task<bool> IsEmailExist(string email, string? id = null)
        {
            var query = this.GetAllAsIqueriable();

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return await query.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsPhoneNumberExist(string phoneNumber, string? id = null)
        {
            var query = this.GetAllAsIqueriable();

            if (!string.IsNullOrEmpty(id))
                query = query.Where(x => x.Id != id);

            return await query.AnyAsync(x => x.PhoneNumber == phoneNumber);
        }




        #region Private
        private IQueryable<User> SetFilter(IQueryable<User> query, UserFilterViewModel filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                query = query.Where(r => r.FullName.Contains(filter.FullName));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            return query;
        }

        private IQueryable<User> SetOrder(IQueryable<User> query, string sortField, bool sortDir)
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
                    case "FullName":
                        query = sortDir ? query.OrderBy(r => r.FullName) : query.OrderByDescending(r => r.FullName);
                        break;
                    //case "RoleName":
                    //    query = sortDir ? query.OrderBy(r => r.RoleName) : query.OrderByDescending(r => r.RoleName);
                    //    break;
                    case "PositionName":
                        query = sortDir ? query.OrderBy(r => r.Position.Name) : query.OrderByDescending(r => r.Position.Name);
                        break;
                    case "IsActive":
                        query = sortDir ? query.OrderBy(r => r.IsActive) : query.OrderByDescending(r => r.IsActive);
                        break;
                }
            }

            return query;
        }

        #endregion
    }

}