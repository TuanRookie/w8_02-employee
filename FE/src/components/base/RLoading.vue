<template>
  <div>
    <div class="loading__container">
      <img class="loading" src="../../assets/main/loading.gif" />
    </div>
  </div>
</template>

<script>
export default {
  name: "RLoading",
  props: [],
};
</script>

<style scoped>
.loading__container {
  position: fixed;
  width: 100vw;
  height: 100vh;
  background: rgb(102, 100, 100, 0.1);
  top: 0;
  left: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 30;
}
.loading {
  position: absolute;
  opacity: 1;
  scale: 5;
}
</style>
