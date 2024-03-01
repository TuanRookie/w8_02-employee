<template>
    <div  class="input__container" >
        <label class="label">{{labelName}} <span v-if="redPoint" class="lab-mts">*</span></label>
        <input 
        ref="input"
        @focus="handelFocusInput"
        class="input"
        :type ="inputType"
        :placeholder="inputPlaceHolder"
        v-model="value"
    
        :class="{
            'input-required': required && !value,
            'readonly':isReadOnly,
            'input-number': inputType==='number',
        }"
        :step="step"
        :readonly="isReadOnly"
        />
        <span class="error" >{{ errorContent }}</span>
        <span v-show="requiredContent" class="error"  >{{ requiredContent }}</span>
    </div>
</template>

<script>

export default{
    name: "RInput",
    props:[
        "labelName",
        "inputType",
        "inputPlaceHolder",
        "valueInput",
        // "handelChangeValue",
        "errorContent",
        "step",
        "isReadOnly",
        "redPoint",
    ],
    data() {
        return {
            value:this.valueInput,
            required: false,
            requiredContent : "",
        }
    },
    watch: {
    value(newValue) {
      //update 2 chiều input
      this.$emit("update:valueInput", newValue);
      if (!this.value && this.required) {
        this.requiredContent = "Cần phải nhập thông tin";
      } else {
        this.requiredContent = "";
      }
      if (this.inputType === "number") {
        console.log(newValue)
      }
    },
    //
    valueInput(newValue) {
      this.value = newValue;
    },
  },
    methods: {
        /*
        handel focus input
        Author:dcTuan(26/11/2023)
        Modify:null
        */
        handelFocusInput() {
            this.required = true;
            if(!this.value && this.required){
                this.requiredContent = "Bạn cần phải nhập thông tin"
            }
        },
        /*
        focus first Input
        Author: dcTuan(06/12/2023)
        */
        focusFirstInput(){
          this.$refs.input.focus();
        },
    }
}

</script>

<style scoped>
.input__container{
    display: flex;
    flex-direction: column;
    gap: 8px;
    width: 100%;
}
.error {
  color: red;
  font-size: 14px;
  width: fit-content;
}
.lab-mts{
  color: red;
}

</style>