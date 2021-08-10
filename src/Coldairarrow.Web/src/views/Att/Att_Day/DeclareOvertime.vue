<template>
  <a-modal :title="title" width="1100px" :visible="visible" :confirmLoading="loading" okText="保存" @ok="handleSubmit" @cancel="()=>{this.visible=false}">

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="6" :sm="24">
            <a-form-model-item label="加班日期">
              <a-date-picker v-model="declareDate" format="YYYY-MM-DD" />
            </a-form-model-item>

          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="loadAtt()">载入考勤数据</a-button>
          </a-col>
        </a-row>
        <a-row>
          <a-col :md="5" :sm="24">
            <a-form-model-item label="开始时间">
              <a-time-picker v-model="beginTime" @change="timeChange" :minute-step="15" format="HH:mm" />
            </a-form-model-item>
          </a-col>

          <a-col :md="5" :sm="24">
            <a-form-model-item label="结束时间">
              <a-time-picker v-model="endTime" @change="timeChange" :minute-step="15" format="HH:mm" />
            </a-form-model-item>
          </a-col>

          <a-col :md="4" :sm="24">
            <a-form-model-item label="加班时长">
              <a-input-number id="inputNumber" v-model="overTime" :step="0.1" :min="0" :max="100" />
            </a-form-model-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="setTime()" :disabled="!hasSelected()">设定加班信息</a-button>
          </a-col>

        </a-row>
        <a-table ref="table" :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :columns="columns" :scroll="{ y: 400 }" :rowKey="row => row.StaffId" :dataSource="data" :pagination="pagination"
          :bordered="true" size="small">

          <span slot="action" slot-scope="text, record">
            <template>
              <a @click="handleDelete(record.StaffId)">清除加班</a>
            </template>
          </span>
        </a-table>
      </a-form>
    </div>
    <a-spin :spinning="loading"></a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment'
const columns = [
  { title: '日期', dataIndex: 'dstr', width: '7%' },
  { title: '姓名', dataIndex: 'Name', width: '10%' },
  { title: '打卡记录', dataIndex: 'AttRecord', width: '25%' },
  { title: '考勤加班', dataIndex: 'OverTime', width: '10%' },

  {
    title: '加班申报',
    children: [
      { title: '开始', dataIndex: 'BeginOverTimeStr', width: '10%' },
      { title: '结束', dataIndex: 'EndOverTimeStr', width: '10%' },
      { title: '时长', dataIndex: 'DeclareOverTime', width: '10%' },
    ]
  },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      pagination: {
        pageSize: 30,
      },
      data: [],
      defaultTime: {},
      defaultTime2: {},
      overTime: 2,
      beginTime: {},
      endTime: {},
      declareDate: '',
      visible: false,
      loading: false,
      title: '',
      deptId: '',
      columns,
      selectedRowKeys: [],
      deptName:'',
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.beginTime = moment('18:00', 'HH:mm');
      this.endTime = moment('20:00', 'HH:mm');
    },

    timeChange() {
      let re = this.endTime.diff(this.beginTime, 'minute') / 60
      if (re < 0) this.endTime.add(1, 'd')
      if (re > 24) this.endTime.add(-1, 'd')
      console.log("begin:", this.beginTime, "end:", this.endTime)
      let t = this.endTime.diff(this.beginTime, 'minute') / 60
      if (t < 0) this.overTime = 0
      else this.overTime = t
    },
    handleDelete(k) {
      let cp = this.data.find(p => p.StaffId == k);
      cp.BeginOverTime = null
      cp.EndOverTime = null
      cp.DeclareOverTime = null
      cp.BeginOverTimeStr = ''
      cp.EndOverTimeStr = ''
    },
    setTime() {
      console.log(this.beginTime)
      this.selectedRowKeys.forEach(p => {
        let cp = this.data.find(k => k.StaffId == p);

        cp.BeginOverTimeStr = this.beginTime.format('DD.HH:mm')
        cp.EndOverTimeStr = this.endTime.format('DD.HH:mm')
        cp.BeginOverTime = this.beginTime
        cp.EndOverTime = this.endTime
        cp.DeclareOverTime = this.overTime
      });
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    openForm(item) {
      this.deptName=item.label
      this.title = item.label + "-" + '加班申报'
      this.deptId = item.value
      this.declareDate = moment()
      this.data = []
      this.init()

    },
    async loadAtt() {
      this.selectedRowKeys = []
      console.log('载入考勤')
      let re = await this.$http.post('/Att/Att_Day/GetDeptAttList', { deptId: this.deptId, declareDate: this.declareDate });
      console.log(re);
      re.Data.forEach(p => {
        p.dstr = moment(p.AttDate).format('MM-DD');
        p.BeginOverTimeStr = ''
        p.EndOverTimeStr = ''
      });
      this.data = re.Data;

    },
    async handleSubmit() {

      let dlist = []
      this.data.forEach(p => {
        if (p.DeclareOverTime != null && p.DeclareOverTime != "") {
          dlist.push({
            StaffId: p.StaffId,
            BeginOverTime: p.BeginOverTime,
            EndOverTime: p.EndOverTime,
            DeclareOverTime: p.DeclareOverTime,
            AttDate: p.AttDate,
            AttRecord: p.AttRecord,
            CheckInTime: p.CheckInTime,
            CheckOutTime: p.CheckOutTime,
            OverTime: p.OverTime,
            Name:p.Name,
            DeptName:this.deptName,
            DeptId:this.deptId
          })

        }

      });
      console.log(dlist);
      let re = await this.$http.post('/Att/Att_Day/SaveDeclareOverTime', dlist);
      this.visible = false
      this.$emit('submit')

    }
  }
}
</script>
