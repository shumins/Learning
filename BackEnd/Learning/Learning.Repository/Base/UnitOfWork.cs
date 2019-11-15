using Learning.Data;
using Learning.IRepository;
using Learning.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Repository.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private EFCoreContext _context;
        public UnitOfWork(EFCoreContext eFCoreContext)
        {
            _context = eFCoreContext;
        }


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            else
            {
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// 启动标识
        /// </summary>
        public bool IsStart
        {
            get;
            set;
        }



        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            IsStart = true;
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        public int Save()
        {

            try
            {
                return _context.SaveChanges();
            }
            finally
            {
                IsStart = false;
            }
        }
        /// <summary>
        /// 提交更新（异步）
        /// </summary>
        public async Task<int> SaveAsync()
        {

            try
            {
                return await _context.SaveChangesAsync();
            }
            finally
            {
                IsStart = false;
            }
        }

        /// <summary>
        /// 通过启动标识执行提交，如果已启动，则不提交
        /// </summary>
        public int SaveByStart()
        {
            if (IsStart)
                return 0;
            return _context.SaveChanges();
        }

        public async Task<int> SaveByStartAsync()
        {
            if (IsStart)
                return 0;
            return await _context.SaveChangesAsync();
        }



    }
}
