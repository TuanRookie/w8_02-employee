

const mutations = {
  setModalTitle(state, payload) {
    state.modalTitle = payload;
  },
  changeFormType(state, payload) {
    state.formType = payload;
  },
  toggleModal(state) {
    state.isShowModal = !state.isShowModal;
  },
  toggleImport(state){
    state.isShowImport = !state.isShowImport;
  },
  setCheckedEmployeeIds(state, payload) {
    if (!state.checkedEmployeeIds.includes(payload)) {
      state.checkedEmployeeIds.push(payload);
    } else {
      state.checkedEmployeeIds = state.checkedEmployeeIds.filter(
        (item) => item !== payload
      );
    }
  },
  getEmployee(state, payload) {
    state.employees = payload.Data;
    state.totalEmployee = payload.TotalRecord;
    state.totalPage = payload.TotalPage;
    state.checkedEmployeeIds = [];
  },
  getImportEmployee(state, payload){
    state.importEmployee = payload;
  },
  setNewEmployeeCode(state, payload) {
    state.singleEmployee.EmployeeCode = payload;
    state.employee.EmployeeCode = payload;
  },
  selectEmployee(state, payload) {
    state.singleEmployee = payload;
    state.employee = JSON.parse(JSON.stringify(payload));
  },
  setFilter(state, payload) {
    state.filter.pageSize = payload.pageSize;
    state.filter.pageNumber = payload.pageNumber;
    state.filter.employeeFilter = payload.employeeFilter;
  },
  setFileImport(state, payload){
    state.importFile = payload;
  },
};
export default mutations;
