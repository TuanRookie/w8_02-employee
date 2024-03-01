<template>
  <button
    :class="[
      {
        btn: btnText,
        'btn-secondary': isSecondary,
        'btn-with-icon': icon && btnText,
        'icon-btn': icon && !btnText,
        'btn-circle-radius': circleRadius,
      },
      btnColor,
    ]"
    :disabled="isDisabled"
    :tabindex="tabIndex"
    :title="btnToolTip"
  >
    <div v-if="icon" :class="['btn-icon', icon]"></div>
    <div v-if="btnText" class="btn-text">{{ btnText }}</div>
  </button>
</template>
<script>
export default {
  name: "BaseButton",
  props: [
    "isSecondary",
    "btnText",
    "btnToolTip",
    "isDisabled",
    "tabIndex",
    "icon",
    "btnColor",
    "circleRadius",
  ],
};
</script>
