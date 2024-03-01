<template>
  <div class="wrapper__dialog">
    <div class="wrapper__dialog-content">
      <div class="dialog-header">
        <div class="dialog-title">{{ dialog.title }}</div>
        <div class="icon icon-close" @click="toggleDialog"></div>
      </div>
      <div class="dialog-body">
        <div class="icon icon-warning"></div>
        <div>{{ dialog.message }}</div>
      </div>
      <div v-if="dialog.type == 'delete'" class="dialog-footer">
        <BaseButton
          :btnText="Text.cancel"
          :isSecondary="true"
          @click="toggleDialog"
          tabIndex="2"
        />
        <BaseButton
          ref="primary"
          btnColor="red"
          :btnText="Text.delete"
          @click="confirmClick"
          tabIndex="1"
        />
      </div>
      <div v-if="dialog.type == 'store'" class="dialog-footer">
        <BaseButton
          :btnText="Text.close"
          :isSecondary="true"
          @click="toggleDialog"
          tabIndex="3"
        />
        <BaseButton
          :btnText="Text.deny"
          :isSecondary="true"
          @click="closeAll"
          tabIndex="2"
        />
        <BaseButton
          ref="primary"
          :btnText="Text.save"
          @click="confirmClick"
          tabIndex="1"
        />
      </div>
      <div v-if="dialog.type == 'error'" class="dialog-footer">
        <BaseButton
          :btnText="Text.close"
          :isSecondary="true"
          @click="toggleDialog"
          tabIndex="3"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";
import BaseButton from "../baseButton/BaseButton.vue";
import RResourceVN from "../../../js/RResource";
export default {
  name: "BaseDialog",
  components: { BaseButton },
  props: ["dialog"],
  emits: ["confirmClick", "closeAll"],
  methods: {
    ...mapActions(["toggleDialog"]),

    /**
     * xác nhận cất hoặc xóa dữ liệu
     * Author:DcTuan (09/01/2024)
     */
    confirmClick() {
      this.toggleDialog();
      this.$emit("confirmClick");
    },

    /**
     * chọn không xóa dữ liệu
     * Author:DcTuan (09/01/2024)
     */
    closeAll() {
      this.toggleDialog();
      this.$emit("closeAll");
    },
  },
  data() {
    return {
      Text: RResourceVN.Text,
    };
  },
};
</script>
