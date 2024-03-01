<template>
  <BaseTable
    :headers="headers"
    :dataList="employees"
    :mapData="mapData"
    :isContext=true
    @modifyForm="modifyForm"
    @selectDuplicate="selectDuplicate"
    @selectDelete="selectDelete"
    @checkAll="checkAll"
    @checkOne="checkOne"
  />
</template>
<script>
import BaseTable from "../../components/base/baseTable/BaseTable.vue";
import FormType from "@/enums/formType.js";
import DialogAction from "../../enums/dialogAction.js";
import { mapActions, mapGetters } from "vuex";
import RResource from "../../js/RResource";
export default {
  name: "EmployeeTable",
  components: { BaseTable },
  computed: mapGetters([
    "employees",
    "totalEmployee",
    "singleEmployee",
    "checkedEmployeeIds",
  ]),
  data() {
    return {
      headers: [
        {
          name: RResource.FieldName.employeeCode,
          minWidth: "140px",
        },
        {
          name: RResource.FieldName.employeeName,
          minWidth: "170px",
        },
        {
          name: RResource.FieldName.gender,
          minWidth: "105px",
        },
        {
          name: RResource.FieldName.dateOfBirth,
          minWidth: "120px",
          class: "date",
        },
        {
          name: RResource.FieldName.phoneNumber,
          minWidth:"180px",
        },
        {
          name: RResource.FieldName.email,
          minWidth:"150px",
        },
        {
          name: RResource.FieldName.identityNumber,
          minWidth: "150px",
          title: RResource.ToolTip.identityNumber,
        },
        {
          name: RResource.FieldName.employeePosition,
          minWidth: "120px",
        },
        {
          name: RResource.FieldName.departmentName,
          minWidth: "230px",
        },
        {
          name: RResource.FieldName.bankAccount,
          minWidth: "150px",
        },
        {
          name: RResource.FieldName.bankName,
          minWidth: "400px",
        },
        {
          name: RResource.FieldName.bankBranch,
          minWidth: "240px",
          title: RResource.ToolTip.bankBranch,
        },
      ],
    };
  },
  methods: {
    ...mapActions([
      "changeFormType",
      "toggleModal",
      "setCheckedEmployeeIds",
      "getEmployee",
      "selectEmployee",
      "setDialog",
      "setModalTitle",
      "toggleDialog",
      "setNewEmployeeCode",
    ]),

    /**
     * chỉnh sửa bản ghi
     * @param {*} emp
     * Author: DCTuan (11/01/2024)
     */
    modifyForm(emp) {
      this.setModalTitle(RResource.Text.modifyFormTitle);
      this.changeFormType(FormType.EDIT);
      this.selectEmployee(emp);
      this.toggleModal();
    },

    /**
     * chọn xóa nhân viên
     * @param {*} emp
     * Author: DCTuan (11/01/2024)
     */
    selectDelete(emp) {
      this.selectEmployee(emp);
      this.setDialog({
        type: "delete",
        title: RResource.Dialog.deleteEmployeeTitle,
        message: RResource.Dialog.deleteEmployeeMessage(emp.EmployeeCode),
        action: DialogAction.CONFIRM_DELETE,
      });
    },

    /**
     * chọn nhân bản
     * @param {*} emp
     * Author: DCTuan (11/01/2024)
     */
    selectDuplicate(emp) {
      this.setModalTitle(RResource.Text.addFormTitle);
      this.changeFormType(FormType.STORE);
      let newEmp = JSON.parse(JSON.stringify(emp));
      delete newEmp.EmployeeId;
      this.selectEmployee(newEmp);
      this.setNewEmployeeCode();
      this.toggleModal();
    },

    /**
     * Sau khi click checkbox thead đẩy EmployId vào danh sách ids trong store
     * @param {*} event
     * Author: DCTuan (11/01/2024)
     */
    checkAll(event) {
      for (const emp of this.employees) {
        if (event.target.checked == true) {
          if (!this.checkedEmployeeIds.includes(emp.EmployeeId)) {
            this.setCheckedEmployeeIds(emp.EmployeeId);
          }
        } else {
          this.setCheckedEmployeeIds(emp.EmployeeId);
        }
      }
    },

    /**
     * Sau khi click checkbox tbody đẩy EmployId vào danh sách ids trong store
     * @param {*} emp
     * Author: DCTuan (11/01/2024)
     */
    checkOne(emp) {
      this.setCheckedEmployeeIds(emp.EmployeeId);
    },

    /**
     * lấy dữ liệu nhân viên vào bảng
     * @param {*} emp
     * Author: DCTuan (11/01/2024)
     */
    mapData(emp) {
      var arr = [
        emp.EmployeeCode,
        emp.FullName,
        this.$RHelper.convertGender(emp.Gender),
        this.$RHelper.formatDateTable(emp.DateOfBirth),
        emp.PhoneNumber,
        emp.Email,
        emp.IdentityNumber,
        emp.Positions,
        emp.DepartmentName,
        emp.BankAccount,
        emp.BankName,
        emp.BankBranch,
      ];
      return arr;
    },
  },
};
</script>
