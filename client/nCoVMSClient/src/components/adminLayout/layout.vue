<template>
  <div>
    <div class="main_container">
      <router-view />
    </div>
    <tabbar />
  </div>
</template>
<script>
import tabbar from "./tabbar.vue";
export default {
  name: "layout",
  components: {
    tabbar
  }
};
</script>
