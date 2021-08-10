<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete(selectedRowKeys)" :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
      <a-button type="danger" icon="sync" @click="SyncStaffByDingDing()">从钉钉同步用户</a-button>
      <a-button type="primary" icon="sync" @click="SyncStaffAttID()">同步用户考勤ID</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="StaffName">员工姓名</a-select-option>
                <a-select-option key="DepartmentId">部门ID</a-select-option>
                <a-select-option key="UserId">绑定用户ID</a-select-option>
                <a-select-option key="DDid">钉钉ID</a-select-option>
                <a-select-option key="Tel">手机</a-select-option>
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
          <a @click="handleEdit(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
import EditForm from './EditForm'

const columns = [
  {
    title: '员工姓名', dataIndex: 'StaffName', width: '10%', sorter: true,
    sortDirections: ['descend', 'ascend']
  },
  {
    title: '部门', dataIndex: 'DeptName', width: '10%', sorter: true,
    sortDirections: ['descend', 'ascend']
  },
  { title: '系统帐号', dataIndex: 'UserId', width: '10%' },
  {
    title: '考勤ID', dataIndex: 'AttId', width: '10%', sorter: true,
    sortDirections: ['descend', 'ascend']
  },
  { title: '钉钉ID', dataIndex: 'DDid', width: '15%' },
  { title: '部门ID', dataIndex: 'DepartmentId', width: '10%' },
  { title: '手机', dataIndex: 'Tel', width: '10%' },
  { title: '操作', dataIndex: 'action', scopedSlots: { customRender: 'action' } }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
  },
  data() {
    return {
      data: [],
      pagination: {
        current: 1,
        pageSize: 20,
        showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
      },
      filters: {},
      sorter: { field: 'DepartmentId', order: 'asc' },
      loading: false,
      columns,
      queryParam: {},
      selectedRowKeys: []
    }
  },
  methods: {
    async SyncStaffByDingDing() {
      this.loading = true
      let re = await this.$http('/Base_Manage/Base_Staff/SyncStaffByDingDing')
      this.getDataList()
      this.$message.success('同步成功！')
      this.loading = false;

    },
    //同步考勤ID
    async SyncStaffAttID() {
      this.loading = true
      let re = await this.$http.get('/Base_Manage/Base_Staff/SyncStaffAttID')
      this.getDataList()
      this.$message.success('同步成功！')
      this.loading = false;
    },
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []
      console.log(this.sorter.field);
      this.loading = true
      this.$http
        .post('/Base_Manage/Base_Staff/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
          this.loading = false
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
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除吗?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.$http.post('/Base_Manage/Base_Staff/DeleteData', ids).then(resJson => {
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