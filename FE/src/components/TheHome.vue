<template>
  <div class="body">
    <div class="wrapper">
      <div class="wrapper__container">
        <SideBarComponent />
        <div :class="{'wrapper__container-content-zoom':!zoomSidebar}"  class="wrapper__container-content">
          <HeaderComponent />
          <MainComponent />
        </div>
      </div>
      <BaseToast v-if="isShowAlert"/>
      <RLoading v-if="isLoad" />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import SideBarComponent from "./layout/TheSidebar.vue";
import MainComponent from "./layout/TheMain.vue";
import HeaderComponent from "./layout/TheHeader.vue";
import BaseToast from "./base/BaseToast.vue";
import RLoading from './base/RLoading.vue';

export default {
  name: "HomeComponent",
  components: {
    SideBarComponent,
    MainComponent,
    HeaderComponent,
    BaseToast,
    RLoading,
  },
  computed: mapGetters(["isShowAlert", "zoomSidebar", "isLoad"]),
};
</script>

<style>
body {
  margin: 0;
  overflow: hidden;
}
.body {
  width: 100%;
  height: 100vh;
}
.wrapper {
  background-color: #ffffff;
}
.wrapper__container {
  display: flex;
}

.wrapper__container-content {
  width: calc(100% - 200px);
  height: 100vh;
}
.wrapper__container-content-zoom{
  width: calc(100% - 73px);
  height: 100vh;
}

.hidden {
  visibility: hidden;
}
</style>
