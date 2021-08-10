<template>
  <div>
    <a-row type="flex">
      <a-col class="mr5">
        <a-card hoverable style="height: 360px">
          <img slot="cover" alt="example" class="img1" src="../../assets/nopic.jpg" />
          <template slot="actions" class="ant-card-actions">
            <ImgCutter  :sizeChange="false" :boxWidth="1000" :boxHeight="600" :cutWidth="450" :cutHeight="600" @cutDown="upLoadStylePic" fileType="jpeg">
              <a-button type="link" :disabled="!entity.StyleId" slot="open">
                <a-icon type="edit" /> 上传款式图
              </a-button>
            </ImgCutter>
          </template>
        </a-card>
      </a-col>
      <a-col>
        <a-card hoverable title="基本信息" style="height: 360px">
          <div slot="extra">
            <a-button type="link" size="small" @click="doedit">
              <p v-if="!isedit">编辑</p>
              <p v-else>取消</p>
            </a-button>
            <a-button :disabled="!isedit" type="link" size="small" @click="save">
              保存
            </a-button>
          </div>
          <ValidationObserver ref="formRef">
            <table>
              <tr>
                <td>款式ID</td>
                <td class="tdWidth">
                  <a-input v-model="entity.StyleId" autocomplete="off" readOnly />
                </td>
                <td>款号</td>
                <td class="tdWidth">
                  <ValidationProvider name="款号" rules="required" v-slot="{ errors }">
                    <a-input  v-bind:class="{ editcolor: isedit&&!errors[0], errorcolor:isedit&&errors[0]}" :readOnly="!isedit" v-model="entity.StyleNo" autocomplete="off">
                      <a-tooltip v-show="isedit&&errors[0]" slot="suffix" :title="errors[0]">
                        <a-icon type="info-circle" style="color: red" />
                      </a-tooltip>
                    </a-input>
                  </ValidationProvider>
                </td>
              </tr>

              <tr>
                <td>季节</td>
                <td>
                  <a-input v-model="entity.Season" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" autocomplete="off" />
                </td>
                <td>客款号</td>
                <td>
                  <a-input v-model="entity.ClientStyleNo" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" autocomplete="off" />
                </td>

              </tr>
              <tr>
                <td>客户</td>
                <td>
                  <a-input v-show="!isedit" v-model="entity.ClientName" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" autocomplete="off" />
                  <client-select v-show="isedit" class="editcolor" v-model="entity.ClientId" @change="clientChange"></client-select>
                </td>
                <td>品牌</td>
                <td>
                  <a-input v-model="entity.Brand" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" autocomplete="off" />
                </td>
              </tr>
              <tr>
                <td>针型</td>
                <td>
                  <a-input v-show="!isedit" v-model="entity.Gauge" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" autocomplete="off" />
                  <a-select v-show="isedit" v-model="entity.Gauge" class="editcolor" style="width:100%">
                    <a-select-option v-for="item in needlist" :key="item.NeedleName">
                      {{ item.NeedleName }}
                    </a-select-option>
                  </a-select>
                </td>
                <td>克重(g)</td>
                <td>
                  <ValidationProvider name="克重" rules="min_value:1" v-slot="{ errors }">
                    <a-input v-model="entity.Weight" v-bind:class="{editcolor: isedit&&!errors[0], errorcolor:isedit&&errors[0] }" :readOnly="!isedit" autocomplete="off">
                      <a-tooltip v-if="isedit&&errors[0]" slot="suffix" :title="errors[0]">
                        <a-icon type="info-circle" style="color: red" />
                      </a-tooltip>
                    </a-input>
                  </ValidationProvider>
                </td>

              </tr>
              <tr style="height:90px">
                <td>备注</td>
                <td colspan="3">
                  <a-textarea v-model="entity.Notes" v-bind:class="{ editcolor: isedit }" :readOnly="!isedit" :auto-size="{ minRows: 3, maxRows: 3}" autocomplete="off" />
                </td>
              </tr>
            </table>
          </ValidationObserver>
        </a-card>
      </a-col>
    </a-row>
  </div>
</template>

<script>
import ImgCutter from 'vue-img-cutter'
import clientSelect from '../BaseInfo/clientSelect.vue'
import { ValidationProvider, ValidationObserver } from 'vee-validate'
export default {
  components: {
    ImgCutter,
    clientSelect,
    ValidationProvider,
    ValidationObserver
  },
  props: {

  },
  data() {
    return {
      entity: {
        StyleId: "",
        StyleNo: '',
        ClientStyleNo: '',
        ClientId: '',
        ClientName: '',
        Weight: '',
        Gauge: '',
        Notes: '',
        Brand: '',
        Season: ''
      },
      isedit: false,

    }
  },
  mounted() {

  },
  computed: {
    needlist: function () {
      return this.$store.state.baseinfo.Needle
    }
  },
  methods: {
    upLoadStylePic(picobj){
      //上传图片
      console.log(picobj)
    },
    clientChange(v) {
      this.entity.ClientName = v.Name
    },
    newStyle() {
      this.entity = {}
      this.isedit = true
    },
    async loadStyle(id) {
      if (id) {
        await this.$http.post('/Prod/Prod_Style/GetTheData', { id: id }).then(resJson => {
          this.entity = resJson.Data
          this.isedit = false
        })
      }
    },
    doedit() {
      this.isedit = !this.isedit;
    },
    async save() {
      if (this.isedit) {
        let vali = await this.$refs.formRef.validate()
        if (vali) {
          let re = await this.$http.post("/Prod/Prod_Style/SaveData", this.entity)
          this.isedit = false;
          this.$message.success('操作成功');
          this.entity = re.Data;
          console.log('save obj', re)
          return true
        } else {
          this.$message.warning('输入错误，请重新检查提交');
          return false
        }
      }
      return true
    }
  },
}
</script>

<style scoped>
.img1 {
  width: 225px;
  height: 300px;
  object-fit: cover;
}

table {
  width: 100%;
}
tr {
  height: 45px;
}
.tdWidth {
  width: 195px;
}
td {
  text-align: right;
  padding-left: 10px;
}
.editcolor {
  border: 1px solid darkseagreen;
}
.errorcolor {
  border: 1px solid red;
}
</style>
<style>
.mr5 {
  margin-right: 8px;
}
</style>

