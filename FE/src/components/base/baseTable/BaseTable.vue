<template>
  <div class="wrapper__table" :class="tableName">
    <div class="wrapper__table-content">
      <table>
        <thead>
          <tr @mouseover="showFunctionButtons($event, {})">
            <th>
              <input
                ref="checkboxAll"
                id="checkboxAll"
                @change="checkAll"
                class="input-checkbox"
                type="checkbox"
              />
            </th>
            <th
              v-for="(header, index) in headers"
              :key="index"
              :class="header.class"
              :style="{ 'min-width': header.minWidth }"
              :title="header.title"
            >
              {{ header.name }}
            </th>
            <!-- <th class="function" style="min-width: 120px">
              {{ Text.function }}
            </th> -->
          </tr>
        </thead>
        <tbody v-if="isShowLoading">
          <tr v-for="index in 4" :key="index">
            <td>
              <input
                ref="checkbox"
                @change="checkOne(data, index)"
                class="input-checkbox"
                type="checkbox"
              />
            </td>
            <td v-for="index in headers.length" :key="index">
              <div class="loading"></div>
            </td>
            <td><div class="loading"></div></td>
          </tr>
        </tbody>
        <tbody v-else>
          <tr
            v-for="(data, index) in dataList"
            :key="index"
            :class="{ 'row-checked': checkedRows[index] == true }"
            @mouseover="showFunctionButtons($event, data)"
            @click="rowClick(index)"
            @dblclick="modifyForm(data)"
          >
            <td @click.stop @dblclick.stop>
              <input
                ref="checkbox"
                @change="checkOne(data, index)"
                class="input-checkbox"
                type="checkbox"
              />
            </td>
            <td
              v-for="(value, index) in mapData(data)"
              :key="index"
              :class="headers[index].class"
            >
              {{ value }}
            </td>
            <!-- <td @dblclick.stop :style="{ 'z-index': 3 }">
              <div class="table-function">
                <div class="modify" @click="modifyForm(data)">
                  {{ Text.modify }}
                </div>
                <div class="more-functions">
                  <div
                    class="icon dropdown-button icon-arrow-down-blue"
                    @click="toggleListContext(index)"
                  ></div>
                  <div
                    ref="contextMenu"
                    class="function-list context-menu"
                    :style="{
                      visibility: isShowListContext[index]
                        ? 'visible'
                        : 'hidden',
                    }"
                  >
                    <div
                      @click="selectDuplicate(selectedData)"
                      class="context-menu-item"
                    >
                      <div class="context-menu-item-icon icofont-ui-copy"></div>
                      <div class="context-menu-item-text">
                        {{ Text.duplicate }}
                      </div>
                    </div>
                    <div
                      @click="selectDelete(selectedData)"
                      class="context-menu-item"
                    >
                      <div class="context-menu-item-icon icofont-bin"></div>
                      <div class="context-menu-item-text">
                        {{ Text.delete }}
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </td> -->
          </tr>
        </tbody>
      </table>
      <div
        v-if="isContext"
        class="function-buttons"
        :style="{ top: top - 210 + 'px' }"
      >
        <button
          class="btn-secondary btn-modify"
          :title="Text.modify"
          @click="modifyForm(selectedData)"
        >
          <div class="icofont-ui-edit"></div>
        </button>
        <div class="more-functions">
          <button class="btn-secondary btn-more" @click="toggleList">
            <div class="icofont-navigation-menu"></div>
          </button>
          <div
            ref="contextMenu"
            class="function-list context-menu"
            :style="{
              top: !reversePosition ? 'calc(100% + 2px )' : 'auto',
              bottom: reversePosition ? 'calc(100% + 2px )' : 'auto',
              visibility: isShowList ? 'visible' : 'hidden',
            }"
          >
            <div
              @click="selectDuplicate(selectedData)"
              class="context-menu-item"
            >
              <div class="context-menu-item-icon icofont-ui-copy"></div>
              <div class="context-menu-item-text">{{ Text.duplicate }}</div>
            </div>
            <div @click="selectDelete(selectedData)" class="context-menu-item">
              <div class="context-menu-item-icon icofont-bin"></div>
              <div class="context-menu-item-text">{{ Text.delete }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import RResource from "../../../js/RResource";
export default {
  name: "BaseTable",
  //[thead string,data bản ghi,lấy dữ liệu dât emit,tên bảng]
  props: ["headers", "dataList", "mapData", "tableName", "isContext"],
  //[edit,Nhân bản,Xoá,checkbox thead,checkbox tbody]
  emits: [
    "modifyForm",
    "selectDuplicate",
    "selectDelete",
    "checkAll",
    "checkOne",
  ],
  computed: mapGetters(["isShowLoading"]),
  methods: {
    /**
     * hiển thị thanh chức năng
     * @param {*} e
     * @param {*} data
     * Author: DCTuan (11/01/2024)
     */
    showFunctionButtons(e, data) {
      if (this.isContext == true) {
        // Lấy vị trí top của phần tử mà chuột đang di chuyển qua
        this.top = e.target.getBoundingClientRect().top;
        // Lưu trữ dữ liệu của phần tử hiện tại
        this.selectedData = data;
        // Nếu đã hiển thị danh sách (isShowList là true), đặt isShowList về false để ẩn đi
        if (this.isShowList == true) {
          this.isShowList = false;
        }
        // Kiểm tra vị trí của phần tử so với bottom của phần tử chứa contextMenu
        if (
          e.target.getBoundingClientRect().bottom +
            this.$refs.contextMenu.clientHeight >
          this.$el.getBoundingClientRect().bottom
        ) {
          // Nếu phần tử nằm gần cuối trang, đặt reversePosition về true
          this.reversePosition = true;
        } else {
          // Ngược lại, đặt reversePosition về false
          this.reversePosition = false;
        }
      }
    },

    /**
     * chỉnh sửa dữ liệu
     * @param {*} data
     * Author: DCTuan (11/01/2024)
     */
    modifyForm(data) {
      this.$emit("modifyForm", data);
    },

    /**
     * chọn nhân bản
     * @param {*} data
     * Author: DCTuan (11/01/2024)
     */
    selectDuplicate(data) {
      this.$emit("selectDuplicate", data);
    },

    /**
     * chọn xóa dữ liệu
     * @param {*} data
     * Author: DCTuan (11/01/2024)
     */
    selectDelete(data) {
      this.$emit("selectDelete", data);
    },

    /**
     * thao tác với checkbox thead
     * @param {*} event
     * Author: DCTuan (11/01/2024)
     */
    checkAll(event) {
      this.$refs["checkbox"].forEach((checkbox) => {
        checkbox.checked = event.target.checked;
      });
      this.checkedRows = new Array(this.dataList.length).fill(
        event.target.checked
      );
      this.$emit("checkAll", event);
    },

    /**
     * thao tác với checkbox tbody
     * Author: DCTuan (11/01/2024)
     */
    checkOne(data, index) {
      this.$refs["checkboxAll"].checked =
        document.querySelectorAll("tbody input:checked").length ==
        this.dataList.length;
      this.$emit("checkOne", data);
      this.checkedRows[index] = !this.checkedRows[index];
    },

    /**
     * Ẩn/hiện dropdown
     * @param {*} event
     * Author: DCTuan (11/01/2024)
     */
    toggleList() {
      this.isShowList = !this.isShowList;
    },

    // toggleListContext(index) {
    //   this.isShowListContext[index] = !this.isShowListContext[index];
    // },

    /**
     * click vào hàng thay đổi checkboxOne
     * @param {*} index
     * Author: DCTuan (11/01/2024)
     */
    rowClick(index) {
      this.$refs["checkbox"][index].click();
    },
  },

  watch: {
    dataList: function () {
      this.checkedRows = new Array(this.dataList.length).fill(false);
      this.isShowListContext = new Array(this.dataList.length).fill(false);
      this.$refs["checkbox"].forEach((checkbox) => (checkbox.checked = false));
      this.$refs["checkboxAll"].checked = false;
    },
  },
  data() {
    return {
      Text: RResource.Text,
      checkedRows: [],
      top: 400,
      selectedData: {},
      isShowList: false,
      // isShowListContext: [],
      reversePosition: false,
    };
  },
};
</script>
