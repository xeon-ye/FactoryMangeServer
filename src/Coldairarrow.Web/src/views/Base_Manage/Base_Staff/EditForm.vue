<template>
  <a-modal :title="title" width="40%" :visible="visible" :confirmLoading="loading" @ok="handleSubmit" @cancel="()=>{this.visible=false}">
    <a-spin :spinning="loading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">

        <a-form-model-item label="员工姓名" prop="StaffName">
          <a-input v-model="entity.StaffName" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="部门" prop="DeartmentList">
          <a-tree-select @change="deptChange" v-model="entity.DepartmentId" allowClear :treeData="DepartmentIdTreeData" placeholder="请选择部门" treeDefaultExpandAll></a-tree-select>
        </a-form-model-item>
        <a-form-model-item label="系统帐号" prop="RoleIdList">
          <a-select :options="selectData" v-model="entity.UserId" allowClear>
          </a-select>
        </a-form-model-item>
        <a-form-model-item label="钉钉ID" prop="DDid">
          <a-input v-model="entity.DDid" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="考勤ID" prop="AttId">
          <a-input v-model="entity.AttId" autocomplete="off" />
        </a-form-model-item>
        <a-form-model-item label="手机" prop="Tel">
          <a-input v-model="entity.Tel" autocomplete="off" />
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
      DepartmentIdTreeData: [],
      visible: false,
      loading: false,
      entity: {},
      rules: {},
      title: '',
      selectData: [
        { value: '001', label: 'admin' },
        { value: '002', label: 'admin2' },


      ]

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
      this.getUserList();
    },
    async getUserList() {
      let re = await this.$http.post('/Base_Manage/Base_User/GetDataList', {
        PageIndex: 1,
        PageRows: 100,
        SortField: 'Id',
        SortType: 'asc',
      });
       console.log("aaa", re);
      this.selectData = [];
      re.Data.forEach(item => {
        this.selectData.push({value:item.Id,label:item.UserName+'('+item.RealName+')|'+item.RoleNames})
      })
    },

    openForm(id, title) {
      this.init()

      if (id) {
        this.loading = true
        this.$http.post('/Base_Manage/Base_Staff/GetTheData', { id: id }).then(resJson => {
          this.loading = false
          this.entity = resJson.Data
        })
      }
    },
    deptChange(value, label, extra) {
      this.entity.DeptName = label[0];
    },
    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }
        this.loading = true
        this.$http.post('/Base_Manage/Base_Staff/SaveData', this.entity).then(resJson => {
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
