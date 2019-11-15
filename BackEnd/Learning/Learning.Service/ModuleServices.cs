using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Service
{
    public class ModuleServices: BaseServices<Module>, IModuleServices
    {
        IModuleRepository _dal;
        public ModuleServices(IModuleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
