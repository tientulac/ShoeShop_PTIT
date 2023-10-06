using ShoeShopAPI.Models;
using System;
using System.Data.Linq;
using System.Linq;

namespace ShoeShopAPI.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly LinqDataContext _dbContext;

        public BaseRepository(LinqDataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public void Add(TEntity entity)
        {
            try
            {
                _dbContext.GetTable<TEntity>()
                    .InsertOnSubmit(entity);
                _dbContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.GetTable<TEntity>().AsQueryable();
        }

        public void Remove(TEntity entity)
        {
            try
            {
                _dbContext.GetTable<TEntity>().DeleteOnSubmit(entity);
                _dbContext.SubmitChanges();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _dbContext.SubmitChanges(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}