<template>
  <a-card :bordered="false">

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="6" :sm="24">
            <a-form-item label="部门">
              <a-select allowClear v-model="selectDeptId" :options="deptList"> </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-form-item label="考勤月份">
              <a-select allowClear v-model="selectMonth" :options="monthList"> </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="loadAttDate">查询</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table ref="table" :columns="columns" :rowKey="row => row.StaffId" :dataSource="data" :pagination="pagination" :loading="loading" @change="handleTableChange"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="small">
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleDetail(record)">详情</a>
        </template>
      </span>
    </a-table>

    <Detail-form ref="DetailForm" :parentObj="this"></Detail-form>
  </a-card>
</template>

<script>
import DetailForm from './DetailForm'
import moment from 'moment'
const columns = [
  // { title: '员工ID', dataIndex: 'StaffId', width: '10%' },
  { title: '姓名', dataIndex: 'Name', width: '10%' },
  { title: '部门', dataIndex: 'DeptName', width: '10%' },
  { title: '月份', dataIndex: 'MonthStr', width: '10%' },
  { title: '出勤天数', dataIndex: 'AttendanceDays', width: '10%' },
  { title: '缺卡次数', dataIndex: 'Absence', width: '10%' },
  { title: '加班小时', dataIndex: 'OverTime', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    DetailForm
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

      //设置选择默认值
      this.selectDeptId = this.deptList[0].value;
      this.selectMonth = this.monthList[0].value;
      this.loadAttDate();
      // debugger;

    },
    async loadAttDate() {
      this.pagination.current = 1
      this.pagination.SelectDeptId = this.selectDeptId
      this.pagination.SelectMonth = this.selectMonth
      this.getDataList();
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
        .post('/Att/Att_Month/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          Search: {
            SelectDeptId: this.selectDeptId,
            SelectMonth: this.selectMonth,
          },

          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          //Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
          this.data.forEach(p => {
            p.MonthStr = this.selectMonth
            p.OverTime = p.OverTime.toFixed(1)
          });

        })
    },
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    handleDetail(record) {
      this.$refs.DetailForm.openForm(record)
    }


  }
}
</script>