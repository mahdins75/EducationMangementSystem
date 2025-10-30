using Mstech.Entity.Etity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mstech.Accounting.Data;
using Mstech.ViewModel.DTO;
using ViewModel.Infrastructure;
using Implementation.BaseService;
using MStech.Wallet.DataBase.Etity.Client;
using DataBase.Repository;

namespace Implementation.AccessLinkeRoles
{
    public class AccessLinkRolesService : BaseService<RoleAccessedLink>
    {
        public readonly ApplicationDbContext applicationDbContext;
        public readonly IServiceProvider serviceProvider;

        public AccessLinkRolesService(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider,
            IUnitOfWork _unitOfWork, IRepository<WalletClient> _repository) : base(_unitOfWork)
        {
            this.applicationDbContext = applicationDbContext;
            this.serviceProvider = serviceProvider;
        }

        public async Task<ResponseViewModel<List<RoleAccessedLinkViewModel>>> GetAllAsync(
            RoleAccessedLinkViewModel model)
        {
            var query = this.GetAllAsIqueriable().Include(m => m.Role)
                .Where(x => !x.IsDeleted);

            var result = new ResponseViewModel<List<RoleAccessedLinkViewModel>>();
            if (model.Id > 0)
            {
                query = query.Where(m => m.Id == model.Id);
            }

            if (model.IsPagination)
            {
                result.Entity = query.Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    Role = m.Role != null
                        ? new RoleViewModel()
                        {
                            Id = m.Role.Id,
                            Name = m.Role.Name,
                            PersianName = m.Role.PersianName
                        }
                        : new RoleViewModel()
                }).Skip(model.Skip).Take(model.PageSize).ToList();
                result.QueryCount = query.Count();
                return result;
            }
            else
            {
                result.Entity = query.Select(m => new RoleAccessedLinkViewModel()
                {
                    Id = m.Id,
                    Role = m.Role != null
                        ? new RoleViewModel()
                        {
                            Id = m.Role.Id,
                            Name = m.Role.Name,
                            PersianName = m.Role.PersianName
                        }
                        : new RoleViewModel()
                }).ToList();
                result.QueryCount = query.Count();
                return result;
            }
        }

        public async Task<bool> CheckRoleHasUrl(string url, List<string> roles)
        {
            try
            {
                if (!roles.Any())
                {
                    return false;
                }

                if (roles.Where(m => m.ToLower() == "admin").Any()/*||roles.Where(m => m.ToLower() == "user").Any()*/)
                {
                    return true;
                }

                if (url == "/")
                {
                    return true;
                }

                //if (url == "/api/Common/Common/GetNav")
                //{
                //    return true;
                //}

                if (url == "/api/Common/Common/CheckAuthentication" || url == "/api/Admin/Membership/CreateConfirmationCodeForPhoneNumber")
                {
                    return true;
                }

                roles = roles.Select(x => x.ToLower()).ToList();

                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var allAccessedLinks = await db.Set<RoleAccessedLink>()
                        .Include(m => m.AccessedLink)
                        .Include(m => m.Role)
                        .Where(m => roles.Contains(m.Role.Name.ToLower()))
                        .ToListAsync();

                    if (!allAccessedLinks.Any())
                    {
                        return false;
                    }

                    string areaName = url.Split("/")[1].ToLower() + "/" + url.Split("/")[2].ToLower();
                    string controllerName = url.Split("/")[3].ToLower();
                    string actionName = url.Split("/")[4].ToLower();

                    var selectedLink = allAccessedLinks
                        .Any(m =>
                                  m.AccessedLink.Action.ToLower() == url.ToLower());

                    return selectedLink;

                    //foreach (var item in roles.Distinct())
                    //{
                    //    var asasd = selectedLink.RoleAccessedLinks.Where(m => m.Role.Name.ToLower() == item);
                    //    var dddf = selectedLink.RoleAccessedLinks;
                    //    if (selectedLink.RoleAccessedLinks.Where(m => m.Role.Name.ToLower() == item.ToLower()).Any())
                    //    {
                    //        return true;
                    //    }
                    //}
                    //return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<string>> GetLinks(List<string> roles)
        {
            var result = new List<string>();

            if (roles == null || !roles.Any())
            {
                return result;
            }

            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var accessLinks = db.Set<AccessedLink>().Include(m => m.RoleAccessedLinks).ThenInclude(m => m.Role)
                        .ToList();
                    var selectedLink = accessLinks.Where(m =>
                        m.RoleAccessedLinks.Where(s => roles.Where(r => r == s.Role.Name).Any()).Any() &&
                        m.ParentId != null).ToList();
                    result = selectedLink.Select(m => "/" + m.Area + "/" + m.Controller + "/" + m.Action).ToList();
                    return result;
                }
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}