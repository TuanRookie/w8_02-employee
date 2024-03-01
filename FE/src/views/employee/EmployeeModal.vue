<template>
  <div class="wrapper__modal">
    <div class="modal modal-add-employee">
      <div class="wrapper__modal-header">
        <div class="wapper__modal-header-left">
          <div class="modal-title">{{ modalTitle }}</div>
          <div class="input-checkbox-wrapper">
            <input
              ref="First"
              tabindex="1"
              class="input-checkbox"
              type="checkbox"
            />
            <label class="input-checkbox-label">{{ text.isCustomer }}</label>
          </div>
          <div class="input-checkbox-wrapper">
            <input
              ref="Last"
              tabindex="21"
              class="input-checkbox"
              type="checkbox"
            />
            <label class="input-checkbox-label">{{ text.isProvider }}</label>
          </div>
        </div>
        <div class="wapper__modal-header-right">
          <div class="icon icon-help" :title="text.helpToolTip"></div>
          <div
            @click="escModal"
            id="btnEsc"
            class="icon icon-close"
            :title="text.escToolTip"
          ></div>
        </div>
      </div>
      <form id="employeeForm" class="wrapper__modal-body">
        <div class="wrapper__modal-body-top">
          <div class="wrapper__modal-body-left w-50">
            <div class="row-flex">
              <BaseInput
                @keydown="focusLast"
                ref="EmployeeCode"
                :inputLabel="fieldName.employeeCode"
                inputWidth="w-40"
                inputType="required"
                v-model="employee.EmployeeCode"
                :errorMess="errorMessage.emptyEmployeeCode"
                :tabIndex="2"
                @isError="checkError"
                @blur="validateData"
              />
              <BaseInput
                :inputLabel="fieldName.employeeName"
                inputWidth="flex-1"
                inputType="required"
                v-model="employee.FullName"
                :errorMess="errorMessage.emptyEmployeeName"
                :tabIndex="3"
                @isError="checkError"
              />
            </div>
            <BaseCombobox
              :inputLabel="fieldName.departmentName"
              :errorMess="errorMessage.emptyDepartmentName"
              :dataList="departments.map((dep) => dep.DepartmentName)"
              :isTick="true"
              className="combobox-deparment"
              inputType="required"
              v-model="employee.DepartmentName"
              @selectAction="selectDepartment"
              :tabIndex="6"
              @isError="checkError"
            />
            <BaseInput
              :inputLabel="fieldName.employeePosition"
              v-model="employee.Positions"
              :errorMess="errors.EmployeePosition"
              :tabIndex="9"
            />
          </div>
          <div class="wrapper__modal-body-right w-50">
            <div class="row-flex">
              <BaseInput
                v-model="dateOfBirth"
                :inputLabel="fieldName.dateOfBirth"
                inputWidth="w-40"
                :errorMess="errorMessage.invalidDateOfBirth"
                :tabIndex="4"
                @isError="checkError"
                inputType="date"
              />
              <div class="input-wrapper flex-1">
                <label class="input-label">{{ fieldName.gender }}</label>
                <div class="input-checkbox-list gender-list">
                  <div class="input-checkbox-wrapper">
                    <input
                      class="input-checkbox"
                      type="radio"
                      value="0"
                      name="Gender"
                      :checked="employee.Gender === 0"
                      tabindex="5"
                      @change="
                        (e) => (employee.Gender = parseInt(e.target.value))
                      "
                    />
                    <label class="input-label">{{ text.male }}</label>
                  </div>
                  <div class="input-checkbox-wrapper">
                    <input
                      class="input-checkbox"
                      type="radio"
                      value="1"
                      name="Gender"
                      :checked="employee.Gender === 1"
                      tabindex="5"
                      @change="
                        (e) => (employee.Gender = parseInt(e.target.value))
                      "
                    />
                    <label class="input-label">{{ text.female }}</label>
                  </div>
                  <div class="input-checkbox-wrapper">
                    <input
                      class="input-checkbox"
                      type="radio"
                      value="2"
                      name="Gender"
                      :checked="employee.Gender === 2"
                      tabindex="5"
                      @change="
                        (e) => (employee.Gender = parseInt(e.target.value))
                      "
                    />
                    <label class="input-label">{{ text.other }}</label>
                  </div>
                </div>
              </div>
            </div>
            <div class="row-flex">
              <BaseInput
                :inputLabel="fieldName.identityNumber"
                inputWidth="w-60"
                v-model="employee.IdentityNumber"
                :errorMess="errors.IdentityNumber"
                :labelTooltip="toolTip.identityNumber"
                :tabIndex="7"
              />
              <BaseInput
                v-model="identityDate"
                :inputLabel="fieldName.identityDate"
                inputWidth="flex-1"
                :errorMess="errorMessage.invalidIdentityDate"
                :tabIndex="8"
                inputType="date"
              />
            </div>
            <BaseInput
              :inputLabel="fieldName.identityPlace"
              v-model="employee.IdentityPlace"
              :errorMess="errors.IdentityPlace"
              :tabIndex="10"
            />
          </div>
        </div>
        <div class="wrapper__modal-body-bottom">
          <BaseInput
            :inputLabel="fieldName.address"
            v-model="employee.Address"
            :errorMess="errors.Address"
            :tabIndex="11"
          />
          <div class="row-flex">
            <BaseInput
              :inputLabel="fieldName.phoneNumber"
              :labelTooltip="toolTip.phoneNumber"
              inputWidth="w-25"
              v-model="employee.PhoneNumber"
              :errorMess="errors.PhoneNumber"
              :tabIndex="13"
              @isError="checkError"
            />
            <BaseInput
              :inputLabel="fieldName.landlineNumber"
              :labelTooltip="toolTip.landlineNumber"
              inputWidth="w-25"
              v-model="employee.LandlineNumber"
              :errorMess="errors.LandlineNumber"
              :placeHolder="placeHolder.landlineNumber"
              :tabIndex="12"
            />
            <BaseInput
              :inputLabel="fieldName.email"
              inputWidth="w-25"
              inputType="email"
              v-model="employee.Email"
              :errorMess="errorMessage.invalidEmail"
              :placeHolder="placeHolder.email"
              :tabIndex="14"
              @isError="checkError"
            />
            <div class="w-25"></div>
          </div>
          <div class="row-flex">
            <BaseInput
              :inputLabel="fieldName.bankAccount"
              inputWidth="w-25"
              v-model="employee.BankAccount"
              :errorMess="errors.BankAccount"
              :tabIndex="15"
            />
            <BaseInput
              :inputLabel="fieldName.bankName"
              inputWidth="w-25"
              v-model="employee.BankName"
              :errorMess="errors.BankName"
              :tabIndex="16"
            />
            <BaseInput
              :inputLabel="fieldName.bankBranch"
              :labelTooltip="toolTip.bankBranch"
              inputWidth="w-25"
              v-model="employee.BankBranch"
              :errorMess="errors.BankBranch"
              :tabIndex="17"
            />
            <div class="w-25"></div>
          </div>
        </div>
      </form>
      <div class="wrapper__modal-footer">
        <BaseButton
          :btnText="text.cancel"
          :isSecondary="true"
          @click="toggleModal"
          @keydown="focusFirst"
          tabIndex="20"
        />
        <div class="btn-action">
          <BaseButton
            :btnText="text.store"
            :isSecondary="true"
            @click="saveData"
            tabIndex="19"
          />
          <BaseButton
            :btnText="text.storeAdd"
            @click="saveAddData"
            tabIndex="18"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapGetters } from "vuex";
