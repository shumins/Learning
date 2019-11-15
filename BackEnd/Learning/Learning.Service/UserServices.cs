using Learning.Data.Models;
using Learning.IRepository;
using Learning.IService;
using Learning.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Learning.Data.Dtos.Identity;

namespace Learning.Service
{
   public class UserServices: BaseServices<User>, IUserServices
    {
        IUserRepository _dal;
        IUserRoleServices _userRoleServices;
        IRoleServices _roleServices;
        public UserServices(IUserRepository dal, IUserRoleServices userRoleServices, IRoleServices roleServices)
        {
            this._dal = dal;
            this._userRoleServices = userRoleServices;
            this._roleServices = roleServices;
            base.BaseDal = dal;
        }

        public OnlineUserDto GetUserInfo( string username)
        {
           var info= (from u in _dal.Find()
             join ur in _userRoleServices.Find() on u.Id equals ur.UserId
             where u.LoginName == username
             select new OnlineUserDto
             {
                 Id = u.Id.ToString(),
                 Email = u.Email,
                 HeadImg = u.HeadImg,
                 NickName = u.RealName,
                 UserName = u.LoginName,
                 Roles=(from ur in _userRoleServices.Find() 
                        join r in _roleServices.Find() on ur.RoleId equals r.Id
                        where ur.UserId == u.Id
                        select r.Name).ToArray()
             }).FirstOrDefault();
            return info;
        }
    }
}
