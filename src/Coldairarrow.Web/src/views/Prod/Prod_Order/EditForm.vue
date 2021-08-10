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
        <a-form-model-item label="订单ID" prop="OrderId">
          <a-input v-model="entity.OrderId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="订单编号" prop="OrderNo">
          <a-input v-model="entity.OrderNo" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="客户ID" prop="ClientId">
          <a-input v-model="entity.ClientId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="客户名" prop="ClientName">
          <a-input v-model="entity.ClientName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="交期" prop="DeliveryTime">
          <a-input v-model="entity.DeliveryTime" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="订单日期" prop="OrderDate">
          <a-input v-model="entity.OrderDate" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="订单金额" prop="OrderAmount">
          <a-input v-model="entity.OrderAmount" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="订单数量" prop="OrderQty">
          <a-input v-model="entity.OrderQty" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="跟单员ID" prop="OrderSuperId">
          <a-input v-model="entity.OrderSuperId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="跟单姓名" prop="OrderSuperName">
          <a-input v-model="entity.OrderSuperName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="装运日期" prop="ShippingDate">
          <a-input v-model="entity.ShippingDate" autocomplete="off" />
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
        this.$http.post('/Prod/Prod_Order/GetTheData', { id: id }).then(resJson => {
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
        this.$http.post('/Prod/Prod_Order/SaveData', this.entity).then(resJson => {
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
