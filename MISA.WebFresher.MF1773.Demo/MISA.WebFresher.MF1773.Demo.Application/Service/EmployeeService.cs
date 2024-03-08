using AutoMapper;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Globalization;
using MISA.WebFresher.MF1773.Demo.Domain.Resource;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidate _employeeValidate;
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        IUnitOfWork _unitOfWork;
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidate employeeValidate, IMapper mapper, IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeValidate = employeeValidate;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public override Employee MapCreateDtoToEntity(EmployeeCreateDto createDto)
        {
            var employee = _mapper.Map<Employee>(createDto);

            employee.EmployeeId = Guid.NewGuid();

            return employee;
        }

        public override async Task ValidateCreateBusiness(Employee entity)
        {
            //check trùng mã 
            await _employeeValidate.CheckEmployeeExistByCodeAsync(entity);

        }

        public override async Task ValidateUpdateBusiness(Employee newEntity)
        {
            var employee = await _employeeRepository.GetAsync(newEntity.EmployeeId);
            if (employee.EmployeeCode != newEntity.EmployeeCode)
            {
                //check trùng mã 
                await _employeeValidate.CheckEmployeeExistByCodeAsync(newEntity);
            }
        }

        public override EmployeeDto MapEntityToDto(Employee entity)
        {
            var employeeDto = _mapper.Map<EmployeeDto>(entity);
            return employeeDto;
        }

        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto updateDto, Employee entity)
        {
            var newEmployee = _mapper.Map(updateDto, entity);

            return newEmployee;
        }

        public async Task<EntitisPaging<EmployeeDto>> GetEmployeePagingAsync(int? pageSize, int? pageNumber, string? employeeFilter)
        {

            var entitiesPaging = await _employeeRepository.GetEntityPagingAsync(pageSize, pageNumber, employeeFilter);

            var data = _mapper.Map<List<EmployeeDto>>(entitiesPaging.Data);

            var entitiesPagingDto = new EntitisPaging<EmployeeDto>()
            {
                TotalPage = entitiesPaging.TotalPage,
                TotalRecord = entitiesPaging.TotalRecord,
                CurrentPage = entitiesPaging.CurrentPage,
                CurrentPageRecords = entitiesPaging.CurrentPageRecords,
                Data = data,
            };

            return entitiesPagingDto;
        }

        public async Task<string> GetNewEmployeeCode()
        {
            var result = await _employeeRepository.GetNewEmployeeCode();

            return result;
        }

        public override byte[] ExportExcelAsync(List<Employee> data)
        {
            //danh sách properties
            var properties = typeof(Employee).GetProperties();
            //danh sách các cột export vào excel
            var columns = typeof(Employee).GetProperties()
            .Select(property => new
            {
                Name = property.Name,
                DisplayName = property.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName
            })
            .ToList();
            var headerRows = 3;


            var stream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(stream))
            {

                // đặt tên người tạo file
                excelPackage.Workbook.Properties.Author = "DCTuan";

                //Tạo một sheet để làm việc trên đó
                var ws = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // set style mặc định cho toàn bộ file
                ws.Cells.Style.Font.Size = 14;
                ws.Cells.Style.Font.Name = "Times New Roman";

                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = columns.Count();

                // gán giá trị cho cell vừa merge
                ws.Cells[1, 1].Value = "Danh sách Nhân viên";
                // merge các column lại từ column 1 đến số column header và set style
                ws.Cells[1, 1, 2, countColHeader - 6].Merge = true;
                ws.Cells[1, 1, 2, countColHeader - 6].Style.Font.Bold = true;
                ws.Cells[1, 1, 2, countColHeader - 6].Style.Font.Size = 24;
                ws.Cells[1, 1, 2, countColHeader - 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerCol = 2;
                var headerRow = 3;
                ws.Cells[headerRow, 1].Value = "STT";



                //set style cho header
                var cells = ws.Cells[headerRow, 1, headerRow, countColHeader - 6];
                //set màu thành gray
                var fill = cells.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                //căn chỉnh các border
                var border = cells.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = ExcelBorderStyle.Thin;


                var currentRow = 4;
                var colSTT = 1;
                var colIndex = 2;
                //tạo các header từ column header đã tạo từ bên trên
                foreach (var column in columns.Skip(2).Take(columns.Count() - 7))
                {
                    ws.Cells[headerRows, colIndex].Value = column.DisplayName ?? column.Name;
                    colIndex++;
                }

                //gán giá trị cho từng dòng và cột
                foreach (var item in data)
                {
                    ws.Cells[currentRow, 1].Value = colSTT;
                    var currentCol = 2;

                    foreach (var property in properties.Skip(2).Take(properties.Length - 7))
                    {
                        var propValue = property.GetValue(item);
                        if (propValue != null && property.PropertyType == typeof(DateTime?))
                        {
                            propValue = String.Format("{0:dd/MM/yyyy}", propValue); ;
                        }
                        //gán giá trị cho từng cell                      
                        ws.Cells[currentRow, currentCol].Value = propValue;
                        currentCol++;
                    }

                    //set style cho từng ô giá trị
                    ws.Cells[currentRow, 1, currentRow, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells.AutoFitColumns();
                    ws.Rows[currentRow].Height = 20;

                    colSTT++;
                    currentRow++;
                }

                excelPackage.Save();
                //chuyển đổi nội dung của đối tượng stream thành một mảng byte.
                var bytes = stream.ToArray();
                return bytes;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> ImportEmployeeAsync(IFormFile fileImport)
        {
            CheckFileImport(fileImport);
            var employees = new List<EmployeeImportDto>();
            using (var stream = new MemoryStream())
            {
                fileImport.CopyTo(stream);
                using (var excelPackage = new ExcelPackage(stream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets[0];
                    _unitOfWork.BeginTransaction();
                    if (worksheet != null)
                    {
                        var rowCount = worksheet.Dimension.Rows;
                        for (var row = 2; row <= rowCount; row++)
                        {
                            var employeeImport = new EmployeeImportDto()
                            {
                                EmployeeCode = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                FullName = worksheet?.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                Gender = ConvertGender(worksheet?.Cells[row, 4]?.Value?.ToString()?.Trim()),
                                DateOfBirth = ConvertDate(worksheet?.Cells[row, 5]?.Value?.ToString()?.Trim()),
                                PhoneNumber = worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim(),
                                Email = worksheet?.Cells[row, 7]?.Value?.ToString()?.Trim(),
                                Address = worksheet?.Cells[row, 8]?.Value?.ToString()?.Trim(),
                                DepartmentName = worksheet?.Cells[row, 9]?.Value?.ToString()?.Trim(),
                                DepartmentId = await ConvertDepartmentId(worksheet?.Cells[row, 9]?.Value?.ToString()?.Trim()),
                                CreatedBy = "Đinh Công Tuấn",
                                CreatedDate = DateTime.Now,
                        };


                            //Kiểm tra trùng mã
                            var isDuplicate = await _unitOfWork.Employees.IsExistEmployeeAsync(employeeImport.EmployeeCode);
                            if (employeeImport.EmployeeCode == null || employeeImport.EmployeeCode == "")
                            {
                                employeeImport.ImportInvalidErrors.Add(
                                Resource.ExceptionNotNull);
                            }
                            if (isDuplicate)
                            {
                                employeeImport.ImportInvalidErrors.Add(
                                String.Format(Resource.Exception_Duplicate_Employee, employeeImport.EmployeeCode));
                            }
                            if (employeeImport.ImportInvalidErrors == null || employeeImport.ImportInvalidErrors?.Count == 0)
                            {
                                var employee = _mapper.Map<Employee>(employeeImport);
                                //Thực hiện thêm mới
                                var res = await _unitOfWork.Employees.InsertAsync(employee);
                                if (res)
                                {
                                    employeeImport.IsImport = true;
                                }
                            }
                            employees.Add(employeeImport);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            return employees;
        }

        private DateTime? ConvertDate(string? date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return null;
            }
            string[] formats =
            {
                "dd-MM-yyyy", "dd/MM/yyyy", "dd*MM/yyyy", "dd#MM#yyyy", "d/M/yyyy", "dd*M/yyyy", "dd#M#yyyy",
                "d/M/yyyy", "dd*M/yyyy", "dd#M#yyyy", "M/d/yyyy", "M/dd/yyyy", "M-d-yyyy", "M/dd/yyyy", "M*d/yyyy", "M#d#yyyy",
                "yyyy-MM-dd", "yyyy/MM/dd", "yyyy*M*dd", "yyyy#M#dd", "yyyy-d-M", "yyyy*d*M", "yyyy#d#M"
            };

            DateTime result;

            // Try parsing with specific formats
            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Try parsing with generic format
            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Try parsing as year only
            if (int.TryParse(date, out int year))
            {
                return new DateTime(year, 1, 1);
            }

            // If all attempts fail, return null
            return null;
        }

        private GenderEnum? ConvertGender(string? gender)
        {
            if (gender != null)
            {
                if (gender == "Nam")
                {
                    return GenderEnum.Male;
                }
                else if (gender == "Nữ")
                {
                    return GenderEnum.Female;
                }
                else
                {
                    return GenderEnum.Other;
                }
            }
            else
            {
                return null;
            }
        }

        private void CheckFileImport(IFormFile fileImport)
        {
            if (fileImport == null || fileImport.Length <= 0)
            {
                throw new ValidateException("File nhập khẩu không được để trống");
            }
            if (!Path.GetExtension(fileImport.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidateException("File nhập khẩu không đúng định dạng cho phép");

            }
        }

        private async Task<Guid> ConvertDepartmentId(string? departmentName)
        {
            var departments = await _departmentRepository.GetAllAsync();

            foreach (var department in departments)
            {
                if (department.DepartmentName.Equals(departmentName))
                {
                    return department.DepartmentId;
                }
            }
            return Guid.Empty;
        }
    }
}
