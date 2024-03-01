<template>
  <div class="wrapper__toast">
    <div class="wrapper__toast-content">
      <div v-if="alert.type == 'success'" class="toast-type toast-success">
        <div class="toast-type-icon icofont-check-circled"></div>
        <div>{{ Text.success }}</div>
      </div>
      <div v-if="alert.type == 'error'" class="toast-type toast-error">
        <div class="toast-type-icon icofont-close-circled"></div>
        <div>{{ Text.error }}</div>
      </div>
      <div class="alert-message">{{ alert.message }}</div>
    </div>
    <div class="toast-close-icon icofont-close" @click="toggleAlert"></div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import RResourceVN from "../../js/RResource";

export default {
  name: "BaseToast",
  computed: mapGetters(["alert", "isShowAlert"]),
  created() {
    const me = this;
    setTimeout(function () {
      if (me.isShowAlert == true) {
        me.toggleAlert();
      }
    }, 5000);
  },
  methods: {
    ...mapActions(["toggleAlert"]),
  },
  data() {
    return {
      Text: RResourceVN.Text,
    };
  },
};
</script>

