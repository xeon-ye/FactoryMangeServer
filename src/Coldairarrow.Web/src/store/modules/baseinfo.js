import Vue from 'vue'
import { Axios } from '@/utils/plugin/axios-plugin'

const baseinfo = {
  state: {
    Process: [],
    Needle: [],
    ProofType: [],
    Size: []
  },
  mutations: {
    Process: (state, value) => {
      state.Process = value;
    },
    Needle: (state, value) => {
      state.Needle = value;
    },
    ProofType: (state, value) => {
      state.ProofType = value;
    },
    Size: (state, value) => {
      state.Size = value;
    },

  },
  actions: {
    async GetProcess(context) {
      let re = await Axios.post('/BaseInfo/BaseInfo_Process/GetAllDataList')
      context.commit('Process', re.Data);
      // console.log(re.Data)
      return re;
    },
    async GetNeedle(context) {
      let re = await Axios.post('/BaseInfo/BaseInfo_Needle/GetAllDataList')
      context.commit('Needle', re.Data);
      //  console.log(re.Data)

      return re;
    },
    async GetProofType(context) {
      let re = await Axios.post('/BaseInfo/BaseInfo_ProofType/GetAllDataList')
      context.commit('ProofType', re.Data);
      //  console.log(re.Data)

      return re;
    },
    async GetSize(context) {
      let re = await Axios.post('/BaseInfo/BaseInfo_Size/GetAllDataList')
      context.commit('Size', re.Data);
      //  console.log(re.Data)

      return re;
    },
    async GetAllBaseInfo(context) {
      context.dispatch('GetProcess')
      context.dispatch('GetNeedle')
      context.dispatch('GetProofType')
      context.dispatch('GetSize')
    }


  },
  getters: {}
}

export default baseinfo
