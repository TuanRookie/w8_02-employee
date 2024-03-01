<template>
  <div class="content">
    <div class="employee__header">
      <div class="content-title">{{ text.employee }}</div>
      <BaseButton :btnText="text.addEmployee" @click="openForm" />
    </div>
    <div class="employee__body">
      <EmployeeToolBar />
      <div style="height: 70vh;">
        <EmployeeTable />
      </div>
      

      <EmployeePaging />
    </div>
  </div>
  <EmployeeModal
    v-if="isShowModal"
    :isStore="isStore"
    @isStoreDone="() => (this.isStore = false)"
  />
  
  <EmployeeImport v-if="isShowImport"/>

  <EmployeeDialog
    v-if="isShowDialog"
    @setIsStore="() => (this.isStore = true)"
  />
</template>
<script>
import { mapActions, mapGetters } from "vuex";
import Gender from "../../enums/gender.js";
import formType from "../../enums/formType.js";
import EmployeeModal from "./EmployeeModal.vue";
import EmployeeImport from "./EmployeeImport.vue"
import EmployeeTable from "./EmployeeTable.vue";
import EmployeePaging from "./EmployeePaging.vue";
import EmployeeToolBar from "./EmployeeToolBar.vue";
import EmployeeDialog from "./EmployeeDialog.vue";
import RResource from "../../js/RResource";
import BaseButton from "../../components/base/baseButton/BaseButton.vue";
export default {
  name: "EmployeeList",
  components: {
    EmployeeToolBar,
    EmployeeTable,
    EmployeePaging,
    EmployeeModal,
    EmployeeImport,
    EmployeeDialog,
    BaseButton,
  },
  computed: mapGetters(["isShowModal", "isShowDialog", "isShowImport"]),
  created() {
    //load dữ liệu nhân viên và phòng ban
    this.getEmployee();
    this.getDepartment();
  },
  mounted() {
    document.addEventListener("keydown", this.shortcutKey);
  },
  unmounted() {
    document.removeEventListener("keydown", this.shortcutKey);
  },
  methods: {
    ...mapActions([
      "changeFormType",
      "toggleModal",
      "getEmployee",
      "getDepartment",
      "selectEmployee",
      "setModalTitle",
      "setNewEmployeeCode",
    ]),
    /**
     * xử lí phím tắt
     * @param {*} e
     * Author:DcTuan (09/01/2024)
     */
    shortcutKey(e) {
      if (e.key == "Insert") {
        this.openForm();
      }
    },

    /**
     * Mở form nhân viên
     * Author:DcTuan (09/01/2024)
     */
    openForm() {
      this.setModalTitle(RResource.Text.addFormTitle);
      this.selectEmployee({ Gender: Gender.MALE });
      this.changeFormType(formType.STORE);
      this.setNewEmployeeCode();
      this.toggleModal();
    },
  },
  data() {
    return {
      isStore: false,
      text: RResource.Text,
    };
  },
};
</script>
