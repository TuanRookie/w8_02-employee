<template>
  <!-- Khi thu nhỏ sidebar -> main rộng(nhận zoomIn-main)-->
  <div class="wrapper__container--main">
    <router-view></router-view>
    
  </div>
</template>

<script>

export default {
  name: "MainComponent",
  components: {
  },
};
</script>

<style scoped>
</style>
