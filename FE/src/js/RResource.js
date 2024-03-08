const VN = {
  warningSave: "Bạn có muốn lưu các thay đổi này?",
  cancel: "Hủy bỏ",
  noSave: "Không",
  save: "Cất",
  delete: "Xóa",
  deleteOneContent: "Bạn có muốn xóa nhân viên",
  deleteNullContent: "Không có nhân viên nào được chọn",
  deleteManyContent:
    " nhân viên đã được chọn. Bạn có muốn xóa các nhân viên này khỏi danh sách ?",
  ok: "OK",
  error400: "Yêu cầu không hợp lệ, vui lòng kiểm tra lại thông tin",
  error401: "Bạn cần phải đăng nhập để truy cập tài nguyên này",
  error403: "Bạn không có quyền truy cập vào tài nguyên này",
  error404: "Không tìm thấy dữ liệu",
  error500: "Lỗi máy chủ, vui lòng thử lại sau",
  errorDownloadData: "Đã xảy ra lỗi khi tải dữ liệu",
};
const ERROR_VALIDATE = {
  errorCustomerCode: "Mã khách hàng có ít nhất 7 và nhiều nhất 25 ký tự",
  errorCustomerName: "Tên khách hàng có ít nhất 5 và nhiều nhất 25 ký tự",
  duplicate: "Mã khách hàng bị trùng lặp",
};

const Text = {
  employee: "Nhân viên",
  addEmployee: "Thêm mới nhân viên",
  save: "Lưu",
  store: "Cất",
  storeAdd: "Cất và thêm",
  cancel: "Hủy",
  deny: "Không",
  confirm: "Có",
  close: "Đóng",
  prev: "Trước",
  next: "Sau",
  modify: "Sửa",
  duplicate: "Nhân bản",
  delete: "Xóa",
  stopUsing: "Ngưng sử dụng",
  addFormTitle: "Thêm Nhân viên",
  modifyFormTitle: "Sửa Nhân viên",
  pageSize: " bản ghi trên 1 trang",
  total: "Tổng",
  record: "bản ghi",
  isCustomer: "Là khách hàng",
  isProvider: "Là nhà cung cấp",
  helpToolTip: "Trợ giúp",
  escToolTip: "Thoát",
  male: "Nam",
  female: "Nữ",
  other: "Khác",
  batchAction: "Thực hiện hàng loạt",
  success: "Thành công!",
  error: "Lỗi!",
  selected: "Đã chọn",
  clearSelect: "Bỏ chọn",
  recordsPerPage: "Số bản ghi/trang:",
  add: "Thêm",
  filter: "Lọc",
  utility: "Tiện ích",
  function: "Chức năng",
  organization: "Tổ chức",
  personal: "Cá nhân",
  addReceive: "Thêm thu tiền",
  addPurchase: "Thêm chi tiền",
  import:"Nhập khẩu",
  comback:"Quay lại",
  forgotPassword:"Quên mật khẩu?",
  login:"Đăng nhập",
  loginWith:"Đăng nhập với",
  selectValueToImport: "Chọn dữ liệu đã chuẩn bị để nhập khẩu vào phần mềm",
  selectAFile: "Chọn một tệp để nhập khẩu",
  select: "Chọn",
  noFileTemplate:
    "Chưa có tệp mẫu để chọn dữ liệu? Tải tệp excel mẫu mà phần mềm cung cấp để chuẩn bị dữ liệu nhập khẩu",
  here: "Tại đây",
};

const FieldName = {
  employeeCode: "Mã nhân viên",
  employeeName: "Tên nhân viên",
  gender: "Giới tính",
  dateOfBirth: "Ngày sinh",
  identityNumber: "Số CMND",
  identityDate: "Ngày cấp",
  identityPlace: "Nơi cấp",
  employeePosition: "Chức danh",
  departmentName: "Đơn vị",
  address: "Địa chỉ",
  phoneNumber: "ĐT di động",
  telephoneNumberToolTip: "Điện thoại di động",
  landlineNumber: "ĐT cố định",
  phoneNumberToolTip: "Điện thoại cố định",
  email: "Email",
  bankAccount: "Số tài khoản",
  bankName: "Tên ngân hàng",
  bankBranch: "Chi nhánh TK ngân hàng",
  bankBranchNameToolTip: "Chi nhánh tài khoản ngân hàng",
  providerCode: "Mã nhà cung cấp",
  providerName: "Tên nhà cung cấp",
  debt: "Công nợ",
  taxCode: "Mã số thuế",
  debtDays: "Số ngày được nợ",
  description: "Diễn giải",
  branchName: "Chi nhánh",
  phone: "Số điện thoại",
  employee: "Nhân viên",
  state:"Tình trạng",
};

