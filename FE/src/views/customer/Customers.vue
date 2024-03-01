<template>
  <div>
    <main class="content">
      <div class="customer__header">
        <p>Quản lí khách hàng</p>
        <div class="customer__header--add">
          <button class="button-add" @click="handleClickAddCustomer()">
            Thêm mới nhân viên
          </button>
        </div>
      </div>
      <div class="customer__main">
        <div class="customer__main-search">
          <div class="delete" v-if="shouldDisplayDeleteButton">
            <span style="margin: 0px; font-size: 14px"
              >Đã chọn: {{ numberOfCheckbox }}</span
            >
            <button 
            class="button-unselect"
            @click="setUncheckbox"
            >Bỏ chọn</button>
            <div 
            class="button-delete-all"
            @click="handleClickDeleteAllCustomer"
            >
              <p style="font-size: 14px">Xóa lựa chọn</p>
              <div class="icon-delete-all"></div>
            </div>
          </div>
          
          <div class="search">
            <div class="input-search">
              <input class="input-search__filter" v-model="customerFilter" type="text" placeholder="Search" />
              <div class="input-search__icon">
                <div class="icon-search"></div>
              </div>
            </div>
            <div class="button-reload" @click="handleClickReload()">
              <div class="icon-reload"></div>
            </div>
          </div>
        </div>
        <div class="customer__main-table">
          <div class="table-container">
            <div 
            class="thead" 
            >
              <div class="content-center">
                <input
                  type="checkbox"
                  v-model="checkboxAll"
                  @change="setAllCheckboxArray"
                />
              </div>
              <div>Mã nhân viên</div>
              <div>Tên nhân viên</div>
              <div>Giới tính</div>
              <div>Ngày sinh</div>
              <div>Số điện thoại</div>
              <div>Email</div>
              <div>Dư nợ</div>
              <div>Công ty</div>
              <div>Actions</div>
            </div>
            <template v-if="$store.state.totalRecord !== 0">
            <div
              class="trow"
              :class="{'trow-check':checkboxArray[index]}"
              v-for="(customer, index) in customers"
              :key="customer.CustomerId"
            >
              <div class="content-center" style="padding: 0; display: flex;justify-content: center;">
                <input
                  type="checkbox"
                  :checked="checkboxArray[index]"
                  @change="checkboxArray[index] = !checkboxArray[index]"
                />
              </div>
              <div>{{ customer.CustomerCode }}</div>
              <div>{{ customer.FullName }}</div>
              <div>
                {{ $RHelper.convertGender(customer.Gender) }}
              </div>
              <div style="display: flex; justify-content: center;">
                {{ $RHelper.formatDateTable(customer.DateOfBirth) }}
              </div>
              <div>{{ customer.PhoneNumber }}</div>
              <div>{{ customer.Email }}</div>
              <div style="display: flex; justify-content:flex-end;">
                {{$RHelper.formatNumberWithDots(customer.DebitAmount)}}
              </div>
              <div>{{ customer.CompanyName }}</div>
              <div>
                <div
                style="border: none;"
                class="button-edit"
                @click="handleClickEditCustomer(customer)"
                >
                <div class="icon-edit" style="border: none;"></div>
                </div>
                <div
                style="border: none;"
                class="button-delete"
                @click="handleClickDeleteCustomer(index)"
                >
                <div class="icon-delete" style="border: none;"></div>
                </div>
              </div>
            </div>
          </template>
          <template v-else >
            <div class="no-table">
              <div class="icon-nocontent"></div>
              <span>Không tìm thấy dữ liệu nào phù hợp</span>
            </div>
          </template>
          </div>
        </div>
        <div class="customer__main-footer">
            <div class="footer-left">
              <span>Tổng số: </span>
                <span class="text-bold">{{$store.state.totalRecord}}</span>
                <span> bản ghi</span>
            </div>
            <div class="footer-right">
              <select v-model="pageSize">
                <option value="3">3 bản ghi trên 1 trang</option>
                <option value="10">10 bản ghi trên 1 trang</option>
                <option value="20">20 bản ghi trên 1 trang</option>
                <option value="50">50 bản ghi trên 1 trang</option>
                <option value="100">100 bản ghi trên 1 trang</option>
              </select>

            </div>
          </div>
      </div>
    </main>
    <FormDetailComponent
      v-if="showFormDetail"
      @closeFormDetail="closeFormDetail"
      :edit = "edit"
    />

    <RDialog
      v-if="error.message"
      @handleCancel="handleCancel"
      titleDialog="Cảnh báo"
      :content="error.message"
      :buttonWhiteContent="$RResource['VN'].ok"
      :errorCode="error.code"
    />

    <RDialog
      v-if="dialogShow"
      @handleCancel="handleCancel"
      @handleSave="handleSaveDialog"
      titleDialog="Xóa tài liệu"
      :content="contentDialog"
      :buttonWhiteContent="buttonWhiteContent"
      :buttonBlueContent="buttonBlueContent"
    />
  </div>
