<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleDeclareOvertime()">加班申报</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete(selectedRowKeys)" :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>
    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="部门">
              <a-select placeholder="显示所有部门" allowClear v-model="selectDeptId" :options="deptList" @change="getDataList()"> </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="5" :sm="24">
            <a-form-item label="考勤月份">
              <a-select placeholder="显示所有月份" allowClear v-model="selectMonth" :options="monthList" @change="getDataList()"> </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="Name">员工</a-select-option>
                <a-select-option key="DeptName">部门</a-select-option>
                <a-select-option key="AttDate">日期</a-select-option>

              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="() => {this.pagination.current = 1; this.getDataList()}">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination" :loading="loading" @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="small">
      <span slot="action" slot-scope="text, record">
        <template>
          <!-- <a @click="handleEdit(record.Id)">编辑</a> -->
          <!-- <a-divider type="vertical" /> --> 
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
    <declare-overtime ref="declareOvertime" :parentObj="this" @submit="getDataList()"></declare-overtime>

  </a-card>
</template>

<script>
import EditForm from './EditForm'
import DeclareOvertime from './DeclareOvertime'

import moment from 'moment'
const columns = [
  { title: '日期', dataIndex: 'AttDateStr', width: '10%' },
  { title: '姓名', dataIndex: 'Name', width: '8%' },
  { title: '部门', dataIndex: 'DeptName', width: '8%' },
  { title: '打卡记录', dataIndex: 'AttRecord', width: '20%' },
  { title: '考勤加班', dataIndex: 'OverTime', width: '8%' },
  {
    title: '加班申报时间',
    children: [
      { title: '开始', dataIndex: 'BeginOverTimeStr', width: '8%' },
      { title: '结束', dataIndex: 'EndOverTimeStr', width: '8%' },
      { title: '时长', dataIndex: 'DeclareOverTime', width: '8%' },
    ]
  },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm,
    DeclareOvertime
  },
  mounted() {
    this.init()
  },
  data() {
    return {
      deptList: [],
      monthList: [],
      selectDeptId: "",
      selectMonth: "",
      data: [],
      pagination: {
        current: 1,
        pageSize: 10,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: []
    }
  },
  methods: {
    async init() {
      ///生成月份表
      for (var i = 0; i <= 2; i++) {
        let mstr = moment().add(-i, 'month').year() + "-" + (moment().add(-i, 'month').month() + 1)
        this.monthList.push({ value: mstr, label: mstr })
      }
      //获取部门表
      let re = await this.$http.get('/Base_Manage/home/GetCurrentUserDeptList');
      this.deptList = re.Data;
      console.log('dept', this.deptList);

      //设置选择默认值
      this.selectDeptId = this.deptList[0].value;
      this.selectMonth = this.monthList[0].value;
      // this.loadAttDate();
      // debugger;
      this.getDataList()
    },

    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []
      this.loading = true
      this.$http
        .post('/Att/Att_Day/GetDataList', {
          DeptId:this.selectDeptId,
          SelectMonth:this.selectMonth,
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          resJson.Data.forEach(p => {
            p.AttDateStr = moment(p.AttDate).format('YY-MM-DD');
            p.BeginOverTimeStr = moment(p.BeginOverTime).format('HH:mm');
            p.EndOverTimeStr = moment(p.EndOverTime).format('HH:mm');
          });
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
        })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    hanldleDeclareOvertime() {
      let item = this.deptList.find(p => p.value == this.selectDeptId);
      console.log("dept",item);
      if(item!=null) this.$refs.declareOvertime.openForm(item)
      else   this.$message.warning('请选择部门');
    },

    handleDelete(ids) {
      var thisObj = this
      console.log('ids', ids)
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/Att/Att_Day/DeleteData', ids).then(resJson => {
              resolve()

              if (resJson.Success) {
                thisObj.$message.success('操作成功!')

                thisObj.getDataList()
              } else {
                thisObj.$message.error(resJson.Msg)
              }
            })
          })
        }
      })
    }
  }
}
</script>