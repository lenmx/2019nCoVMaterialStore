<template>
  <div class="app_container">
    <div class="title">
      <label>库存<small>查看库存状态</small></label>

      <van-icon name="add-o" size="24px" style="float: right; margin: 0 6px;" @click="search()" />
      <van-icon name="search" size="24px" style="float: right; margin: 0 6px;" @click="add()" />

    </div>
    <van-collapse v-model="activeNames" accordion>
      <van-collapse-item name="3">
        <div slot="title">一次性医用口罩</div>
        <div slot="value">1000个</div>
        <div class="cell_panel">
          <h4>一次性医用口罩<small>(一次性医用口罩一次性医用口罩)</small></h4>
          <p style="text-align: right; color: red;">￥1.80元</p>
          <p>限购： 3</p>
          <p>代买：9</p>
          <p>预定：18</p>
          <p>销售：27 </p>
          <p>总量：1000(剩：163)</p>
          <van-divider />
          <van-button type="warning" size="small">修改</van-button>
        </div>
      </van-collapse-item>
    </van-collapse>

  </div>
</template>

<script>
export default {
  data() {
    return {
      activeNames: ""
    };
  },
  mounted() {},
  methods: {
    search() {},
    add() {}
  }
};
</script>

<style scoped>
.app_container {
  margin: 8px 6px;
}
.app_container .title {
  margin: 16px;
}
.app_container .title small {
  color: #666;
  margin-left: 10px;
}
.cell_panel {
  padding: 0 8px;
}
</style>
 