</template>

<script>
import FormDetailComponent from "@/components/layout/form-detail/TheFormDetail.vue";
import RDialog from "@/components/base/RDialog.vue";
import debounce from 'lodash/debounce';

export default {
  name: "CustomersComponent",
  components: {
    FormDetailComponent,RDialog,
  },
  data() {
    return {
      customers: [],
      contentDialog:"",
      buttonWhiteContent:"",
      buttonBlueContent:"",
      dialogShow:false,
      showFormDetail: false,
      checkboxAll: false,
      checkboxArray:[],
      numberOfCheckbox:0,
      error: {
        code: 0,
        message: "",
      },
      edit:"",
      pageSize:100,
      pageNumber:1,
      customerFilter:"",
      previousFilter: null,
    };
  },
  created(){
    this.getApiFixedCustomers();
  },
  mounted(){
    this.$store.dispatch("getApiCustomersByPage", {
      pageSize: this.pageSize,
      pageNumber: this.pageNumber,
      customerFilter:this.customerFilter,
    });
  },
  computed: {
    shouldDisplayDeleteButton() {
      return this.checkboxArray.some(value => value === true);
    }
  },
  watch: {
    /*
    number checkbox row table
    Author:dcTuan(05/12/2023)
    Modify:null
    */
    checkboxArray: {
      handler(newVal) {
        this.numberOfCheckbox = newVal.filter((value) => value === true).length;
      },
      deep: true, // Đảm bảo theo dõi sâu hơn trong mảng
    },
    /**
     * change assets by assetsApi in store
     * Author: DcTuan(06/12/2023)
     * Modify: null
     */
    "$store.state.customersApi"(){
      this.customers = this.$store.state.customersApi;
      this.checkboxArray = new Array(this.customers.length).fill(false);
    },
    /*
    check change error in store
    Author: DcTuan(06/12/2023)
    Modify: null
    */
    "$store.state.error.message"(){
      this.error.code = this.$store.state.error.code;
      this.error.message = this.$store.state.error.message;
    },
    /**
     * check change pageSize
     * Author: DcTuan(01/02/2024)
     * Modify: null
     */
    pageSize(newValue) {
      this.$store.dispatch("getApiCustomersByPage", { pageSize: newValue, pageNumber:this.pageNumber, customerFilter:this.customerFilter});
    },
    /**
     * check change pageNumber
     * Author: DcTuan(01/02/2024)
     * Modify: null
     */
    pageNumber(newValue){
      this.$store.dispatch("getApiCustomersByPage", { pageSize: this.pageSize, pageNumber:newValue, customerFilter:this.customerFilter});
    },
    /**
     * check change stringFilter
     * Author: DcTuan(01/02/2024)
     * Modify: null
     */
     customerFilter: debounce(function(newValue) {
      this.$store.dispatch("getApiCustomersByPage", {
        pageSize: this.pageSize,
        pageNumber: this.pageNumber,
        customerFilter: newValue,
      });
    }, 3000),
    
  },
  methods: {
    /**
     * get Api FixedCustomer and assigned to assets
     * Author: dcTuan (06/12/2023)
     * Modify: null
     */
    async getApiFixedCustomers() {
      await this.$store.dispatch("getApiCustomers");
      this.customers = this.$store.state.customersApi;
    },
    /*
    close Form Detail
    Author:dcTuan(25/11/2023)
    Modify:null
    */
    closeFormDetail() {
      this.showFormDetail = false;
      this.edit ="";
    },
    /**
     * set all boolean array reverse
     * Author:dcTuan (28/11/2023)
     * Modify:null
     */
     setAllCheckboxArray() {
      this.checkboxArray = this.checkboxArray.map((i) => {
        if(i===this.checkboxAll){
          return i;
        } else{
          return !i;
        }
      });
    },
    /**
    * set all boolean array uncheckbox
    * Author:dcTuan (28/11/2023)
    * Modify:null
    */
    setUncheckbox(){
      this.checkboxAll=false;
      this.checkboxArray = new Array(this.customers.length).fill(false);
    },
    /**
     * cancel form
     * Author:dcTuan (28/11/2023)
     * Modify:null
     */
     handleCancel() {
      this.error.message = "";
      this.dialogShow = false;
    },
    /**
     * event dialog
     * Author:dcTuan (28/11/2023)
     * Modify:null
     */
      async handleSaveDialog() {
      let arrDelete=[];
      let deletePromises = [];
      this.checkboxArray.forEach((i, index) => {
        if (i === true) {
          let id = this.customers[index].CustomerId;
          let name = this.customers[index].FullName;
          let code = this.customers[index].CustomerCode;
          arrDelete.push({ id: id, name: name ,code: code});
          deletePromises.push(this.$store.dispatch("deleteApiCustomers", id));
        }
      });
      try {
        await Promise.all(deletePromises); // Chờ tất cả các yêu cầu xóa hoàn tất
        await this.$store.dispatch("getApiCustomersByPage"); // Cập nhật dữ liệu sau khi xóa hoàn tất
        this.dialogShow = false;
      } catch (error) {
        console.error("Error occurred: ", error);
        // Xử lý lỗi nếu có
      }
     },

    /**
    * event delete customers
    * Author:dcTuan (11/11/2023)
    * Modify:null
    */
    handleClickDeleteAllCustomer(){
      let arrDelete=[];
      this.checkboxArray.forEach(async (i, index) => {
        if (i === true) {
          let id = this.customers[index].CustomerId;
          let name = this.customers[index].FullName;
          let code = this.customers[index].CustomerCode;
          arrDelete.push({ id: id, name: name ,code: code});
        }
      });

      if(arrDelete.length ===1){
        this.contentDialog =
          this.$RResource["VN"].deleteOneContent +
          "<<" +
          arrDelete[0].name +
          ">> ?";
        this.buttonBlueContent = this.$RResource["VN"].delete;
        this.buttonWhiteContent = this.$RResource["VN"].cancel;
      }  else if (arrDelete.length === 0) {
        this.contentDialog = this.$RResource["VN"].deleteNullContent;
        this.buttonWhiteContent = this.$RResource["VN"].ok;
        this.buttonBlueContent="";
      } else {
        this.contentDialog =
          arrDelete.length + " " + this.$RResource["VN"].deleteManyContent;
        this.buttonBlueContent = this.$RResource["VN"].delete;
        this.buttonWhiteContent = this.$RResource["VN"].cancel;
      }

      this.dialogShow = true;
    },
    /**
    * event delete customer
    * Author:dcTuan (12/12/2023)
    * Modify:null
    */
    handleClickDeleteCustomer(index){
      this.checkboxArray = new Array(this.customers.length).fill(false);
      this.checkboxArray[index] = true;
      this.dialogShow = true;
      this.contentDialog =
          this.$RResource["VN"].deleteOneContent +
          "<<" +
          this.customers[index].FullName +
          ">> ?";
      this.buttonBlueContent = this.$RResource["VN"].delete;
      this.buttonWhiteContent = this.$RResource["VN"].cancel;
    },
    /**
     * handle Click Add
     * Author: dcTuan(06/12/2023)
     * Modify:null
     */
     handleClickAddCustomer(){
      this.showFormDetail = true;
    },
    /**
     * handle Click Edit
     * Author: dcTuan(06/12/2023)
     * Modify:null
     */
    handleClickEditCustomer(customer){
      this.showFormDetail = true;
      this.edit = customer;
    },
    /**
     * handle Click Edit
     * Author: dcTuan(06/12/2023)
     * Modify:null
     */
    handleClickReload(){
      this.$store.dispatch("getApiCustomersByPage", { pageSize: this.pageSize, pageNumber:this.pageNumber, customerFilter:this.customerFilter});
    }
  },
};
</script>

