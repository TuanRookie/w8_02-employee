<template>
  <BaseDialog
    :dialog="dialog"
    @confirmClick="confirmClick"
    @closeAll="closeAll"
  />
</template>

<script>
import DialogAction from "@/enums/dialogAction";
import { mapGetters, mapActions } from "vuex";
import BaseDialog from "../../components/base/baseDialog/BaseDialog.vue";
export default {
  name: "EmployeeDialog",
  components: { BaseDialog },
  computed: mapGetters(["dialog"]),
  emits: ["setIsStore"],
  methods: {
    ...mapActions([
      "deleteEmployee",
      "deleteBatchEmployee",
      "toggleModal",
    ]),

    /**
     * xác nhận cất hoặc xóa nhân viên
     * Author:DcTuan (09/01/2024)
     */
    confirmClick() {
      if (this.dialog.action == DialogAction.CONFIRM_STORE) {
        this.$emit("setIsStore");
      }
      if (this.dialog.action == DialogAction.CONFIRM_DELETE) {
        this.deleteEmployee();
      }
      if (this.dialog.action == DialogAction.CONFIRM_DELETE_BATCH) {
        this.deleteBatchEmployee();
      }
    },

    /**
     * chọn không xóa nhân viên
     * Author:DcTuan (09/01/2024)
     */
    closeAll() {
      this.toggleModal();
    },
  },
};
</script>
