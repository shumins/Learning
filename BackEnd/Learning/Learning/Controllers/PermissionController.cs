using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Learning.Common.Helper;
using Learning.Data.Models;
using Learning.IService;
using Learning.Provide;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IRoleServices _roleServices;
        public PermissionController(IUserServices userServices, IUserRoleServices userRoleServices, IRoleModulePermissionServices roleModulePermissionServices,
            IPermissionServices permissionServices,
            IRoleServices roleServices) {
            _userServices = userServices;
            _userRoleServices = userRoleServices;
            _roleModulePermissionServices = roleModulePermissionServices;
            _permissionServices = permissionServices;
            _roleServices = roleServices;
        }



        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNavigationBar")]
        [Description("菜单")]
        public BaseResponse GetNavigationBar(int uid)
        {
            if (uid > 0)
            {
                var roleId = _userRoleServices.Get(d => d.IsDeleted == false && d.UserId == uid)?.RoleId;
                if (roleId > 0)
                {
                    var pids = _roleModulePermissionServices.Query(d => d.IsDeleted == false && d.RoleId == roleId).Select(d => d.PermissionId).Distinct();
                    if (pids.Any())
                    {
                        var rolePermissionMoudles = _permissionServices.Query(d => pids.Contains(d.Id) && d.IsButton == false).OrderBy(c => c.OrderSort);
                        var permissionTrees = (from child in rolePermissionMoudles
                                               where child.IsDeleted == false
                                               orderby child.Id
                                               select new NavigationBar
                                               {
                                                   id = child.Id,
                                                   name = child.Name,
                                                   pid = (int)child.Pid,
                                                   order = child.OrderSort,
                                                   path = child.Code,
                                                   iconCls = child.Icon,
                                                   key=child.Key,
                                                   component=child.Component,
                                                   isbutton=child.IsButton,
                                                   meta = new NavigationBarMeta
                                                   {
                                                       requireAuth = true,
                                                       title = child.Name,
                                                       icon = child.Icon
                                                   }
                                               }).ToList();
                        NavigationBar rootRoot = new NavigationBar()
                        {
                            id = 0,
                            pid = 0,
                            order = 0,
                            name = "根节点",
                            path = "",
                            iconCls = "",
                            component= "BasicLayout",
                            key="",
                            meta = new NavigationBarMeta(),

                        };
                        permissionTrees = permissionTrees.OrderBy(d => d.order).ToList();

                        RecursionHelper.LoopNaviBarAppendChildren(permissionTrees, rootRoot);
                        List<NavigationBar> list = new List<NavigationBar>();
                        list.Add(rootRoot);
                        return new SuccessResponse(list);
                    }
                }
            }
            return new SuccessResponse();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("userList")]
        [Description("用户列表")]
        public BaseResponse UserList(int page = 1, string key = "")
        {
            int pageSize = 10;
            var pager = new Pager(page, pageSize);
            var query = _userServices.Find();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(t => t.RealName.Contains(key));
            }
            pager.TotalCount = query.Count();
            var data = query.OrderByDescending(x => x.Id).Skip(pager.PageSize * (pager.Page - 1)).Take(pager.PageSize).ToList();
            var allUserRoles=_userRoleServices.Query(d => d.IsDeleted == false).ToList();
            var allRoles= _roleServices.Query(d => d.IsDeleted == false).ToList();
            foreach (var item in data)
            {
                item.RID = (allUserRoles.FirstOrDefault(d => d.UserId == item.Id)?.RoleId).ToInt();
                item.RoleName = allRoles.FirstOrDefault(d => d.Id == item.RID)?.Name;
            }
            var rep = new SuccessListResponse<List<User>>(data, pager);
            return rep;
        }
     }
}