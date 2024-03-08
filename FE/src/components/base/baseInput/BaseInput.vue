<template>
  <div class="wrapper__input" :class="inputWidth">
    <label v-if="inputLabel" class="input-label" :title="labelTooltip">
      {{ inputLabel }}
      <span v-if="inputType == 'required'" class="require-mark">*</span>
    </label>
    <div class="input-and-icon">
      <input
        :value="modelValue"
        :class="[
          'input',
          {
            'input-error': isError || orError,
            'input-date': inputType == 'date',
            'input-with-icon': icon,
            
          },
        ]"
        :tabindex="tabIndex"
        :placeholder="placeHolder"
        :readonly="isReadOnly"
        :title="errorMess"
        :type="inputType"
        @blur="validateInputValue"
        @input="onInput"
      />
      <div v-if="icon" :class="['input-icon', icon]"></div>
    </div>
    <span v-if="isError" class="input-error-mess">{{ errorMess }}</span>
  </div>
</template>

<script>
export default {
  name: "BaseInput",
  props: [
    "inputLabel",
    "errorMess",
    "inputWidth",
    "modelValue",
    "inputType",
    "tabIndex",
    "placeHolder",
    "icon",
    "labelTooltip",
    "isReadOnly",
    "orError",
  ],
  emits: ["update:modelValue", "isError"],
  methods: {
    /**
     * trả giá trị input
     * @param {*}
     * Author:DcTuan (15/01/2024)
     */
    onInput(e) {
      this.$emit("update:modelValue", e.target.value);
      this.validateInputValue(e);
    },
    /**
     * Validate
     * Author:DcTuan (15/01/2024)
     */
    validateInputValue(e) {
      let value = e.target.value;
      if (this.inputType == "required") {
        this.isError = value ? false : true;
      }
      if (this.inputType == "email") {
        let emailFormat =
          /^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)@([a-zA-Z0-9-]+)((\.[a-zA-Z0-9-]{2,3})+)$/;
        this.isError = value
          ? value.match(emailFormat)
            ? false
            : true
          : false;
      }
      this.$emit("isError", this.isError);
    },
  },
  data() {
    return {
      isError: false,
    };
  },
};
</script>
