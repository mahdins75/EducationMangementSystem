using DataBase.Repository;
using Mstech.Entity.Etity;
using Implementation.BaseService;

namespace Implementation.ProvinceService
{
    public class ProvinceService : BaseService<Province>
    {
        public ProvinceService(IUnitOfWork _unitOfWork,IRepository<Province> _repository)  :base(_unitOfWork)
        {

        }
    }

}