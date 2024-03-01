<template>
  <div class="background"></div>
  <div class="wrapper__form-detail">
    <div class="form-detail">
      <div class="form-detail__container">
        <div class="form-detail__container--title">
          <div class="title">
            <p v-if="edit">Sửa thông tin khách hàng</p>
            <p v-else>Thêm khách hàng</p>
          </div>
          <img
            @click="$emit('closeFormDetail')"
            class="title__close"
            src="../../../assets/main/x-thin.svg"
            alt=""
          />
        </div>
        <div class="form-detail__container--information">
          <div class="information">
            <div class="name">
              <r-input 
              labelName="Mã khách hàng"  
              :redPoint="true"
              ref="firstInput"
              v-model:valueInput="valueCustomerCode"
              :errorContent="errorCustomerCode"
              />
              <r-input 
              labelName="Họ và tên" 
              :redPoint="true" 
              v-model:valueInput="valueCustomerName"
              :errorContent="errorCustomerName"
              />
            </div>
            <r-input 
            labelName="Số điện thoại" 
            v-model:valueInput="valueCustomer.PhoneNumber"
             />
            <r-input 
            labelName="Email" 
            v-model:valueInput="valueCustomer.Email"
            />
          </div>

          <div class="information-detail">
            <div class="date-birth">
              <r-input 
              labelName="Ngày Sinh" 
              inputType="date"
              v-model:valueInput="valueCustomer.DateOfBirth"
              />
              <div class="container-gender">
                <label style="font-size: 17px;">Giới tính</label>
                <div class="gender">
                  <input type="radio" name="gender" value="Nam" v-model="selectedGender" />
                  <label>Nam</label>
                  <input type="radio" name="gender" value="Nữ" v-model="selectedGender" />
                  <label>Nữ</label>
                  <input type="radio" name="gender" value="Khác" v-model="selectedGender" />
                  <label>Khác</label>
                </div>
              </div>
            </div>
            <div class="cmnd">
              <r-input 
              labelName="Số CMND" 
              v-model:valueInput="valueCustomer.IdentityNumber"
              />
              <r-input 
              labelName="Ngày cấp" 
              inputType="date"
              v-model:valueInput="valueCustomer.IdentityDate"
              />
            </div>
            <r-input 
            labelName="Nơi Cấp"
            v-model:valueInput="valueCustomer.IdentityPlace"
            />
          </div>
        </div>
        <div class="form-detail__container--company">
          <div class="debt">
            <r-input 
            labelName="Mức lương cơ bản"
            inputType="number" 
            v-model:valueInput="valueCustomer.DebitAmount"
            />
            <!-- <r-combobox
            select="Gender"
            ></r-combobox> -->
          </div>
          <r-input 
          labelName="Công Ty" 
          v-model:valueInput="valueCustomer.CompanyName"
          />
          <r-input 
          labelName="Địa chỉ" 
          v-model:valueInput="valueCustomer.Address"
          />
        </div>
        <div class="form-detail__container--button">
          <r-button 
          contentButton="Hủy" 
          class="button-cancel"
          @click="$emit('closeFormDetail')" 
          />
          <div class="save">
            <r-button 
            v-if="!edit"
            contentButton="Cất" 
            class="button-add-save"
            @click="handleClickSave()" 
            />
            <r-button 
            v-else
            contentButton="Lưu" 
            class="button-add-save"
            @click="handleClickSave()" 
            />
            <r-button
              v-if="!edit"
              contentButton="Cất và Thêm"
              class="button-save-and-create"
              @click="handleClickSaveAndCreate()"
            />
          </div>
        </div>
      </div>
    </div>
    <RDialog
    v-if="dialogShow"
    @handelCancel="handelCancel"
    @handelSave="handelSaveDialog"
    :content="$RResource['VN'].warningSave"
    :btnWhiteContent="$RResource['VN'].cancel"
    :btnWhiteCenterContent="$RResource['VN'].noSave"
    :btnBlueContent="$RResource['VN'].save"
    />
  </div>
</template>

<script>
import RDialog from '../../base/RDialog.vue';

