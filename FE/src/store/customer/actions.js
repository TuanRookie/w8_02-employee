import * as ApiConFig from "@/js/ApiConfig.js";
import axios from "axios";

const actions ={
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
}

export default actions;