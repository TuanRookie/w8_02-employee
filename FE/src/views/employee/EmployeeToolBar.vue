<template>
  <div class="wrapper__toolbar">
    <div v-if="checkedEmployeeIds.length == 0"></div>
    <div v-else class="toolbar-left">
      <div class="checked-rows-total">
        {{ text.selected }} <b>{{ checkedEmployeeIds.length }}</b>
      </div>
      <div class="clear-select" @click="clearSelect">
        {{ text.clearSelect }}
      </div>
      <BaseButton
        :btnText="text.delete"
        :isSecondary="true"
        icon="icon icon-bin"
        @click="deleteBatch"
      />
    </div>
    <div class="toolbar-right">
      <div class="search-area">
        <BaseInput
          @keyup="searchEmployee"
          v-model="filter.employeeFilter"
          :placeHolder="PlaceHolder.search"
          icon="icofont-search-1"
        />
      </div>
      <BaseButton
        icon="icofont-refresh"
        :btnToolTip="ToolTip.refresh"
        @click="refreshTable"
      />
      <div style="position: relative">
        <BaseButton
          icon="icofont-file-excel"
          :btnToolTip="ToolTip.export"
          @click="clickExcel"
        />
        <div 
        v-if="isExcel" 
        class="popup-excel" 
        v-click-outside="hidePopupExcel"
        >
          <div class="context-menu-item" @click="clickImport">
            <div class="icon icon-import"></div>
            <div class="context-menu-item-text">Nhập khẩu</div>
          </div>
          <div class="context-menu-item" @click="clickExport">
            <div class="icon icon-export"></div>
            <div class="context-menu-item-text">Xuất ra Excel</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapGetters } from "vuex";
import BaseInput from "../../components/base/baseInput/BaseInput.vue";
import BaseButton from "../../components/base/baseButton/BaseButton.vue";
import RResource from "../../js/RResource";
import DialogAction from "../../enums/dialogAction.js";

export default {
  name: "EmployeeToolBar",
  components: { BaseInput, BaseButton },
  computed: mapGetters(["filter", "checkedEmployeeIds"]),
  methods: {
    ...mapActions(["getEmployee", "setFilter", "setDialog", "exportToExcel", "toggleImport"]),

    /**
     * Tìm kiếm nhân viên
     * Author:DcTuan (09/01/2024)
     */
    searchEmployee() {
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
      }
      this.timer = setTimeout(() => {
        this.setFilter({
          pageSize: this.filter.pageSize,
          pageNumber: 1,
          employeeFilter: this.filter.employeeFilter,
        });
        this.getEmployee();
      }, 500);
    },

    /**
     * refresh dữ liệu danh sách nhân viên
     * Author:DcTuan (09/01/2024)
     */
    refreshTable() {
      this.setFilter({
        pageSize: this.filter.pageSize,
        pageNumber: 1,
        employeeFilter: "",
      });

      this.getEmployee();
    },

    /**
     * Đóng mở popup Excel
     * Author:DcTuan (09/01/2024)
     */
    clickExcel() {
      this.isExcel = !this.isExcel;
    },

    /**
     * Xuất dữ liệu ra Excel
     * Author:DcTuan (09/01/2024)
     */
    clickExport() {
      this.exportToExcel();
      this.clickExcel();
    },

    clickImport(){
      this.toggleImport();
      this.clickExcel();
    },

    /**
     * Xóa tất cả lựa chọn
     * Author:DcTuan (09/01/2024)
     */
    deleteBatch() {
      this.setDialog({
        type: "delete",
        title: RResource.Dialog.deleteBatchEmployeeTitle(
          this.checkedEmployeeIds.length
        ),
        message: RResource.Dialog.deleteBatchEmployeeMessage,
        action: DialogAction.CONFIRM_DELETE_BATCH,
      });
    },
    /**
     * Bỏ các lựa chọn trên bảng
     * Author:DcTuan (09/01/2024)
     */
    clearSelect() {
      let checkAll = document.querySelector("table #checkboxAll");
      if (checkAll.checked == true) {
        checkAll.click();
      } else {
        document
          .querySelectorAll("table tbody input:checked")
          .forEach((checkbox) => checkbox.click());
      }
    },

    /**
     * Ẩn/hiện form
     * Author:DcTuan (09/01/2024)
     */
    toggleList() {
      this.isShowList = !this.isShowList;
    },

    hidePopupExcel(e){
      if(this.isExcel && e.target != this.$refs.button ){
        this.isExcel = false;
      }
    },
  },
  data() {
    return {
      isShowList: false,
      text: RResource.Text,
      ToolTip: RResource.ToolTip,
      PlaceHolder: RResource.PlaceHolder,
      isExcel: false,
    };
  },
};
</script>
