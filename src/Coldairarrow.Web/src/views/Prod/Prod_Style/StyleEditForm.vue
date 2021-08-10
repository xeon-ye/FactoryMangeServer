<template>
  <a-modal :title="title" width="1000px" :visible="visible" :confirmLoading="loading" :maskClosable="false" okText="保存" @ok="handleSubmit" @cancel="()=>{this.visible=false}" centered>
    <!-- <a-spin :spinning="loading"> -->
    <div class="list" ref="listView">
      <a-row>
        <a-col :span="20">
          <a-row>
            <baseinfo ref="baseInfo" id="baseinfo" class="tm"></baseinfo>
          </a-row>

        </a-col>
        <a-col offset="10px" :span="2">
          <a-anchor :getContainer="()=>this.$refs.listView">
            <a-anchor-link href="#baseinfo" title="基本信息" />
            <a-anchor-link href="#cf" title="原料成份" />
            <a-anchor-link href="#proofData" title="打样资料" />
            <a-anchor-link href="#proofRecord" title="打样记录" />
            <a-anchor-link href="#prodRecord" title="大货记录" />
          </a-anchor>
        </a-col>
      </a-row>
    </div>
    <!-- </a-spin> -->
  </a-modal>
</template>
<style scoped>
.list {
  width: 100%;
  max-height: 600px;
  overflow: auto;
}
.tm {
  margin-bottom: 20px;
}
</style>

<script>
import baseinfo from "../../../components/Style/baseinfo.vue";

export default {
  props: {
    parentObj: Object
  },
  components: {
    baseinfo,
  },
  data() {
    return {
      visible: false,
      loading: false,
      title: '款式信息'
    }
  },
  methods: {
    init() {
      this.visible = true
      this.entity = {}

    },
    custormAnchor(anchorName) {
      // 找到锚点
      let anchorElement = document.getElementById(anchorName);
      // 如果对应id的锚点存在，就跳转到锚点
      if (anchorElement) { anchorElement.scrollIntoView(); }
    },
    newStyle() {
      this.init()
      this.$nextTick(async () => {

        await this.$refs.baseInfo.newStyle();
        this.title = "新款式"
      })

    },
    loadStyle(id, title) {
      this.init()
      this.$nextTick(async () => {
        this.loading = true
        await this.$refs.baseInfo.loadStyle(id)
        this.loading = false
        this.title = "款式信息"
      })
    },
    async handleSubmit() {
      if (await this.$refs.baseInfo.save()) this.visible = false
      this.$emit("change")
    }
  }
}
</script>
