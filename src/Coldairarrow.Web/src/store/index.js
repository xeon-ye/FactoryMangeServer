import Vue from 'vue'
import Vuex from 'vuex'

import app from './modules/app'
import baseinfo from './modules/baseinfo'
// import user from './modules/user'
// import permission from './modules/permission'
import getters from './getters'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    app,
    baseinfo,
    // user
    // permission
  },
  state: {
    deptList: [],
    id: "001"

  },
  mutations: {

  },
  actions: {
  },
  getters
})
