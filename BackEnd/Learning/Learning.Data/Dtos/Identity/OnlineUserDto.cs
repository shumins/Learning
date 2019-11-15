using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.Data.Dtos.Identity
{
    public class OnlineUserDto
    {
        //
        // 摘要:
        //     获取或设置 用户编号
        public string Id { get; set; }
        //
        // 摘要:
        //     获取或设置 用户名
        public string UserName { get; set; }
        //
        // 摘要:
        //     获取或设置 用户昵称
        public string NickName { get; set; }
        //
        // 摘要:
        //     获取或设置 用户Email
        public string Email { get; set; }
        //
        // 摘要:
        //     获取或设置 用户头像
        public string HeadImg { get; set; }
        //
        // 摘要:
        //     获取或设置 是否管理
        public bool IsAdmin { get; set; }
        //
        // 摘要:
        //     获取或设置 用户角色
        public string[] Roles { get; set; }


        public int Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string Address { get; set; }
    }
}
