// import Vue from 'vue'
// import Router from 'vue-router'
// import HelloWorld from '@/components/HelloWorld'

// Vue.use(Router)

// export default new Router({
//   routes: [{
//     path: '/',
//     name: 'HelloWorld',
//     component: () => import('@/components/HelloWorld')
//   }]
// })

import adminLayout from "../components/adminLayout/"

const routers = [{
  path: '/',
  meta: {
    title: '库存',
  },
  component: (resolve) => require(['../views/user/store'], resolve)
}, {
  path: '/login',
  name: 'login',
  meta: {
    title: '店铺登录'
  },
  component: (resolve) => require(['../views/admin/login'], resolve)
}, {
  path: '/adminStore',
  name: 'adminStore',
  component: adminLayout,
  meta: {},
  children: [{
    path: '/adminStore',
    name: 'adminStore',
    meta: {
      title: '库存'
    },
    component: (resolve) => require(['../views/admin/store'], resolve)
  },{
    path: '/adminOrder',
    name: 'adminOrder',
    meta: {
      title: '订单'
    },
    component: (resolve) => require(['../views/admin/order'], resolve)
  },{
    path: '/report',
    name: 'report',
    meta: {
      title: '报表'
    },
    component: (resolve) => require(['../views/admin/report'], resolve)
  }]
}]

export default routers
