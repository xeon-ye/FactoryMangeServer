<template>
  <a-select show-search :showArrow="false" allowClear v-model="selectValue" placeholder="选择客户" style="width: 100%" :filter-option="false" :not-found-content="fetching ? undefined : null" @search="getClientSync"
    @change="handleChange">
    <a-spin v-if="fetching" slot="notFoundContent" size="small" />
    <a-select-option v-for="d in data" :key="d.Id">
      {{d.Name}}
    </a-select-option>
  </a-select>
</template>

<script>
import debounce from 'lodash/debounce';
export default {
  components: {
  },
  model: {
    prop: 'clientId',
    event: 'changeId'
  },
  props: {
    clientId: String
  },
  data() {
    this.lastFetchId = 0;
    this.getClientSync = debounce(this.getClient, 800);
    return {
      data: [],
      nonData: [],
      entity: {},
      selectValue: "",
      fetching: false,
      queryParam: {
        condition: 'Name',
        keyword: '',
      }
    };
  },
  async mounted() {
    this.nonData = await this.getClient();
  },
  watch: {
    clientId: async function (newVal, oldval) {
      if (newVal) {
        let e = this.data.find(p => p.Id == newVal);
        if (e == null) {
          this.$http.post('/BaseInfo/BaseInfo_Client/GetTheData', { id: newVal }).then(resJson => {
            this.entity = resJson.Data
            this.data.push(this.entity);
            this.selectValue = newVal;
          })
        } else {
          this.entity = e
          this.selectValue = e.Id
        }
      } else {
        this.selectValue = ""
        this.entity={}
      }
    }
  },
  methods: {
    async getClient(keyword) {
      this.fetching = true;
      this.queryParam.keyword = keyword;
      let re = await this.$http.post('/BaseInfo/BaseInfo_Client/GetDataList', {
        PageIndex: 1,
        PageRows: 10,
        SortField: 'Id',
        Search: this.queryParam
      });
      this.data = re.Data;
      this.fetching = false;
      return re.Data.slice();
    },

    handleChange(v) {
      if (!v) this.data = this.nonData.slice()
      this.entity = this.data.find(p => p.Id == v)
      this.$emit("changeId", this.entity.Id);
      this.$emit("change", this.entity);
    }
  },
}
</script>

<style scoped>
</style>
