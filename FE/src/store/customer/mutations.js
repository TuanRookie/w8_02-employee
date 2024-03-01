import RResource from "@/js/RResource";

const mutations = {
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
}

export default mutations;