export default {
  name: "FormDetailComponent",
  components:{
    RDialog,
  },
  props:["edit",],
  data(){
    return{
      valueCustomerCode: "NV-" + this.$store.state.customerCodeDefaul,
      valueCustomerName:"",
      valueCustomer:{},
      selectedGender:"",
      dialogShow: false,
      errorCustomerCode:"",
      errorCustomerName:"",
    }
  },
  watch:{
    /**
     * check change code customer
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    valueCustomerCode(newVal){
      this.checkCustomerCode(newVal);
    },
    /**
     * check change name customer
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    valueCustomerName(newValue) {
      this.errorCustomerName = "";

      if (!this.$RHelper.validationLengthInput(newValue, 5, 25)) {
        this.errorCustomerName = this.$RResource['ERROR_VALIDATE'].errorCustomerName;
      }
    },
    "$store.state.customerCodeDefaul"(){
      this.valueCustomerCode = "NV-" + this.$store.state.customerCodeDefaul;
    },
  },
  created(){
    if(this.edit){
      this.valueCustomer= Object.assign({},this.edit);
      this.valueCustomerCode = this.edit.CustomerCode;
      this.valueCustomerName = this.edit.FullName;
      this.valueCustomer.DateOfBirth = this.$RHelper.formatDateInput(this.valueCustomer.DateOfBirth);
      // this.valueCustomer.DebitAmount = this.$RHelper.formatNumberWithDots(this.valueCustomer.DebitAmount);
      this.valueCustomer.IdentityDate = this.$RHelper.formatDateInput(this.valueCustomer.IdentityDate);
      this.selectedGender = this.$RHelper.convertGender(this.valueCustomer.Gender);
    }
  },
  mounted(){
    this.$refs.firstInput.focusFirstInput();
    this.checkCustomerCode(this.valueCustomerCode);
  },
  unmounted(){
  },
  methods:{
    /**
     * create and update customer
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    async handleClickSave(){
      let customer = Object.assign({},this.valueCustomer);
      customer.CustomerCode = this.valueCustomerCode;
      customer.FullName= this.valueCustomerName;
      if(this.selectedGender==="Nam") customer.Gender = 0;
      else if(this.selectedGender==="Nữ") customer.Gender = 1;
      else if(this.selectedGender==="Khác") customer.Gender = 2;
      else customer.Gender = null;
      
      if(this.edit){
        await this.$store.dispatch("updateApiCustomers",customer);
        this.$emit('closeFormDetail');
        this.$store.dispatch("getApiCustomers");
      }else{
        await this.$store.dispatch("createApiCustomers",customer);
        this.$emit('closeFormDetail');
        this.$store.dispatch("getApiCustomers");
      }
    },
    /**
     * add and create customer
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    async handleClickSaveAndCreate(){
      let customer = Object.assign({},this.valueCustomer);
      customer.CustomerCode = this.valueCustomerCode;
      customer.FullName= this.valueCustomerName;
      if(this.selectedGender==="Nam") customer.Gender = 0;
      else if(this.selectedGender==="Nữ") customer.Gender = 1;
      else customer.Gender = null;
      await this.$store.dispatch("createApiCustomers",customer);
      this.$store.dispatch("getApiCustomers");

      this.valueCustomer= Object.assign({},this.edit);
      this.$emit("update:edit", "");
      Object.keys(this.valueCustomer).forEach(key => {
        this.valueCustomer[key] = "";
      });
      this.valueCustomerName ="";

    },
    /**
     * check validate customer code
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    checkCustomerCode(newValue){
      let checkDuplicate = false;
      if (!this.edit) {
        this.errorCustomerCode = "";
        this.$store.state.customersApi.map((i) => {
          if (i.CustomerCode === newValue) {
            this.errorCustomerCode = this.$RResource['ERROR_VALIDATE'].duplicate;
            checkDuplicate = true;
          }
        });
      }
      if (!checkDuplicate) {
        if (!this.$RHelper.validationLengthInput(newValue, 7, 25)) {
          this.errorCustomerCode = this.$RResource['ERROR_VALIDATE'].errorCustomerName;
        }
      }
    },
  }
};
</script>

<style scoped>
p {
  margin: 0;
}
.background{
  position: fixed;
  top:0;
  left: 0;
  width: 100%;
  height: 100vh;
  background-color: black;
  box-sizing: border-box;
  opacity: 0.7;
}
.wrapper__form-detail {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100vh;
  border-radius: 4px;
  border: 1px solid rgb(158, 158, 158);
  display: flex;
  justify-content: center;
  align-items: center;
}

.form-detail {
  display: flex;
  font-size: 17px;
  /* width: 100%; */
  /* padding: 10px 10px 0 10px; */
  box-sizing: border-box;
  align-items: center;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16);
}
.form-detail__container {
  width: 900px;
  /* height: 700px; */
  background-color: #ffffff;
  border-radius: 4px;
  padding: 19px;
  box-sizing: border-box;
}

.form-detail__container--title {
  font-weight: 700;
  font-size: 24px;
  display: flex;
  justify-content: space-between;
  background-color: #fff;
  width: 100%;
  margin-top: 12px;
}
.title__close {
  width: 35px;
  height: 35px;
  cursor: pointer;
}
.title__close:hover {
  filter: brightness(70%);
  background-color: rgb(189, 189, 189);
  filter: brightness(130%);
}
.form-detail__container--information {
  display: flex;
  justify-content: space-between;
  background-color: #fff;
  gap: 30px;
  margin-top: 23px;
  padding: 0 10px;
}
.information {
  display: flex;
  flex-direction: column;
  width: 50%;
}
.name {
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: space-between;
  gap: 10px;
}
.information-detail {
  display: flex;
  flex-direction: column;
  width: 50%;
}
.date-birth {
  display: flex;
  flex-direction: row;
  gap: 10px;
}
.container-gender{
  display: flex;
  width: 100%;
  height: 77px;
  gap: 8px;
  flex-direction: column;
}
.gender{
  display: flex;
  height: 36px;
  width: 100%;
  align-items: center;
  justify-content: space-around;
}
.gender label{
  align-items: center;
  font-size: 17px;
}
.gender input{
  width: 24px;
  height: 24px;
}
.cmnd {
  display: flex;
  flex-direction: row;
  gap: 10px;
}
.form-detail__container--company {
  display: flex;
  background-color: #fff;
  flex-direction: column;
  padding: 0 10px;
}
.debt{
  display: flex;
  width: 406px;
}

.form-detail__container--button {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  margin-top: 20px;
}
.save {
  display: flex;
  margin-right: 10px;
  gap: 10px;
}


</style>
