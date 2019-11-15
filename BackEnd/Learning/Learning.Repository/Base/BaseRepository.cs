using Learning.Data;
using Learning.IRepository;
using Learning.IRepository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Learning.Repository.Base
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly EFCoreContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepository() { }
        public BaseRepository(EFCoreContext eFCoreContext, IUnitOfWork unitOfWork)
        {
            _dbContext = eFCoreContext;
            _dbSet = _dbContext.Set<T>();
            // _unitOfWork = new UnitOfWork(_dbContext); 此处会造成不同的UnitOfWork
            _unitOfWork = unitOfWork;
        }


        public bool Add(T entitiy)
        {
            _dbSet.Add(entitiy);
            //_dbContext.SaveChanges();
            return _unitOfWork.SaveByStart() > 0;
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _unitOfWork.SaveByStartAsync() > 0;
        }



        public bool AddRange(ICollection<T> entities)
        {
            _dbSet.AddRange(entities);
            return _unitOfWork.SaveByStart() > 0;
        }

        public async Task<bool> AddRangeAsync(ICollection<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool Delete(T key)
        {
            T entity = _dbSet.Find(key);
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync(T key)
        {
            T entity = await _dbSet.FindAsync(key);
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }


        public bool Delete(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).Delete() > 0;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).DeleteAsync() > 0;

        }






        public bool Update(T entity)
        {
             _dbSet.Update(entity);
            return _dbContext.SaveChanges() > 0;

        }

        public bool UpdateRange(ICollection<T> entities)
        {
            _dbSet.UpdateRange(entities);
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateBatch(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            _dbSet.Where(where).Update(updateExp);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        {
            await _dbSet.Where(where).UpdateAsync(updateFactory);
            return _dbContext.SaveChanges() > 0;
        }


       

        public T Get(object objId)
        {
            return _dbSet.Find(objId);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return  _dbSet.AsNoTracking().SingleOrDefault(where);
                
        }

        public IQueryable<T> Query()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return where == null ? _dbSet.Where(where).AsNoTracking() : _dbSet.AsNoTracking();
        }

        public IEnumerable<T> GetByPagination(Expression<Func<T, bool>> where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        {
            var filter = Query(where);
            if (orderby != null)
            {
                foreach (var func in orderby)
                {
                    filter = asc ? filter.OrderBy(func).AsQueryable() : filter.OrderByDescending(func).AsQueryable();
                }
            }
            return filter.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> Find()
        {
            return _dbSet;
        }
    }
}
