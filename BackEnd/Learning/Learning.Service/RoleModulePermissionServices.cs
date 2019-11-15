using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Service
{
   public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {
        IRoleModulePermissionRepository _dal;
        public RoleModulePermissionServices(IRoleModulePermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
