<template>
  <div class="tab_bar">
    <div class="menu">
      <van-row class="menu_row" gutter="26" type="flex" justify="center" align="center">
        <van-col :class="'menu_col '+(menu==item.id?'active':'')" :span="24/menuMap.length" v-for="(item, index) in menuMap" :key="index" @click="toggleMenu(item.id)" >
          <van-icon :name=" menu==item.id ? item.activeIcon : item.icon" size="26" />
          <label>{{item.name}}</label>
        </van-col>
      </van-row>
    </div>
  </div>
</template>
<script>
import vue from "vue";

export default {
  name: "tabbar",
  components: {},
  data() {
    return {
      menu: "adminStore",
      menuMap: [
        {
          id: "adminStore",
          name: "库存",
          href: "/adminStore",
          icon: "wap-home-o",
          activeIcon: "wap-home"
        },
        {
          id: "adminOrder",
          name: "订单",
          href: "/adminOrder",
          icon: "bill-o",
          activeIcon: "bill"
        },
        {
          id: "report",
          name: "报表",
          href: "/report",
          icon: "todo-list-o",
          activeIcon: "todo-list"
        }
      ]
    };
  },
  computed: {},
  methods: {
    toggleMenu(menuId){
      this.menu = menuId
      let menu = this.menuMap.find(a=>a.id == menuId)
      this.$router.push(menu.href)
    }
  }
};
</script>
<style scope>
.tab_bar {
  position: fixed;
  bottom: 0;
  z-index: 999;
  width: 100%;
  height: 60px;
  background: #fff;
  bottom: 0;
  border-top: 1px solid #f3f3f3;
}
.tab_bar .menu {
  height: 100%;
}
.tab_bar .menu_row {
  height: 100%;
}
.tab_bar .menu_col {
  height: 100%;
  text-align: center;
  color: #999;
}
.tab_bar .menu_col.active {
  color: #333;
}

/* .tab_bar .menu_col::before {
  display: inline-block;
  content: "";
  height: 100%;
  vertical-align: middle;
} */
.tab_bar .menu_col i {
  display: block;
  margin-top: 8px;
}
.tab_bar .menu_col label {
  margin-top: -4px;
  font-size: 12px;
}
</style>

