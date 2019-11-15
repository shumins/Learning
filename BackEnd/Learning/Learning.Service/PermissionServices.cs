using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Service
{
    public class PermissionServices: BaseServices<Permission>, IPermissionServices
    {
        IPermissionRepository _dal;
        public PermissionServices(IPermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
