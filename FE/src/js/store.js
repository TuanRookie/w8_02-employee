import { createStore } from "vuex";
import axios from "axios";
import * as ApiConFig from "./ApiConfig"
import RResource from "./RResource";

const store = createStore({
  state() {
    return {
      customersApi:[{}],//ds Khach hang
      zoomInSidebar: true,
      toast: "",
      loading:false,
      error:{
        code:0,
        message:"",
      },
      customerCodeDefaul: "00001",
      totalRecord:0,
    };
  },

  mutations: {
    /*
    change toast
    CreatedBy:dcTuan 29/11/2023
    Modify:null
    */
    changeToast(state, toast) {
      state.toast = toast;
      setTimeout(function () {
        state.toast = "";
      }, 3000);
    },
    /*
    change zoomsidebar
    CreatedBy:dcTuan 29/11/2023
    Modify:null
    */
    changeZoomSidebar(state, bool) {
      state.zoomInSidebar = bool;
    },
    /**
     * check Api error
     *  @param{cumtomerApi}
     * CreatedBy:dcTuan 06/12/2023
     * Modify:null
     */
    checkApiError(state,response){
      let error ={
        errorCode:0,
        errorMessage:"",
      };
      if(response){
        if(response.status === 400){
          error.errorCode = 400;
          error.errorMessage = RResource["VN"].error400
        }else if (response.status === 401) {
          error.errorCode = 401;
          error.errorMessage = RResource["VN"].error401;
        } else if(response.status === 403){
          error.errorCode = 403;
          error.errorMessage = RResource["VN"].error403;
        } else if(response.status === 404){
          error.errorCode = 404;
          error.errorMessage = RResource["VN"].error404;
        } else if(response.status === 500){
          error.errorCode = 500;
          error.errorMessage = RResource["VN"].error500;
        }else{
          error.errorCode = 0;
          error.errorMessage = RResource["VN"].errorDownloadData;
        }
      }else{
        error.errorCode = 404;
        error.errorMessage = RResource["VN"].error404;
      }
      state.error.code = error.errorCode;
      state.error.message =  error.errorMessage;
    },
    /**
     * increase customer code
     * Author: dcTuan(12/12/2023)
     * Modify:null
     */
    increaseFixedCustomerCode(state){
      let string = state.customerCodeDefaul;
      let number = parseInt(string,10);
      //Kiểm tra number có phải số hợp lệ không
      if(!isNaN(number)){
        number++;
        //chuyển lại thành chuỗi và thêm 0 nếu cần
        state.customerCodeDefaul = number.toString().padStart(string.length,"0");
      }
    }
    
  }, 
  
  actions:{
    /*
    call api get Customers
    CreatedBy:dcTuan 06/12/2023
    Modify:null
    */
    async getApiCustomers(context){
      this.state.loading = true;
      try{
        let response = await axios.get(ApiConFig.FIXED_CUSTOMERS);
        this.state.customersApi = response.data;
      }catch(error){
        context.commit("checkApiError",error.response);
      }finally{
        this.state.loading = false;
      }
    },
    /*
    call api get Customer by page
    CreatedBy:dcTuan 01/02/2024
    Modify:null
    */
    async getApiCustomersByPage(context,page) {
      this.state.loading = true;
      try {
        let response = await axios.get(
          ApiConFig.FIXED_CUSTOMERS +
            `/filter` + `?pageSize=${page.pageSize}&pageNumber=${page.pageNumber}&customerFilter=${page.customerFilter}`
        );
        if(response.data==""){
          this.state.customersApi = [{}];
          this.state.totalRecord = 0;
        }else{
          this.state.customersApi = response.data.Data;
          this.state.totalRecord = response.data.TotalRecord;
        }
        
        console.log("check",response)
      } catch (error) {
        context.commit("checkApiError", error.response);
      } finally {
        this.state.loading = false;
      }
    },
    /**
     * call api delete FixedCustomer by id
     * CreatedBy:dcTuan 07/12/2023
     *  Modify:null
     */
    async deleteApiCustomers(context,id){
      this.state.loading = true;
      try{
        await axios.delete(ApiConFig.FIXED_CUSTOMERS+`/${id}`);
        context.commit("changeToast", {
          status: true,
          message: "Xóa dữ liệu thành công",
        });
      }catch(error){
        console.error(error);
        context.commit("checkApiError", error.response);
        context.commit("changeToast", {
          status: false,
          message: "Xóa dữ liệu thất bại",
        });
      }finally{
        this.state.loading = false;
      }
    },
    /**
     * call api update FixedCustomer by id
     * CreatedBy:dcTuan 11/12/2023
     *  Modify:null
     */
    async updateApiCustomers(context,customer){
      this.state.loading = true;
      let id = customer.CustomerId;
      try{
        await axios.put(ApiConFig.FIXED_CUSTOMERS +`/${id}`,customer);
          context.commit("changeToast", {
            status: true,
            message: "Cập nhật dữ liệu thành công",
          });
      }catch(error){
        console.error("err",error);
        context.commit("checkApiError", error.response);
        context.commit("changeToast", {
          status: false,
          message: "Cập nhật dữ liệu thất bại",
        });
      }finally{
        this.state.loading = false;
      }
    },
    /**
     * call api create FixedCustomer
     * CreatedBy:dcTuan 11/12/2023
     *  Modify:null
     */
    async createApiCustomers(context,customer){
      this.state.loading = true;
      try{
        await axios.post(ApiConFig.FIXED_CUSTOMERS,customer);
          context.commit("changeToast", {
            status: true,
            message: "Thêm mới khách hàng thành công",
          });
        context.commit("increaseFixedCustomerCode");
      }catch(error){
        console.error(error);
        context.commit("checkApiError", error.response);
        context.commit("changeToast", {
          status: false,
          message: "Thêm mới khách hàng thất bại",
        });
      }finally{
        this.state.loading = false;
      }
    },
  },
  
});

export default store;
