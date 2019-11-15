using Learning.IRepository;
using Learning.IRepository.Base;
using Learning.IService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Service.Base
{
    public class BaseServices<T> : IBaseServices<T> where T : class, new()
    {
        public IBaseRepository<T> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public bool Add(T entitiy)
        {
            return BaseDal.Add(entitiy);
        }

        public async Task<bool> AddAsync(T entity)
        {
            return await BaseDal.AddAsync(entity);
        }

        public bool AddRange(ICollection<T> entities)
        {
            return BaseDal.AddRange(entities);
        }

        public async Task<bool> AddRangeAsync(ICollection<T> entities)
        {
            return await BaseDal.AddRangeAsync(entities);
        }

        public bool Delete(T key)
        {
            return BaseDal.Delete(key);
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            return BaseDal.Delete(where);
        }

        public async Task<bool> DeleteAsync(T key)
        {
            return await BaseDal.DeleteAsync(key);
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            return await BaseDal.DeleteAsync(where);
        }

        public T Get(object objId)
        {
            return BaseDal.Get(objId);
        }

        public IEnumerable<T> GetByPagination(Expression<Func<T, bool>> where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        {
            return BaseDal.GetByPagination(where, pageSize, pageIndex, asc, orderby);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return BaseDal.Get(predicate);
        }

        public IQueryable<T> Query()
        {
            return BaseDal.Query();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return BaseDal.Query(where);
        }

       

        public bool Update(T entity)
        {
            return BaseDal.Update(entity);
        }

        public async Task<bool> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        {
            return await BaseDal.UpdateAsync(where, updateFactory);
        }

        public bool UpdateBatch(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            return BaseDal.UpdateBatch(where, updateExp);
        }

        public bool UpdateRange(ICollection<T> entities)
        {
            return BaseDal.UpdateRange(entities);
        }

        public IQueryable<T> Find()
        {
            return BaseDal.Find();
        }
    }
}
