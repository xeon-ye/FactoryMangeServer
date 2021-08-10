<template>
  <a-modal :title="title" width="1024px" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-table ref="table" :scroll="{ y: 450 }" :columns="columns" :rowKey="row => row.AttDate" :pagination="pagination" :dataSource="data" :loading="loading" :bordered="true" size="small">
      </a-table>
    </a-spin>
  </a-modal>
</template>

<script>
import moment from 'moment';
const columns = [
  { title: '日期', dataIndex: 'AttDate', width: '10%' },
  { title: '打卡记录', dataIndex: 'AttRecord', width: '30%' },
  { title: '加班时长', dataIndex: 'OverTime', width: '10%' },
  {
    title: '加班申报',
    children: [{ title: '开始时间', dataIndex: 'BeginOverTime', width: '10%' },
    { title: '结束时间', dataIndex: 'EndOverTime', width: '10%' },
    { title: '申报时长', dataIndex: 'DeclareOverTime', width: '10%' },],

  },

]

export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      data: [],
      visible: false,
      loading: false,
      title: '',
      columns,
      pagination: {
        pageSize: 60,
      },
    }
  },
  methods: {
    init() {
      this.visible = true
    },
    async openForm(record) {
      this.init()
      this.title = record.Name + record.MonthStr + '月打卡明细';
      if (record) {
        this.loading = true
        let re = await this.$http.post('/Att/Att_Day/GetTheDayList', { id: record.AttId, month: record.MonthStr });
        re.Data.forEach(e => {
          e.AttDate = moment(e.AttDate).format('MM-DD');
          e.CheckInTime = e.CheckInTime != null ? moment(e.CheckInTime).format('HH:mm') : "";
          e.BeginOverTime = e.BeginOverTime != null ? moment(e.BeginOverTime).format('HH:mm') : "";
          e.EndOverTime = e.EndOverTime != null ? moment(e.EndOverTime).format('HH:mm') : "";
        });
        this.data = re.Data;
        this.loading = false
      }
    },
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
    },
    handleSubmit() {
      this.visible = false
    }
  }
}
</script>
