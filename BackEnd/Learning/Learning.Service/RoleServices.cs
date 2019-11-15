using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Service
{
   public class RoleServices : BaseServices<Role>, IRoleServices
    {
        IRoleRepository _dal;
        public RoleServices(IRoleRepository dal) {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
