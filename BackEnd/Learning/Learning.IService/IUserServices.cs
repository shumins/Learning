using Learning.Data.Dtos.Identity;
using Learning.Data.Models;
using Learning.IService.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.IService
{
   public interface IUserServices:IBaseServices<User>
    {
        OnlineUserDto GetUserInfo(string username);

    }
}
