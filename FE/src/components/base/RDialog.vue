<template>
  <div>
    <div class="dialog__container">
      <div class="infor__title" >{{titleDialog}}</div>
      <div class="dialog__container--infor">
        <div class="infor__content">
          {{ content }}
          <p v-if="errorCode" class="error-code">Mã lỗi: {{ errorCode }}</p>
        </div>
      </div>
      <div class="dialog__container-footer">
        <r-button
          @click="$emit('handleCancel')"
          class="button-default"
          :contentButton="buttonWhiteContent"
        />
        
        <r-button
          v-if="buttonWhiteCenterContent"
          class="button-default"
          :contentButton="buttonWhiteCenterContent"
        />
        
        <r-button
          v-if="buttonBlueContent"
          @click="$emit('handleSave')"
          class="button button-save"
          :contentButton="buttonBlueContent"
        />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RDialog",
  props: [
    "titleDialog",
    "iconClass",
    "content",
    "buttonWhiteContent",
    "buttonWhiteCenterContent",
    "buttonBlueContent",
    "errorCode",
  ],
};
</script>

<style scoped>
@import url(../../css/base/dialog.css);
</style>
