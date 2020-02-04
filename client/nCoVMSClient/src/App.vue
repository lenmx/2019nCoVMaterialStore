<template>
  <div id="app">
    <van-number-keyboard safe-area-inset-bottom />
    <router-view />
  </div>
</template>

<script>
export default {
  name: "App"
};
</script>

<style>
body,
* {
  margin: 0;
  padding: 0;
}
#app {
}
</style>
