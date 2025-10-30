using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Implementation.CityService
{
    public class CityService : BaseService<City>
    {
        public CityService(IUnitOfWork _unitOfWork, IRepository<City> _repository) : base(_unitOfWork)
        {

        }

        public async Task<List<SelectListItem>> GetCityWhithProvinceListAsync()
        {
            var query = this.GetAllAsIqueriable()
                .Include(x => x.Province)
                .Where(x => x.ProvinceId.HasValue); //add isdeleted

            return await query.Select(x => new SelectListItem()
            {
                Text = x.Province.Name + " - " + x.Name,
                Value = x.Id.ToString(),
            }).ToListAsync();
        }
    }

}