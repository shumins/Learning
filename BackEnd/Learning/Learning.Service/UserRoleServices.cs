using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Service
{
   public class UserRoleServices : BaseServices<UserRole>, IUserRoleServices
    {
        IUserRoleRepository _dal;
        public UserRoleServices(IUserRoleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
