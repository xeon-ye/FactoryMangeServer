<template>
  <a-modal
    :title="title"
    width="40%"
    :visible="visible"
    :confirmLoading="loading"
    @ok="handleSubmit"
    @cancel="()=>{this.visible=false}"
  >
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="StaffId" prop="StaffId">
          <a-input v-model="entity.StaffId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="AttDate" prop="AttDate">
          <a-input v-model="entity.AttDate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="AttRecord" prop="AttRecord">
          <a-input v-model="entity.AttRecord" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="DayOverTime" prop="DayOverTime">
          <a-input v-model="entity.DayOverTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="CheckInTime" prop="CheckInTime">
          <a-input v-model="entity.CheckInTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="CheckOutTime" prop="CheckOutTime">
          <a-input v-model="entity.CheckOutTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="BeginOverTime" prop="BeginOverTime">
          <a-input v-model="entity.BeginOverTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="EndOverTime" prop="EndOverTime">
          <a-input v-model="entity.EndOverTime" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  props: {
    parentObj: Object
  },
  data() {
    return {
      layout: {
        labelCol: { span: 5 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: ''
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}
      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Att/Att_Day/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Att/Att_Day/SaveData', this.entity).then(resJson => {
          this.loading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false

            this.parentObj.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>
