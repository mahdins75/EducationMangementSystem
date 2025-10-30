using Microsoft.EntityFrameworkCore;
using Mstech.Accounting.Data;
using System.Reflection;

namespace DataBase.Repository
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void SaveChanges();
    }
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        List<TEntity> InsertRange(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public TEntity Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public List<TEntity> InsertRange(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            return entity;
        }
    }

}