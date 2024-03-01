const getters = {
    modalTitle: state => state.modalTitle,
    isShowModal: state => state.isShowModal,
    isShowImport: state => state.isShowImport,
    formType: state => state.formType,
    filter:state => state.filter,
    employees: state => state.employees,
    employee:state => state.employee,
    singleEmployee:state => state.singleEmployee,
    totalEmployee: state => state.totalEmployee,
    totalPage: state => state.totalPage,
    checkedEmployeeIds: state => state.checkedEmployeeIds,
    importFile: state => state.importFile,
    importEmployee: state => state.importEmployee,
}
export default getters