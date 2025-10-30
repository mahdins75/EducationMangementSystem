using DataBase.Repository;

namespace Implementation.BaseService
{
    public class BaseService<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _modelRepository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _modelRepository = _unitOfWork.GetRepository<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _modelRepository.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _modelRepository.GetAll();
        }
        public IQueryable<TEntity> GetAllAsIqueriable()
        {
            return _modelRepository.GetAll();
        }

        public TEntity Insert(TEntity model)
        {
            _modelRepository.Insert(model);
            _unitOfWork.SaveChanges();
            return model;
        }

        public List<TEntity> InsertRange(List<TEntity> model)
        {
            _modelRepository.InsertRange(model);
            _unitOfWork.SaveChanges();
            return model;
        }
        public TEntity Update(TEntity model)
        {
            _modelRepository.Update(model);
            _unitOfWork.SaveChanges();
            return model;
        }
       
        public void Delete(TEntity model)
        {
            _modelRepository.Delete(model);
            _unitOfWork.SaveChanges();
        }
        public bool DeleteWithResult(TEntity model)
        {
            _modelRepository.Delete(model);
            _unitOfWork.SaveChanges();
            return true;
        }
    }

}