<template>
  <router-view></router-view>
</template>

<script>

export default {
  name: "App",
  components: {

  },
};
</script>

<style>
@import url(./css/base/button.css);
@import url(./css/base/combobox.css);
@import url(./css/base/dialog.css);
@import url(./css/base/input.css);
@import url(./css/base/icon.css);
@import url(./css/base/contextmenu.css);
@import url(./css/base/table.css);
@import url(./css/base/paging.css);
@import url(./css/base/toast.css);
@import url(./css/base/modal.css);
@import url(./css/base/import.css);
@import url(./css/font.css);
@import url(./css/layout/header.css);
@import url(./css/layout/employee.css);
@import url(./css/layout/sidebar.css);


* {
  box-sizing: border-box;
}
#app {
  font-family: Noto-Sans-Regular;
  font-size: 14px;
  display: flex;
}
</style>
