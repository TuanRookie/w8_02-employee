import axios from "axios";
import {API_BASE_URL} from "@/js/ApiConfig.js";


const actions = {
    /**
     * Lấy dữ liệu phòng ban
     * @param {*} context
     * Author:DcTuan (09/01/2024)
     */
    async getDepartment(context) {
      try {
        const res = await axios.get(`${API_BASE_URL}Departments`);
        context.commit("getDepartment", res.data);
      } catch (error) {
        console.log(error);
      }
    },
};
export default actions