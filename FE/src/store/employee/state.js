const state = {
    modalTitle: "Thêm nhân viên",
    isShowModal: false,
    isShowImport: false,
    formType: 0,
    filter:{
        pageSize:10,
        pageNumber:1,
        employeeFilter:null
    },
    employees: [],
    employee:{},
    singleEmployee:{},
    totalEmployee: 0,
    totalPage: 0,
    checkedEmployeeIds: [],
    importFile:null,
    importEmployee:[],
}
export default state