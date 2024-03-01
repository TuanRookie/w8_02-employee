<template>
  <div class="r-combobox" ref="rCombobox">
    <div class="r-combobox__header">
      <input
        class="input-combobox"
        type="text"
        @focus="handleFocus"
        @input="handleChangeSelect"
        v-model="comSelect"
      />
      <div class="arrow-down" @click="open = !open">
        <div class="icon-arrow-down"></div>
      </div>
    </div>
    <div class="r-combobox__body" :class="{ 'r-combobox__body-close': !open }">
      <div
        class="row"
        v-for="(option, index) in comeOptions"
        :key="index"
        :class="{ 'row-select': tick[option] }"
      >
        <div class="row__container" @click="handleClickOption(option)">
          <div :class="{ hidden: !tick[option] }" class="design-tick">
            <div class="icon-tick"></div>
          </div>
          <span>{{ option }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "RCombobox",
  props: ["select"],
  data() {
    return {
      comSelect: this.select,
      options: [],
      comeOptions: [],
      tick: {},
      open: false,
      clickRow: false,
    };
  },
  created() {
    this.options = ["Nam", "Nữ", "Khác"];
    this.comeOptions = this.options;
    for (const item of this.options) {
      this.tick[item] = false; // tạo tất cả các tick option trong options bằng false
    }
  },
  mounted() {
    document.addEventListener("click", this.outSideClick);
    if(this.selectOption){
      this.handelClickOption(this.selectOption)
      document.removeEventListener("click", this.outSideClick);
      this.open = false
    }
    document.addEventListener("keydown", this.handleKeyDown);
  },
  unmounted() {
    document.removeEventListener("click", this.outSideClick);
    document.removeEventListener("keydown", this.handleKeyDown);
  },
  methods: {
    /*
    focus select
    Created:dcTuan 05/12/2023
    Modify:null
    */
    handleFocus() {
      this.comSelect = "";
    },
    /*
    change select
    Created:dcTuan 05/12/2023
    Modify:null
    */
    handleChangeSelect() {
      this.open = true;
      this.comeOptions = this.options.filter((item) =>
        item.toLowerCase().startsWith(this.comSelect.toLowerCase())
      );
    },
    /**
     * handle click option
     * Created:dcTuan 05/12/2023
     * Modify:null
     */
    handleClickOption(option) {
      for (const key in this.tick) {
        if (typeof this.tick[option] === "boolean") {
          this.tick[key] = false;
        }
      }
      this.tick[option] = true;
      this.comSelect = option;
      this.clickRow = true;
      this.open = !this.open;
    },

    /**
     * event click out combobox
     * Created:dcTuan 05/12/2023
     * Modify:null
     */
    outSideClick(event) {
      if (!this.$refs.rCombobox.contains(event.target)) {
        if (!this.clickRow) {
          for (const item of this.options) {
            if (this.tick[item] === true) {
              this.comSelect = item;
              break;
            }
            this.comSelect = this.select;
          }
        }
        this.clickRow = false;
        this.comOptions = this.options;
        this.open = false;
      }
    },
    handleKeyDown(event){
      if (event.key === "ArrowDown" || event.key === "ArrowUp") {
        event.preventDefault();
        this.handleArrowKeyPress(event.key);
      }else if (event.key === "Enter") {
        event.preventDefault();
        this.handleEnterKeyPress();
      }
    },
    handleArrowKeyPress(key) {
      if (key === "ArrowDown" && this.open) {
        // Move selection down the list
        const currentIndex = this.comeOptions.indexOf(this.comSelect);
        this.tick[this.comeOptions[currentIndex]]=false;
        const newIndex = currentIndex === this.comeOptions.length - 1 ? 0 : currentIndex + 1;
        this.comSelect = this.comeOptions[newIndex];
        this.tick[this.comSelect]=true;
      } else if (key === "ArrowUp" && this.open) {
        // Move selection up the list
        const currentIndex = this.comeOptions.indexOf(this.comSelect);
        this.tick[this.comeOptions[currentIndex]]=false;
        const newIndex = currentIndex === 0 ? this.comeOptions.length - 1 : currentIndex - 1;
        this.comSelect = this.comeOptions[newIndex];
        this.tick[this.comSelect]=true;
      }
    },
    handleEnterKeyPress() {
      if (this.open) {
        if (this.comSelect !== this.select) {
          this.clickRow = true;
        }
        this.open = false;

      }
    },
  },
};
</script>
<style scoped>
@import url(../../css/base/combobox.css);
</style>