import FormType from "../../enums/formType";
import DialogAction from "../../enums/dialogAction.js";
import BaseButton from "../../components/base/baseButton/BaseButton.vue";
import BaseCombobox from "../../components/base/baseCombobox/BaseCombox.vue";
import BaseInput from "../../components/base/baseInput/BaseInput.vue";
import RResource from "../../js/RResource";
export default {
  name: "EmployeeModal",
  components: { BaseButton, BaseInput, BaseCombobox },
  computed: mapGetters([
    "isShowModal",
    "isShowAlert",
    "formType",
    "employee",
    "singleEmployee",
    "departments",
    "alert",
    "modalTitle",
  ]),
  created() {
    this.dateOfBirth = this.$RHelper.formatDateInput(this.employee.DateOfBirth);
    this.identityDate = this.$RHelper.formatDateInput(
      this.employee.IdentityDate
    );
  },
  mounted() {
    //focus vào input mã nhân viên
    this.$refs.EmployeeCode.$el.querySelector("input").focus();
    document.addEventListener("keydown", this.shortcutKey);
  },
  unmounted() {
    document.removeEventListener("keydown", this.shortcutKey);
  },
  props: ["isStore"],
  emits: ["isStoreDone"],
  methods: {
    ...mapActions([
      "changeFormType",
      "toggleModal",
      "setDialog",
      "setAlert",
      "addEmployee",
      "editEmployee",
    ]),

    /**
     * Click button Cất
     * Author:DcTuan (15/01/2024)
     */
    saveData() {
      if (this.formType == this.formTypeModal.STORE_AND_ADD) {
        this.changeFormType(this.formTypeModal.STORE);
      }
      if (this.formType == this.formTypeModal.EDIT_AND_ADD) {
        this.changeFormType(this.formTypeModal.EDIT);
      }
      this.storeEmployee();
    },

    /**
     * Click button Cất và Thêm
     * Author:DcTuan (15/01/2024)
     */
    saveAddData() {
      if (this.formType == this.formTypeModal.STORE) {
        this.changeFormType(this.formTypeModal.STORE_AND_ADD);
      }
      if (this.formType == this.formTypeModal.EDIT) {
        this.changeFormType(this.formTypeModal.EDIT_AND_ADD);
      }
      this.storeEmployee();
    },

    /**
     * cất dữ liệu nhân viên
     * Author:DcTuan (15/01/2024)
     */
    storeEmployee() {
      this.employee.DateOfBirth = this.dateOfBirth;
      this.employee.IdentityDate = this.identityDate;

      //validate
      let isValid = this.validateData();

      this.employee.CreatedBy = "Đinh Công Tuấn";
      this.employee.ModifiedBy = "Đinh Công Tuấn";
      console.log(this.employee);

      if (isValid) {
        if (
          this.formType == this.formTypeModal.STORE ||
          this.formType == this.formTypeModal.STORE_AND_ADD
        ) {
          //thêm nhân viên
          this.addEmployee();
        } else if (
          this.formType == this.formTypeModal.EDIT ||
          this.formType == this.formTypeModal.EDIT_AND_ADD
        ) {
          //sửa nhân viên
          this.editEmployee();
        }
      }
    },

    checkError(isError) {
      this.errorResult = isError;
    },
    /**
     * validate dữ liệu
     * Author:DcTuan (15/01/2024)
     */
    validateData() {
      var isValid = false;
      //Xoá tất cả lỗi của employee
      for (const prop of Object.getOwnPropertyNames(this.errors)) {
        delete this.errors[prop];
      }

      // Mã không được để trống
      if (!this.employee.EmployeeCode) {
        this.errors.EmployeeCode = RResource.ErrorMessage.emptyEmployeeCode;
      } else {
        if (!this.employee.EmployeeCode.match(/(NV-)(\d+)/)) {
          this.errors.EmployeeCode = RResource.ErrorMessage.invalidEmployeeCode;
        }
      }

      // Tên không được để trống
      if (!this.employee.FullName) {
        this.errors.FullName = RResource.ErrorMessage.emptyEmployeeName;
      }

      // Đơn vị không được để trống
      if (!this.employee.DepartmentName) {
        this.errors.DepartmentName = RResource.ErrorMessage.emptyDepartmentName;
      }

      //chuyển empty thành null
      if (this.employee.DateOfBirth == "") {
        this.employee.DateOfBirth = null;
      }

      // Ngày sinh không được lớn hơn hiện tại
      if (this.employee.DateOfBirth) {
        this.employee.DateOfBirth = new Date(this.employee.DateOfBirth);
        let currentDate = new Date();
        if (this.employee.DateOfBirth > currentDate) {
          this.errors.DateOfBirth = RResource.ErrorMessage.invalidDateOfBirth;
        }
      }

      //chuyển empty thành null
      if (this.employee.IdentityDate == "") {
        this.employee.IdentityDate = null;
      }

      // Ngày cấp không được lớn hơn hiện tại
      if (this.employee.IdentityDate) {
        let currentDate = new Date().toISOString().split("T")[0];
        if (this.employee.IdentityDate > currentDate) {
          this.errors.IdentityDate = RResource.ErrorMessage.invalidIdentityDate;
        }
      }

      // Email không hợp lệ
      if (this.employee.Email) {
        let mailFormat =
          /^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)@([a-zA-Z0-9-]+)((\.[a-zA-Z0-9-]{2,3})+)$/;
        if (!this.employee.Email.match(mailFormat)) {
          this.errors.Email = RResource.ErrorMessage.invalidEmail;
        }
      }

      if (this.employee.IdentityNumber) {
        let identityFormat = /^\d+$/;
        if (!this.employee.IdentityNumber.match(identityFormat)) {
          this.errors.IdentityNumber = RResource.ErrorMessage.invalidIdentityNumber;
        }
      }

      // Số điện thoại di động không hợp lệ
      if (this.employee.PhoneNumber) {
        let phoneFormat = /^\d+$/;
        if (!this.employee.PhoneNumber.match(phoneFormat)) {
          this.errors.PhoneNumber = RResource.ErrorMessage.invalidPhoneNumber;
        }
      }

      // Số điện thoại cố định không hợp lệ
      if (this.employee.LandlineNumber) {
        let landlineFormat = /^\(0[0-9]{2}\)[0-9]{7}$/;
        if (!this.employee.LandlineNumber.match(landlineFormat)) {
          this.errors.LandlineNumber = RResource.ErrorMessage.invalidLandlineNumber;
        }
      }

      // Kiểm tra xem có lỗi nào không
      if (Object.keys(this.errors).length == 0) {
        isValid = true;
      } else {
        this.setDialog({
          type:"error",
          title: RResource.Dialog.invalid,
          message: Object.values(this.errors)[0],
          action: DialogAction.DEFAULT,
        });
      }
      return isValid;
    },

    /**
     * combobox chọn phòng ban
     * @param {*} dep
     * Author:DcTuan (15/01/2024)
     */
    selectDepartment(departmentName) {
      for (const dep of this.departments) {
        if (dep.DepartmentName == departmentName) {
          this.employee.DepartmentId = dep.DepartmentId;
        }
      }
    },

    /**
     * chọn nút thoát (X) khỏi form
     * @param {*} dep
     * Author:DcTuan (15/01/2024)
     */
    escModal() {
      // Check dữ liệu trên form đã thay đổi
      if (
        JSON.stringify(this.employee) !== JSON.stringify(this.singleEmployee)
      ) {
        this.setDialog({
          type: "store",
          title: RResource.Dialog.changeDataTitle,
          message: RResource.Dialog.changeDataMessage,
          action: DialogAction.CONFIRM_STORE,
        });
      } else {
        this.toggleModal();
      }
    },

    /**
     * focus từ nút cuối cùng quay về input đầu tiên(focus cancal+tab)
     * @param {*} e
     * Author:DcTuan (15/01/2024)
     */
    focusFirst(e) {
      if (!e.shiftKey && e.which == 9) {
        this.$refs.First.focus();
      }
    },

    /**
     * focus từ input đầu tiên về nút cuối cùng(Shift+ Tab)
     * @param {*} e
     * Author:DcTuan (15/01/2024)
     */
    focusLast(e) {
      if (e.shiftKey && e.which == 9) {
        this.$refs.Last.focus();
      }
    },

    /**
     * xử lí phím tắt
     * @param {*} e
     * Author:DcTuan (15/01/2024)
     */
    shortcutKey(e) {
      if (e.which == 27) {
        this.escModal();
      }
      if (e.ctrlKey && e.key == "F8") {
        this.saveData();
      }
      if (e.ctrlKey && e.key == "F9") {
        this.toggleModal();
      }
    },
  },
  watch: {
    isStore(newValue) {
      if (newValue == true) {
        this.saveData();
        this.$emit("isStoreDone");
      }
    },
  },
  data() {
    return {
      errorResult: false,
      errors: {},
      fieldName: RResource.FieldName,
      text: RResource.Text,
      toolTip: RResource.ToolTip,
      placeHolder: RResource.PlaceHolder,
      formTypeModal: FormType,
      errorMessage: RResource.ErrorMessage,
      dateOfBirth: "",
      identityDate: "",
    };
  },
};
</script>
