<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="NewsTime" prop="NewsTime">
          <a-date-picker v-model="entity.NewsTime" :format="dateFormat" />
        </a-form-model-item>
        <a-form-model-item label="标志" prop="Title">
          <a-input v-model="entity.Title" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="intro" prop="intro">
          <a-input v-model="entity.intro" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="Text" prop="Text">
          <a-input v-model="entity.Text" autocomplete="off" />
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
 /* eslint-disable */ 
import moment from 'moment';
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
      title: '',
      dateFormat: 'YYYY/MM/DD',
    }
  },
  methods: {
    moment,
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
        this.$http.post('/test/News/GetTheData', { id: id }).then(resJson => {
          this.loading = false
        
          console.log("new entiry",resJson.Data)
          this.entity = resJson.Data
          this.entity.NewsTime = moment(this.entity.NewsTime)
        })
      }
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
      //  this.entity.NewsTime = moment(this.entity.NewsTime).format('YYYY-MM-DD')
        this.$http.post('/test/News/SaveData', this.entity).then(resJson => {
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
