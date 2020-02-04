// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import Routers from './router'
import Vant from 'vant';
import 'vant/lib/index.css'
import App from './App'

Vue.use(Vant)
Vue.use(VueRouter)
const router = new VueRouter({
  mode: 'history',
  routes: Routers
})
router.beforeEach((to, from, next) => {
  next();
})

router.afterEach((to, from, next) => {
  window.scrollTo(0, 0)
})


Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: {
    App
  },
  template: '<App/>'
})
