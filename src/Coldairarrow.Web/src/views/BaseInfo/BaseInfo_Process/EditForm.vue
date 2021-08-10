<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="工序名" prop="ProcessName">
          <a-input v-model="entity.ProcessName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="部门" prop="DeartmentList">
          <a-tree-select v-model="entity.DeptId" allowClear :treeData="DepartmentIdTreeData" placeholder="请选择部门" @change="deptChange" treeDefaultExpandAll></a-tree-select>
        </a-form-model-item>

        <a-form-model-item label="是否检验工序" prop="IsCheckProcess">
          <a-checkbox v-model="entity.IsCheckProcess">
          </a-checkbox>
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
      DepartmentIdTreeData: [],
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
      this.$http.post('/Base_Manage/Base_Department/GetTreeDataList', {}).then(resJson => {
        if (resJson.Success) {
          this.DepartmentIdTreeData = resJson.Data
        }
      })

    },
    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/BaseInfo/BaseInfo_Process/GetTheData', { id: id }).then(resJson => {
          this.loading = false

          this.entity = resJson.Data
        })
      }
    },
    deptChange(v, item) {
      this.entity.DeptName = item[0];
      console.log('deptChange', v, item);
      console.log('entity', this.entity)
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/BaseInfo/BaseInfo_Process/SaveData', this.entity).then(resJson => {
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
