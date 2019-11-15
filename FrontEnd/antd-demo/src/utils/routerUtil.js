import { getNavigationBar } from '@/api/login'
import { UserLayout, BasicLayout, RouteView, BlankLayout, PageView } from '@/layouts'
const _import = require("@/router/_import_" + process.env.NODE_ENV); //获取组件的方法
// 前端路由表
const constantRouterComponents = {
	// 基础页面 layout 必须引入
	BasicLayout: BasicLayout,
	BlankLayout: BlankLayout,
	RouteView: RouteView,
	PageView: PageView,

	// 你需要动态引入的页面组件
	analysis: () => import('@/views/dashboard/Analysis'),
	workplace: () => import('@/views/dashboard/Workplace'),
	monitor: () => import('@/views/dashboard/Monitor'),
	userList: () => import('@/views/system/userList')
	// ...more
}

// 前端未找到页面路由（固定不用改）
const notFoundRouter = {
	path: '*', redirect: '/404', hidden: true
}
const servicePermissionMap = {
	"message": "",
	"result": [
		{
			"title": "首页",
			"key": "",
			"name": "index",
			"component": "BasicLayout",
			"redirect": "/dashboard/workplace",
			"children": [
				{
					"title": "仪表盘",
					"key": "dashboard",
					"component": "RouteView",
					"icon": "dashboard",
					"children": [
						{
							"title": "分析页",
							"key": "analysis",
							"icon": ""
						},
						{
							"title": "监控页",
							"key": "monitor",
							"icon": ""
						},
						{
							"title": "工作台",
							"key": "workplace",
							"icon": ""
						}
					]
				},
				{
					"title": "系统管理",
					"key": "system",
					"component": "PageView",
					"icon": "setting",
					"children": [
						{
							"title": "用户管理",
							"key": "userList"
						},
						{
							"title": "角色管理",
							"key": "roleList"
						},
						{
							"title": "权限管理",
							"key": "tableList"
						}
					]
				}
			]
		}
	],
	"status": 200,
	"timestamp": 1534844188679
}

export const get = (component, key) => {
	if (component) {
		return servicePermissionMap[component]
	}
	if (key) {
		return servicePermissionMap[key]


	}

}

/**
 * 获取路由菜单信息
 *
 * 1. 调用 getRouterByUser() 访问后端接口获得路由结构数组
 *    @see https://github.com/sendya/ant-design-pro-vue/blob/feature/dynamic-menu/public/dynamic-menu.json
 * 2. 调用
 * @returns {Promise<any>}
 */
export const generatorDynamicRouter = (data) => {
	return new Promise((resolve, reject) => {
		getNavigationBar(data).then(res => {
			const result = res.data
			//const result = servicePermissionMap.result
			const routers = generator(result)
			routers.push(notFoundRouter)
			resolve(routers)
		}).catch(err => {
			reject(err)
		})
	})
}

/**
 * 格式化 后端 结构信息并递归生成层级路由表
 *
 * @param routerMap
 * @param parent
 * @returns {*}
 */
export const generator = (routerMap, parent) => {

	return routerMap.map(item => {

		//let url = `${parent && parent.path || ''}/${item.key}`.replace('//', '/')
		const currentRouter = {
			// 路由地址 动态拼接生成如 /dashboard/workplace
			path: `${parent && parent.path || ''}/${item.key}`,
			// 路由名称，建议唯一
			name: item.key || '',
			// 该路由对应页面的 组件
			component: constantRouterComponents[item.component],
			// meta: 页面标题, 菜单图标, 页面权限(供指令权限用，可去掉)
			meta: { title: item.name, icon: item.icon || undefined, permission: item.key && [item.key] || null }
		}
		if (typeof (currentRouter.component) === "undefined") {
			//currentRouter.component=constantRouterComponents[item.key];	
			//	currentRouter.component=() => import('@/views/system/userList')
			//	currentRouter.component = () => import('@/views' + url)
			try {

				currentRouter.component = () => import('@/views' + `${parent && parent.path || ''}/${item.key}`.replace('//', '/'))
				//currentRouter.component = _import('@/views' + `${parent && parent.path || ''}/${item.key}`.replace('//', '/'))

			} catch (e) {
				console.info(
					"%c 当前路由 " +
					`${parent && parent.path || ''}/${item.key}`.replace('//', '/') +
					".vue 不存在，因此如法导入组件，请检查接口数据和组件是否匹配，并重新登录，清空缓存!",
					"color:red"
				);
			}
		}
		// 为了防止出现后端返回结果不规范，处理有可能出现拼接出两个 反斜杠
		currentRouter.path = currentRouter.path.replace('//', '/')
		// 重定向
		item.redirect && (currentRouter.redirect = item.redirect)
		// 是否有子菜单，并递归处理
		if (item.children && item.children.length > 0) {
			// Recursion
			currentRouter.children = generator(item.children, currentRouter)
		}
		return currentRouter
	})
}