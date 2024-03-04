import FormType from "@/enums/formType";
import Gender from "@/enums/gender";
import { API_BASE_URL } from "@/js/ApiConfig.js";
import axios from "axios";
import state from "./state";
import RResource from "@/js/RResource";
import router from "@/routers/router";
const actions = {
  /**
   * Đặt tiêu đề cho form nhân viên
   * @param {*} context
   * @param {*} title
   * Author:DcTuan (09/01/2024)
   */
  setModalTitle(context, title) {
    context.commit("setModalTitle", title);
  },

  /**
   * đổi chế độ của form
   * @param {*} context
   * @param {*} type
   * Author:DcTuan (09/01/2024)
   */
  changeFormType(context, type) {
    context.commit("changeFormType", type);
  },

  /**
   * Ẩn/hiện form Modal
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  toggleModal(context) {
    context.commit("toggleModal");
  },

  /**
   * Ẩn hiện form Import
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  toggleImport(context) {
    context.commit("toggleImport");
  },

  /**
   * lấy danh sách những nhân viên được chọn
   * @param {*} context
   * @param {*} id
   * Author:DcTuan (09/01/2024)
   */
  setCheckedEmployeeIds(context, id) {
    context.commit("setCheckedEmployeeIds", id);
  },

  /**
   * Xử lí bộ lọc danh sách nhân viên
   * @param {*} context
   * @param {*} filter
   * Author:DcTuan (09/01/2024)
   */
  setFilter(context, filter) {
    context.commit("setFilter", filter);
  },

  setFileImport(context, file) {
    context.commit("setFileImport", file);
  },

  getImportEmployee(context, employees) {
    context.commit("getImportEmployee", employees);
  },

  /**
   * Lấy dữ liệu nhân viên
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  async getEmployee(context) {
    const token = JSON.parse(localStorage.getItem("Token"));
    let now = new Date();
    let expriedAccessTokenString = token.ExpiredAccessToken;
    let expriedAccessToken = new Date(expriedAccessTokenString);
    if (token && expriedAccessToken <= now) {
      context.dispatch("getRefreshToken");
    }
    if (token) {
      try {
        context.dispatch("toggleLoading");
        const res = await axios.get(`${API_BASE_URL}Employees/filter`, {
          headers: {
            Authorization: `Bearer ${token.AccessToken}`,
          },
          params: {
            pageSize: state.filter.pageSize,
            pageNumber: state.filter.pageNumber,
            employeeFilter: state.filter.employeeFilter,
          },
        });
        context.dispatch("toggleLoading");
        context.commit("getEmployee", res.data);
      } catch (error) {
        console.error(error);
      }
    }
  },

  /**
   * Lấy mã nhân viên mới
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  async setNewEmployeeCode(context) {
    try {
      const res = await axios.get(`${API_BASE_URL}Employees/NewEmployeeCode`);
      context.commit("setNewEmployeeCode", res.data);
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Thêm nhân viên
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  async addEmployee(context) {
    try {
      await axios.post(`${API_BASE_URL}Employees`, state.employee);
      //thông báo thành công
      context.dispatch("setAlert", {
        type: "success",
        message: RResource.AlertMessage.addEmployeeSuccess,
      });

      // Check type
      if (state.formType == FormType.STORE) {
        // Cất
        context.dispatch("toggleModal");
      } else {
        // Cất và thêm
        context.dispatch("changeFormType", FormType.STORE);
        context.dispatch("selectEmployee", { Gender: Gender.MALE });
        context.dispatch("setNewEmployeeCode");
      }

      //load lại dữ liệu
      context.dispatch("getEmployee");
    } catch (error) {
      handleException(error, context);
    }
  },

  /**
   * Chỉnh sửa nhân viên
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  async editEmployee(context) {
    try {
      await axios.put(
        `${API_BASE_URL}Employees/${state.employee.EmployeeId}`,
        state.employee
      );

      //thông báo thành công
      context.dispatch("setAlert", {
        type: "success",
        message: RResource.AlertMessage.editEmployeeSuccess,
      });

      // Check type
      if (state.formType == FormType.EDIT) {
        // Cất
        context.dispatch("toggleModal");
      } else {
        // Cất và thêm
        context.dispatch("setModalTitle", "Thêm khách hàng");
        context.dispatch("changeFormType", FormType.STORE);
        context.dispatch("selectEmployee", { Gender: Gender.MALE });
        context.dispatch("setNewEmployeeCode");
      }

      //load lại dữ liệu
      context.dispatch("getEmployee");
    } catch (error) {
      handleException(error, context);
    }
  },

  /**
   * Xóa nhân viên
   * @param {*} context
   * @param {string} id
   * Author:DcTuan (09/01/2024)
   */
  async deleteEmployee(context) {
    try {
      await axios.delete(
        `${API_BASE_URL}Employees/${state.employee.EmployeeId}`
      );

      //thông báo thành công
      context.dispatch("setAlert", {
        type: "success",
        message: RResource.AlertMessage.deleteEmployeeSuccess,
      });

      //Quay về trang đầu tiên nếu xóa hết bản ghi ở trang cuối cùng
      if (
        state.filter.pageNumber == state.totalPage &&
        state.employees.length == 1
      ) {
        context.dispatch("setFilter", {
          pageSize: state.filter.pageSize,
          pageNumber: 1,
          employeeFilter: state.filter.employeeFilter,
        });
      }
      //load lại dữ liệu
      context.dispatch("getEmployee");
    } catch (error) {
      handleException(error, context);
    }
  },

  /**
   * Xóa hàng loạt nhân viên
   * @param {*} context
   * @param {string} id
   * Author:DcTuan (09/01/2024)
   */
  async deleteBatchEmployee(context) {
    try {
      await axios.delete(`${API_BASE_URL}Employees/DeleteMany`, {
        data: state.checkedEmployeeIds,
      });
      //thông báo thành công
      context.dispatch("setAlert", {
        type: "success",
        message: RResource.AlertMessage.deleteEmployeeSuccess,
      });

      //Quay về trang đầu tiên nếu xóa hết bản ghi ở trang cuối cùng
      if (
        state.filter.pageNumber == state.totalPage &&
        state.employees.length == state.checkedEmployeeIds.length
      ) {
        context.dispatch("setFilter", {
          pageSize: state.filter.pageSize,
          pageNumber: 1,
          employeeFilter: state.filter.employeeFilter,
        });
      }
      //load lại dữ liệu
      context.dispatch("getEmployee");
    } catch (error) {
      handleException(error, context);
    }
  },

  /**
   * Chọn nhân viên
   * @param {*} context
   * @param {*} emp
   * Author:DcTuan (09/01/2024)
   */
  selectEmployee(context, emp) {
    context.commit("selectEmployee", emp);
  },

  /**
   * xuất dữ liệu ra file excel
   * @param {*} context
   * Author:DcTuan (09/01/2024)
   */
  exportToExcel(context) {
    context.dispatch("toggleLoad");
    axios({
      url: `${API_BASE_URL}Employees/Export`,
      method: "GET",
      responseType: "blob",
    })
      .then((res) => {
        var FILE = window.URL.createObjectURL(new Blob([res.data]));
        var docUrl = document.createElement("a");
        docUrl.href = FILE;
        docUrl.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
        document.body.appendChild(docUrl);
        docUrl.click();
      })
      .catch((error) => handleException(error, context))
      .then(() => {
        context.dispatch("toggleLoad");
      });
  },

  async importToExcel(context) {
    try {
      context.dispatch("toggleLoad");
      var formData = new FormData();
      formData.append("formFile", state.importFile);
      const result = await axios.post(
        `${API_BASE_URL}Employees/Import`,
        formData,
        { headers: { "Content-Type": "multipart/form-data" } }
      );
      context.commit("getImportEmployee", result.data);
      context.dispatch("getEmployee");
    } catch (error) {
      console.log(error);
    } finally {
      context.dispatch("toggleLoad");
    }
  },

  async getRefreshToken(context) {
    const token = JSON.parse(localStorage.getItem("Token"));
    const now = new Date();
    const expriedAccessTokenString = token.ExpiredAccessToken;
    const expriedAccessToken = new Date(expriedAccessTokenString);
    if (expriedAccessToken < now) {
      try {
        const res = await axios.post(`${API_BASE_URL}Users/RenewToken`, token);
        switch (res.status) {
          case 200:
            localStorage.setItem("Token", JSON.stringify(res.data));
            break;
        }
      } catch (error) {
        handleException(error, context);
      }
    }
    let expriedRefeshTokenString = token.ExpiredRefreshToken;
    let expriedRefeshToken = new Date(expriedRefeshTokenString);
    if (token && expriedRefeshToken <= now) {
      router.push("/login");
      context.dispatch("setAlert", {
        type: "success",
        message: RResource.AlertMessage.accessTokenExpired,
      });
      localStorage.setItem("Token", JSON.stringify(null));
    }
  },
};
/**
 * Xử lí lỗi
 * @param {*} error
 * @param {*} context
 * Author:DcTuan (09/01/2024)
 */
const handleException = (error, context) => {
  //thông báo có lỗi
  context.dispatch("setAlert", {
    type: "error",
    message: error.response.data.UserMessage,
  });
};

export default actions;
