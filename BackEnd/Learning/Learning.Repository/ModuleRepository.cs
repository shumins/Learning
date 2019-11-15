using Learning.Data;
using Learning.Data.Models;
using Learning.IRepository;
using Learning.IRepository.Base;
using Learning.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Repository
{
   public class ModuleRepository: BaseRepository<Module>, IModuleRepository
    {
        private readonly EFCoreContext _dbContext;
        private readonly IUnitOfWork _uow;
        public ModuleRepository(EFCoreContext eFCoreContext, IUnitOfWork uow)
          : base(eFCoreContext, uow)
        {
            _dbContext = eFCoreContext;
            _uow = uow;
        }
    }
}
