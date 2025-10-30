using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Mstech.ViewModel.DTO;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Mstech.Wallet.ViewModel.DTO;
using System.ComponentModel.DataAnnotations;

namespace Implementation.AccessedLinkService
{
    public class RoleAccessedLinkService : BaseService<RoleAccessedLink>
    {
        private readonly UserManager<User> userManager;
        //private readonly RoleService.RoleService roleService;
        private readonly RoleUserService.RoleUserService roleUserService;
        public readonly AccessedLinkService accessedLinkService;
        public RoleAccessedLinkService(IUnitOfWork _unitOfWork, IRepository<RoleAccessedLinkViewModel> _repository, UserManager<User> userManager, RoleUserService.RoleUserService roleUserService, AccessedLinkService accessedLinkService) : base(_unitOfWork)
        {
            this.userManager = userManager;
            //this.roleService = roleService;
            this.roleUserService = roleUserService;
            this.accessedLinkService = accessedLinkService;
        }

        public async Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> GetAll(RoleAccessedLinkViewModel model)
        {
            var result = new ResponseViewModel<List<RoleAccessedLinkViewModel>>();

            var query = this.GetAll().AsQueryable();

            if (model.RoleId > 0)
            {
                query = query.Where(m => m.RoleId == model.RoleId);
            }
            if (model.AccessedLinkId > 0)
            {
                query = query.Where(m => m.AccessedLinkId == model.AccessedLinkId);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Include(m => m.AccessedLink).Include(m => m.Role).Skip(model.Skip).Take(model.PageSize).Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    AccessedLinkId = m.AccessedLinkId,
                    RoleId = m.RoleId,

                    Role = new RoleViewModel()
                    {
                        Id = m.Role.Id,
                        Name = m.Role.Name,
                        PersianName = m.Role.PersianName
                    }
                }).ToList();
                result.QueryCount = query.Count();
            }
            else
            {
                result.Entity = query.Include(m => m.AccessedLink).Include(m => m.Role).Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    AccessedLinkId = m.AccessedLinkId,
                    RoleId = m.RoleId,

                    Role = new RoleViewModel()
                    {
                        Id = m.Role.Id,
                        Name = m.Role.Name,
                        PersianName = m.Role.PersianName
                    }
                }).ToList();
            }

            return result;

        }

        public async Task<ResponseViewModel<List<AccessedLinkViewModel>>> GetAllMenueLinksForRole(RoleAccessedLinkViewModel model)
        {
            var query = this.GetAll().AsQueryable();

            if (model.RoleId > 0)
            {
                query = query.Where(m => m.RoleId == model.RoleId);
            }

            var result = query.Include(m => m.AccessedLink).Where(m => m.AccessedLink.IsInMenue).Select(m => new AccessedLinkViewModel()
            {
                Id = m.AccessedLinkId,
                Action = m.AccessedLink.Action,
                Controller = m.AccessedLink.Action,
                Area = m.AccessedLink.Area,

            }).ToList();
            return new ResponseViewModel<List<AccessedLinkViewModel>>()
            {
                Entity = result,
                Message = "Success",
                IsSuccess = true
            };

        }

        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> GetmodelById(int id)
        {
            var accessedLink = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == id);

            return new ResponseViewModel<RoleAccessedLinkViewModel>()
            {
                Entity = new RoleAccessedLinkViewModel()
                {
                    Id = accessedLink.Id,
                    AccessedLinkId = accessedLink.AccessedLinkId,
                    RoleId = accessedLink.RoleId
                },
                Message = "Success",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> Insertmodel(RoleAccessedLinkViewModel model)
        {
            var result = this.Insert(model.Adapt<RoleAccessedLink>());
            return new ResponseViewModel<RoleAccessedLinkViewModel>()
            {
                Entity = new RoleAccessedLinkViewModel()
                {
                    Id = result.Id,
                    AccessedLinkId = result.AccessedLinkId,
                    RoleId = result.RoleId

                },
                Message = "Success",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> InsertRangeModel(List<RoleAccessedLinkViewModel> model)
        {
            var result = this.InsertRange(model.Adapt<List<RoleAccessedLink>>());
            return new ResponseViewModel<List<RoleAccessedLinkViewModel>>()
            {
                Entity = result.Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    AccessedLinkId = m.AccessedLinkId,
                    RoleId = m.RoleId

                }).ToList(),
                Message = "ویرایش لینک های سطح دسترسی با موفقیت انجام شد",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> Updatemodel(RoleAccessedLinkViewModel model)
        {
            var editItem = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == model.Id);
            if (editItem == null)
            {
                return new ResponseViewModel<RoleAccessedLinkViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            editItem.AccessedLinkId = model.AccessedLinkId;
            editItem.RoleId = model.RoleId;
            var result = this.Update(editItem);
            return new ResponseViewModel<RoleAccessedLinkViewModel>()
            {
                Entity = new RoleAccessedLinkViewModel()
                {
                    Id = editItem.Id,
                    AccessedLinkId = editItem.AccessedLinkId,
                    RoleId = editItem.RoleId
                },
                Message = "Success",
                IsSuccess = true
            };
        }
        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> Deletemodel(RoleAccessedLinkViewModel model)
        {
            var deleteItem = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == model.Id);
            if (deleteItem == null)
            {
                return new ResponseViewModel<RoleAccessedLinkViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            var result = this.Update(deleteItem);
            return new ResponseViewModel<RoleAccessedLinkViewModel>()
            {
                Entity = new RoleAccessedLinkViewModel()
                {
                    Id = deleteItem.Id,
                    AccessedLinkId = deleteItem.AccessedLinkId,
                    RoleId = deleteItem.RoleId
                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<ResponseViewModel<RoleAccessedLinkViewModel>> HardDeletemodel(RoleAccessedLinkViewModel model)
        {
            var deleteItem = this.GetAllAsIqueriable().FirstOrDefault(m => m.Id == model.Id);
            if (deleteItem == null)
            {
                return new ResponseViewModel<RoleAccessedLinkViewModel>()
                {
                    Entity = null,
                    Message = "Not Found",
                    IsSuccess = false,
                    ErrorCode = "404"
                };
            }
            this.Delete(deleteItem);
            return new ResponseViewModel<RoleAccessedLinkViewModel>()
            {
                Entity = new RoleAccessedLinkViewModel()
                {
                    Id = deleteItem.Id,
                    AccessedLinkId = deleteItem.AccessedLinkId,
                    RoleId = deleteItem.RoleId
                },
                Message = "Success",
                IsSuccess = true
            };
        }

        public async Task<bool> HardDeleteByRoleIdAsync(int roleId)
        {
            var roleLinks = await this.GetAllAsIqueriable().Where(m => m.RoleId == roleId).ToListAsync();

            if (roleLinks.Any())
            {
                foreach (var rl in roleLinks)
                {
                    this.Delete(rl);
                }
            }

            return true;
        }

        public async Task<bool> CreateByRoleIdAsync(int roleId, List<int> linkIds)
        {
            if (linkIds.Any())
            {
                var model = new List<RoleAccessedLink>();
                foreach (var id in linkIds)
                {
                    var linksInfo = await accessedLinkService.FindAsync(id);
                    if (linksInfo.ParentId.HasValue && linksInfo.Parent.IsInMenue)
                    {
                        bool existParent = this.GetAllAsIqueriable().Any(x => x.AccessedLinkId == linksInfo.ParentId.Value);
                        //Add ParentMenu 
                        if (!model.Any(x => x.AccessedLinkId == linksInfo.ParentId.Value) && !existParent)
                        {
                            model.Add(new RoleAccessedLink()
                            {
                                RoleId = roleId,
                                AccessedLinkId = linksInfo.ParentId.Value,
                            });
                        }

                    }
                    model.Add(new RoleAccessedLink()
                    {
                        RoleId = roleId,
                        AccessedLinkId = id,
                    });
                }

                this.InsertRange(model);
            }

            return true;
        }

        public async Task<List<NavItemViewModel>> GetMenuItemsInHTML(string userName, List<string> role)
        {
            var result = new List<NavItemViewModel>();
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return result;
            }
            var userRoles = await this.roleUserService.GetAllAsIqueriable()
                .Include(m => m.Role).ThenInclude(m => m.RoleAccessedLinks).ThenInclude(m => m.AccessedLink)
                .Where(m => m.UserId == user.Id).ToListAsync();

            var allAccessLinks = this.accessedLinkService
                .GetAllAsIqueriable()
                .Include(x => x.Children)
                .Where(m => m.IsInMenue && !m.IsDeleted)
                .OrderBy(m => m.Order)
                .ToList();

            //if (role.Where(m => m.ToLower() == "admin").Any())
            //{
            foreach (var link in allAccessLinks.Where(x => x.ParentId == null))
            {
                var temp = new NavItemViewModel();
                if (userRoles.Any(m => m.Role.Name.ToLower() == "admin" || m.Role.RoleAccessedLinks.Any(r => r.AccessedLinkId == link.Id)))
                {
                    string href = "";
                    if (string.IsNullOrWhiteSpace(link.Area) && string.IsNullOrWhiteSpace(link.Controller) && string.IsNullOrWhiteSpace(link.Action))
                    {
                        href = "#";
                    }
                    else
                    {

                        if (!string.IsNullOrEmpty(link.Action))
                        {
                            href += link.Action;
                        }
                    }
                    temp.Href = href;
                    temp.Text = link.Title;
                    temp.IconClass = link.Icon;
                    temp.SubItems = new List<NavItemViewModel>();
                    if (link.Children.Any(x => x.IsInMenue))
                    {

                        foreach (var child in link.Children.Where(x => x.IsInMenue))
                        {
                            if (userRoles.Any(m => m.Role.Name.ToLower() == "admin" || m.Role.RoleAccessedLinks.Any(r => r.AccessedLinkId == child.Id)))
                            {
                                var childHref = "";

                                if (string.IsNullOrWhiteSpace(child.Area) && string.IsNullOrWhiteSpace(child.Controller) && string.IsNullOrWhiteSpace(child.Action))
                                {
                                    childHref = "#";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(child.Action))
                                    {
                                        childHref += "/" + child.Action;
                                    }
                                }

                                temp.SubItems.Add(new NavItemViewModel() { Href = childHref, Text = child.Title, IconClass = child.Icon });
                            }
                        }
                    }
                    else
                    {
                        temp.Href = href;
                        temp.Text = link.Title;
                        temp.IconClass = link.Icon;
                        temp.SubItems = new List<NavItemViewModel>();
                    }
                    result.Add(temp);
                }
            }
            return result;

        }

    }

}