<style scoped>
.content {
  background-color: #f5f5f5;
  padding: 0px 20px;
}
.customer__main {
  background-color: #ffffff;
  padding: 20px 20px;
  border-radius: 4px;
}
.customer__header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0px;
}
.customer__header p {
  font-weight: 700;
  font-size: 20px;
  size: 20px;
}

.customer__main-search {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
  height: 40px;
}

.delete {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.search {
  display: flex;
  gap: 10px;
  align-items: center;
  margin-left: auto;
}
.input-search__icon{
  display: flex;
  align-items: center;
  justify-content: center;
  position: absolute;
  width: 36px;
  height: 36px;
  background-color: rgb(250, 250, 250);
  margin-left: 1px;
}

.no-table{
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 14px;
  flex-direction: column;
}

.customer__main-table {
  font-size: 14px;
  height: 70vh;
}
.table-container {
  background-color: #ffffff;
  border: 1px solid #afafaf;
  overflow-y: scroll;
  overflow-x: hidden;
  position: relative;
  /* ẩn nội dung vươt quá kích thước */
  border: none;
  height: 64.9vh;
  padding-top: 49px;
}
.table-container:hover{
  box-shadow: 0 3px 10px rgba(0, 0, 0, 0.16);
}
.content-center input {
  width: 24px;
  height: 24px;
}
.thead {
  position: fixed;
  background-color: #f5f5f5;
  height: 48px;
  font-weight: 700;
  font-size: 14px;
  align-items: stretch;
  display: grid;
  grid-template-columns: 2vw 6vw 10vw 5vw 8vw 8vw 15.4vw 9vw 12.8vw 8vw;
  border-bottom: 1px solid #eeeeee;
  margin-top: -49px;
}
.zoomIn-thead{
  grid-template-columns: 2.6vw 6.6vw 10.6vw 5.6vw 8.6vw 8.6vw 16.6vw 9.6vw 13.5vw 8.6vw;
}
.thead div {
  display: flex;
  border-left: 1px solid #eeeeee;
  align-items: center;
  justify-content: center;
}
.trow {
  display: grid;
  align-items: stretch;
  font-weight: 400;
  font-size: 14px;
  height: 48px;
  grid-template-columns: 2vw 6vw 10vw 5vw 8vw 8vw 15.4vw 9vw 12.8vw 8vw;
  border-bottom: 1px solid #eeeeee;
  box-sizing: border-box;
}
.trow:hover{
  background-color:#f5f5f5 ;
}
.trow-check{
  background-color: rgb(156, 213, 179);
}
.zoomIn-trow{
  grid-template-columns: 2.6vw 6.6vw 10.6vw 5.6vw 8.6vw 8.6vw 16.6vw 9.6vw 13.5vw 8.6vw;
}
.trow div {
  display: flex;
  border-left: 1px solid #eeeeee;
  border-top: none;
  align-items: center;
  justify-content: flex-start;
  padding: 0 16px;
}
.customer__main-footer{
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-top: 1px solid #eeeeee;
  padding-top: 20px;
}
.text-bold{
  font-weight: 700;
}

</style>
