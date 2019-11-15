using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learning.IRepository.Base
{

    /// <summary>
    /// 定义实体仓储模型的数据标准操作
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IBaseRepository<T>
        where T : class
    {
        #region Insert

        /// <summary>
        /// 插入实体(同步)
        /// </summary>
        /// <param name="entitiy">实体对象集合</param>
        bool Add(T entitiy);

        Task<bool> AddAsync(T entity);

        /// <summary>
        /// 批量插入实体(同步)
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        bool AddRange(ICollection<T> entities);

        /// <summary>
        /// 批量插入实体(异步)
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        Task<bool> AddRangeAsync(ICollection<T> entities);



        #endregion

        #region Delete
        /// <summary>
        /// 删除指定编号的实体（同步）
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>操作影响的行数</returns>
        bool Delete(T key);
        /// <summary>
        /// 删除指定编号的实体（异步）
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>操作影响的行数</returns>
        Task<bool> DeleteAsync(T key);

        /// <summary>
        /// 删除所有符合特定条件的实体(同步)
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        bool Delete(Expression<Func<T, bool>> where);

        /// <summary>
        /// 删除所有符合特定条件的实体(异步)
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> where);
        #endregion

        #region Update
        bool Update(T entity);

        bool UpdateRange(ICollection<T> entities);


        bool UpdateBatch(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp);

        Task<bool> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);
        #endregion

        #region Query
        /// <summary>
        /// 查找实体
        /// </summary>
        IQueryable<T> Find();

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        T Get(object objId);

        /// <summary>
        /// 查找第一个符合条件的数据
        /// </summary>
        /// <param name="predicate">数据查询谓语表达式</param>
        /// <returns>符合条件的实体，不存在时返回null</returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取<typeparamref name="T"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <returns>符合条件的数据集</returns>
        IQueryable<T> Query();

        /// <summary>
        /// 获取<typeparamref name="T"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        /// <param name="where">数据查询谓语表达式</param>
        /// <returns>符合条件的数据集</returns>
        IQueryable<T> Query(Expression<Func<T, bool>> where);


        IEnumerable<T> GetByPagination(Expression<Func<T, bool>> where, int pageSize, int pageIndex, bool asc = true,
            params Func<T, object>[] orderby);

        #endregion
    }
}
