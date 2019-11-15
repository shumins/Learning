import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/'
import './core/use'

// mock
//import './mock'

import bootstrap from './core/bootstrap'
import './permission' // permission control
Vue.config.productionTip = false
console.log('123'+process.env.VUE_APP_BASEURL)
new Vue({
  router,
  store,
  created: bootstrap,
  render: h => h(App),
}).$mount('#app')
