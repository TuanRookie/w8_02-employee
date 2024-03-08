<template>
  <div class="import-excel">
    <div class="warrap-excel">
      <div class="import__header">
        <p v-if="this.nextPage === 0">
          {{ excelImport.Step }}
          {{ excelImport.StepOne }}
        </p>
        <p v-if="this.nextPage === 1">
          {{ excelImport.Step }}
          {{ excelImport.StepSecond }}
        </p>
        <p v-if="this.nextPage === 2">
          {{ excelImport.Step }}
          {{ excelImport.StepThird }}
        </p>
      </div>
      <div class="import__container">
        <div class="import__sidebar">
          <ul class="nav_list-import">
            <a
              @click="this.nextPage = 0"
              to=""
              :class="{
                'nav__list-item-import--selected': this.nextPage === 0,
              }"
              class="nav__list-item-import"
              >{{ excelImport.StepOne }}</a
            >
            <a
              @click="this.nextPage = 1"
              to=""
              class="nav__list-item-import"
              :class="{
                'nav__list-item-import--selected': this.nextPage === 1,
              }"
              >{{ excelImport.StepSecond }}</a
            >
            <a
              @click="this.nextPage = 2"
              :class="{
                'nav__list-item-import--selected': this.nextPage === 2,
              }"
              to=""
              class="nav__list-item-import"
              >{{ excelImport.StepThird }}</a
            >
          </ul>
        </div>
        <div class="import__content">
          <ImportSelectFile
            v-model="fileImport"
            v-if="this.nextPage == 0"
          ></ImportSelectFile>
          <BaseTable
            v-if="this.nextPage == 1"
            :headers="headers"
            :dataList="importEmployee"
            :mapData="mapData"
            :isContext="false"
          />
          <BaseTable
            v-if="this.nextPage == 2"
            :headers="headers"
            :dataList="importEmployee"
            :mapData="mapData"
            :isContext="false"
          />
        </div>
      </div>
      <div class="import__footer">
        <BaseButton
          icon="fa-regular fa-circle-question"
          :isSecondary="true"
          btnText="Giúp"
          id="btn-import-help"
        ></BaseButton>
        <div v-if="this.nextPage == 0" class="import__footer-rigth">
          <BaseButton
            icon="fa-solid fa-right-long"
            :isSecondary="true"
            btnText="Tiếp tục"
            id="btn-import-next"
            iconRight="iconRight"
            @click="onClickNextPage"
          ></BaseButton>
          <BaseButton
            icon="fa-solid fa-ban"
            :isSecondary="true"
            btnText="Hủy bỏ"
            id="btn-import-cancel"
            @click="onClickCancelImport"
          ></BaseButton>
        </div>

        <div v-if="this.nextPage == 1" class="import__footer-rigth">
          <BaseButton
            icon="fa-solid fa-left-long"
            :isSecondary="true"
            btnText="Quay lại"
            id="btn-import-prev"
            @click="onClickPrevPage"
          ></BaseButton>
          <BaseButton
            icon="fa-solid fa-file-import"
            :isSecondary="true"
            btnText="Thực hiện"
            id="btn-import-next"
            iconRight="iconRight"
            @click="onClickPerform"
          ></BaseButton>
          <BaseButton
            icon="fa-solid fa-ban"
            :isSecondary="true"
            btnText="Hủy bỏ"
            id="btn-import-cancel"
            @click="onClickCancelImport"
          ></BaseButton>
        </div>
        <div v-if="this.nextPage == 2" class="import__footer-rigth">
          <BaseButton
            icon="fa-solid fa-ban"
            :isSecondary="true"
            btnText="Đóng"
            id="btn-import-cancel"
            @click="onClickCancelImport"
          ></BaseButton>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import RResource from "../../js/RResource";
import { mapActions, mapGetters } from "vuex";
import BaseButton from "../../components/base/baseButton/BaseButton.vue";
import BaseTable from "../../components/base/baseTable/BaseTable.vue";
import ImportSelectFile from "./ImportSelectFile.vue";
export default {
  name: "EmployeeImport",
  components: { BaseButton, ImportSelectFile, BaseTable },
  computed: mapGetters(["importEmployee"]),
  data() {
    return {
      excelImport: RResource.ExcelImport,
      nextPage: 0,
      fileImport: "",
      countImportSuccess: null,
      countImportFailure: null,
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
          minWidth: "180px",
        },
        {
          name: RResource.FieldName.email,
          minWidth: "150px",
        },
        {
          name: RResource.FieldName.departmentName,
          minWidth: "230px",
        },
        {
          name: RResource.FieldName.state,
          minWidth: "230px",
        },
      ],
    };
  },
  methods: {
    ...mapActions([
      "toggleImport",
      "importToExcel",
      "getEmployee",
    ]),

    /**
     * Nhảy về trang trước
     * Author: DCTuan (11/01/2024)
     */
    onClickPrevPage() {
      this.nextPage--;
    },
    /**
     * Nhảy đến trang sau
     * Author: DCTuan (11/01/2024)
     */
    async onClickNextPage() {
      await this.importToExcel();
      await this.nextPage++;
    },
    /**
     * Nhảy đến trang sau
     * Author: DCTuan (11/01/2024)
     */
    onClickPerform() {
      this.getEmployee();
      this.nextPage++;
    },
    /**
     * Hủy khi hoàn tất hoặc bỏ
     * Author: DCTuan (11/01/2024)
     */
    onClickCancelImport() {
      this.toggleImport();
    },

    /**
     * Đẩy dữ liệu import vào bảng
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
        emp.DepartmentName,
        this.$RHelper.errImport(emp.ImportInvalidErrors),
      ];
      return arr;
    },
  },
};
</script>
<style scoped>
.import-excel {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 10;
  background-color: rgba(0, 0, 0, 0.5);
}
.warrap-excel {
  width: 80%;
  height: 80vh;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 10;
}
.import__header {
  height: 48px;
  line-height: 48px;
  font-size: 14px;
  font-weight: 600;
  font-size: 14px;
  background-color: #eeeff1;
}
.import__container {
  display: flex;
  height: calc(100% - 48px - 48px);
  background-color: #eeeff1;
}
.import__sidebar {
  width: 200px;
  background-color: #eeeff1;
  height: 100%;
  flex-shrink: 0;
  margin-right: 12px;
}
.nav_list-import {
  padding-left: 10px;
}
.import__sidebar .nav__list-item-import {
  list-style: none;
  text-decoration: none;
  display: block;
  padding: 0 16px;
  height: 38px;
  line-height: 38px;
  font-weight: 600;
  font-size: 14px;
  color: #000;
  border: 1px solid #e0e0e0;
}
.nav__list-item-import--selected {
  background-color: #98c1e3;
}
.nav__list-item-import.router-link-active {
  background-color: #98c1e3;
}
.import__container .import__content {
  border: 1px solid #e0e0e0;
  width: calc(100% - 212px);
  height: 100%;
  padding: 8px;
}
.import__footer {
  height: 48px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #eeeff1;
  padding: 0 10px;
}

.import__footer-rigth {
  display: flex;
}
</style>
