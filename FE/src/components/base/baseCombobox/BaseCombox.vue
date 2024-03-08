<template>
  <div class="wrapper__combobox" :class="inputWidth">
    <BaseInput
      ref="input"
      @click="toggleList"
      @keydown.enter="toggleList"
      @keydown.arrow-up="navigateList('up')"
      @keydown.arrow-down="navigateList('down')"
      :modelValue="selectedValue"
      @update:modelValue="filterList"
      :inputLabel="inputLabel"
      :errorMess="errorMess"
      :inputType="inputType"
      :tabIndex="tabIndex"
      :placeHolder="placeHolder"
      icon="combobox-icon icofont-rounded-down"
      @isError="$emit('isError', isError)"
      :orError="orError"
    />
    <div
      v-click-outside="hideDataList"
      class="combobox-data"
      :class="className"
      v-if="isShowList"
      ref="comboboxList"
    >
      <div
        v-for="(item, index) in filterDataList"
        :key="index"
        :class="['data-item', { checked: modelValue == item }]"
        @click="selectItem(item)"
      >
        <div v-if="isTick == true" class="tick">
          <div v-if="modelValue == item" class="icon icon-tick"></div>
        </div>
        <div>{{ item }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseInput from "../baseInput/BaseInput.vue";
export default {
  name: "BaseCombobox",
  components: { BaseInput },
  //[Giá trị ô input,tên class,data từ cha truyền xuống,lỗi,tabIndex,,Danh sách list combobox,]
  props: [
    "modelValue",
    "className",
    "dataList",
    "errorMess",
    "tabIndex",
    "inputLabel",
    "inputType",
    "inputWidth",
    "labelToolTip",
    "placeHolder",
    "isTick",
    "orError",
  ],
  emits: ["selectAction", "isError", "update:modelValue"],
  computed: {
    filterDataList: function () {
      return this.dataList.filter((item) =>
        item.toLowerCase().includes(this.searchValue.toLowerCase())
      );
    },
  },
  methods: {
    /**
     * Ẩn/hiện combobox
     * Author: DCTuan (15/01/2024)
     */
    toggleList() {
      this.isShowList = !this.isShowList;
      this.$emit("update:modelValue", this.selectedValue);
    },

    /**
     * Ẩn combobox data khi ấn bên ngoài
     * Author: DCTuan (15/01/2024)
     */
    hideDataList(e) {
      if (this.isShowList && e.target != this.$refs.button) {
        this.isShowList = false;
      }
    },

    /**
     * Lựa chọn item trong danh sách list combobox
     * @param {*} item
     * Author:DCTuan (15/01/2024)
     */
    selectItem(item) {
      this.toggleList();
      this.$refs.input.$el.querySelector("input").focus();
      this.selectedValue = item;
      this.$emit("update:modelValue", item);
      this.$emit("selectAction", item);
    },

    /**
     * lọc danh sách dữ liệu
     * @param {*} item
     * Author: DCTuan (15/01/2024)
     */
    filterList(modelValue) {
      this.selectedValue = modelValue;
      this.searchValue = modelValue;
      if (this.isShowList == false) {
        this.isShowList = true;
      }
    },

    /**
     * Thao tác với phím tắt combobox
     * @param {*} direction 
     * Author: DCTuan (26/01/2024)
     */
    navigateList(direction) {
      const currentIndex = this.filterDataList.indexOf(this.selectedValue);
      let newIndex;

      if (direction === "up") {
        newIndex =
          currentIndex > 0 ? currentIndex - 1 : this.filterDataList.length - 1;
      } else if (direction === "down") {
        newIndex =
          currentIndex < this.filterDataList.length - 1 ? currentIndex + 1 : 0;
      }

      this.selectedValue = this.filterDataList[newIndex];
      this.$emit("update:modelValue", this.selectedValue);

      let scrollSpeed = 20; // Tốc độ cuộn
      let scrollTop = this.$refs["comboboxList"].scrollTop;
      // Xác định hướng cuộn
      let scrollValue =
        direction === "up"
          ? scrollTop - scrollSpeed
          : scrollTop + scrollSpeed;
      if (currentIndex === 0) {
        scrollValue = 0;
      }
      if (currentIndex === this.filterDataList.length - 1) {
        scrollValue = this.$refs["comboboxList"].scrollWidth;
      }
      // Cuộn đến vị trí mới
      this.$refs["comboboxList"].scrollTo({
        top: scrollValue,
        behavior: "smooth",
      });
    },
  },
  data() {
    return {
      isShowList: false,
      selectedValue: this.modelValue,
      searchValue: "",
    };
  },
};
</script>