const ToolTip = {
  refresh: "Tải lại dữ liệu",
  export: "Xuất dữ liệu ra file",
  identityNumber: "Số chứng minh nhân dân",
  phoneNumber: "Điện thoại di động",
  landlineNumber: "Điện thoại cố định",
  bankBranch: "Chi nhánh tài khoản ngân hàng",
};

const Dialog = {
  deleteEmployeeTitle: "Xóa nhân viên",
  deleteEmployeeMessage: (employeeCode) =>
    `Nhân viên <${employeeCode}> sẽ bị xóa.`,
  deleteBatchEmployeeTitle: (number) => `Xóa ${number} nhân viên`,
  deleteBatchEmployeeMessage: "Bạn có chắc xoá những nhân viên được chọn?",
  changeDataTitle: "Dữ liệu thay đổi",
  changeDataMessage: "Lưu lại những thay đổi?",
  invalid:"Lỗi nhập dữ liệu",
  errorServer:"Lỗi hệ thống",
};

const PlaceHolder = {
  search: "Tìm theo mã, tên nhân viên",
  email: "example@gmail.com",
  landlineNumber: "(012)3456789",
  import:"Chọn một tệp để xuất khẩu"
};

const AlertMessage = {
  addEmployeeSuccess: "Thêm nhân viên thành công.",
  editEmployeeSuccess: "Sửa nhân viên thành công.",
  deleteEmployeeSuccess: "Xóa nhân viên thành công.",
  accessTokenExpired:"Token hết hạn vui lòng đăng nhập lại",
  errorServer:"Kết nối đến máy chủ bị gián đoạn,vui lòng thử lại sau",
};

const ErrorMessage = {
  emptyEmployeeCode: "Mã nhân viên không được để trống",
  invalidEmployeeCode: "Mã nhân viên không đúng định dạng",
  emptyEmployeeName: "Họ và tên không được để trống",
  emptyDepartmentName: "Đơn vị không được để trống",
  invalidDateOfBirth: "Ngày sinh không được lớn hơn hiện tại",
  invalidIdentityDate: "Ngày cấp không được lớn hơn hiện tại",
  invalidEmail: "Email không đúng định dạng",
  invalidPhoneNumber: "Số điện thoại không hợp lệ",
  invalidLandlineNumber: "Số điện thoại cố định không hợp lệ",
  invalidIdentityNumber: "Số căn cước công dân không hợp lệ",
};

const SidebarItem = {
  accountant:"Kế toán",
  employee:"Nhân viên",
  cash:"Tiền mặt",
  bank:"Tiền gửi",
  buy:"Mua hàng",
  sale:"Bán hàng",
  invoice:"Quản lý hóa đơn",
  stock:"Kho",
  tools:"Công cụ dụng cụ",
  fixedAssets:"Tài sản cố định",
  tax:"Thuế",
  price:"Giá thành",
  general:"Tổng hợp",
  report:"Báo cáo",
  customer:"Khách hàng",
  statistics:"Thống kê",
}

const ExcelImport = {
  Step: "Bước",
  StepOne: "1: Chọn tệp nguồn",
  StepSecond: "2. Kiểm tra dữ liệu",
  StepThird: "3. Kết quả nhập khẩu",
};

const RResource = {
  VN,
  ERROR_VALIDATE,
  Text,
  FieldName,
  Dialog,
  AlertMessage,
  ErrorMessage,
  ToolTip,
  PlaceHolder,
  SidebarItem,
  ExcelImport,
};

export default RResource;
