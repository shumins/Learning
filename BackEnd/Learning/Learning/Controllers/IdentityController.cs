using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Learning.Data.Dtos.Identity;
using Learning.IService;
using Learning.Provide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        public IdentityController(IUserServices userServices, IRoleServices roleServices)
        {
            _userServices = userServices;
            _roleServices = roleServices;
        }

        /// <summary>
        /// jwt登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("Jwtoken")]
        public BaseResponse Jwtoken(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                throw new Warning(1000);
            }
            
           
            var info = _userServices.Get(x => x.LoginName == username && x.LoginPwd == password);
            if (info != null)
            {
                var token = JwtHelper.GetToken(info.LoginName);
                return new SuccessResponse(new { token });
            }
            throw new Warning(1003);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Profile")]
        [Description("用户信息")]
        //[Authorize]
        public BaseResponse Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new ErrorResponse(10001, "数据为空");
            }
            var info= _userServices.GetUserInfo(User.Identity.Name);
            return new SuccessResponse<OnlineUserDto>(info);
        }
    }
}