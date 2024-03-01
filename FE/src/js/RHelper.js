
const RHelper = {
  /**
   * Date table format
   * @param {dateTable}
   * CreatedBy:dcTuan(07/12/2023)
   * Modify:null
   */
  formatDateTable(dateTable) {
    if(dateTable==null){
      return null;
    }
    else{
      dateTable = new Date(dateTable);
      // Lấy ngày
      let date = dateTable.getDate();
      date = date < 10 ? `0${date}` : date;
      // Lấy tháng
      let month = dateTable.getMonth() + 1;
      month = month < 10 ? `0${month}` : month;
      // Lấy năm
      let year = dateTable.getFullYear();

      dateTable = `${date}/${month}/${year}`;
    }
    return dateTable;
  },
  /**
   * Date input format
   * @param {dateInput}
   * CreatedBy:dcTuan(07/12/2023)
   * Modify:null
   */
  formatDateInput(dateInput) {
    if (dateInput == null) {
      return null;
    } else {
      // Tạo đối tượng Date từ chuỗi ngày tháng
    var formattedDate = new Date(dateInput);
    
    // Chuyển đổi thành chuỗi ngày tháng theo định dạng yyyy-mm-dd
    var year = formattedDate.getFullYear();
    var month = String(formattedDate.getMonth() + 1).padStart(2, '0');
    var day = String(formattedDate.getDate()).padStart(2, '0');
    
    var dateString = `${year}-${month}-${day}`;

    return dateString;
    }
  },
  /**
   * convert Gender
   * @param {item}
   * CreatedBy:dcTuan(07/12/2023)
   * Modify:null
   */
  convertGender(item) {
    if (item == 0) {
      return "Nam";
    } else if (item == 1) {
      return "Nữ";
    } else if (item == 2) {
      return "Khác";
    } else {
      return null;
    }
  },
  /*
    format Number With Dots funciton
    @param{number}
    CreatedBy:dcTuan(07/12/2023)
    Modify:null
    */
  formatNumberWithDots(number) {
    const numberString = String(number);
    return numberString.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  },
  /**
   * check length input
   * @param {valueInput}
   * CreatedBy:dcTuan(12/12/2023)
   * Modify:null
   */
  validationLengthInput(valueInput, min_length, max_length) {
    if (valueInput.length < min_length) return false;
    else if (valueInput.length > max_length) return false;
    else return true;
  },
  /**
   * Xử lí lỗi import
   * @param {errorImport}
   * CreatedBy:dcTuan(28/01/2024)
   * Modify:null
   */
  errImport(errorImport){
    if(errorImport==""||errorImport==null){
      return "Đã thêm thành công"
    } else{
      return errorImport.join("\n");
    }
  },
}

export default RHelper;
