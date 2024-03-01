using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher.MF1773.Demo.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher.MF1773.Demo.Application
{
    public class CustomerService : BaseService<Customer, CustomerDto, CustomerCreateDto, CustomerUpdateDto> , ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidate _customerValidate;
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly IMapper _mapper;
        IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository customerRepository, ICustomerValidate customerValidate, IMapper mapper, IUnitOfWork unitOfWork, ICustomerGroupRepository customerGroupRepository) : base(customerRepository) 
        {
            _customerRepository = customerRepository;
            _customerValidate = customerValidate;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerGroupRepository = customerGroupRepository;
        }

        public override Customer MapCreateDtoToEntity(CustomerCreateDto createDto)
        {
            var customer = _mapper.Map<Customer>(createDto);

            customer.CustomerId = Guid.NewGuid();

            return customer;
        }

        public override async Task ValidateCreateBusiness(Customer entity)
        {
            //check trùng mã 
            await _customerValidate.CheckCustomerExistByCodeAsync(entity);

        }

        public override CustomerDto MapEntityToDto(Customer customer)
        {
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public override Customer MapUpdateDtoToEntity(CustomerUpdateDto updateDto, Customer entity)
        {
            var newCustomer = _mapper.Map(updateDto, entity);

            return newCustomer;
        }

        public async Task<EntitisPaging<CustomerDto>> GetCustomerPagingAsync(int? pageSize, int? pageNumber, string? customerFilter)
        {

            var entitiesPaging = await _customerRepository.GetEntityPagingAsync(pageSize, pageNumber, customerFilter);

            var data = _mapper.Map<List<CustomerDto>>(entitiesPaging.Data);

            var entitiesPagingDto = new EntitisPaging<CustomerDto>()
            {
                TotalPage = entitiesPaging.TotalPage,
                TotalRecord = entitiesPaging.TotalRecord,
                CurrentPage = entitiesPaging.CurrentPage,
                CurrentPageRecords = entitiesPaging.CurrentPageRecords,
                Data = data,
            };

            return entitiesPagingDto;
        }

        public async Task<IEnumerable<CustomerDto>> ImportCustomerAsync(IFormFile fileImport)
        {
            CheckFileImport(fileImport);
            var customers = new List<CustomerImportDto>();
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
                        for (var row = 4; row <= rowCount + 3; row++)
                        {
                            var customerImport = new CustomerImportDto()
                            {
                                CustomerCode = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim(),
                                FullName = worksheet?.Cells[row, 3]?.Value?.ToString()?.Trim(),
                                Email = worksheet?.Cells[row, 4]?.Value?.ToString()?.Trim(),
                                PhoneNumber = worksheet?.Cells[row, 5]?.Value?.ToString()?.Trim(),
                                //DateOfBirth = ProcessDate(worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim()),
                                Address = worksheet?.Cells[row, 7]?.Value?.ToString()?.Trim(),
                                CustomerGroupId = await ConvertCustomerGroupId(worksheet?.Cells[row, 8]?.Value?.ToString()?.Trim()),

                            };


                            //Kiểm tra trùng mã
                            var isDuplicate = await _unitOfWork.Customers.IsExistCustomerAsync(customerImport.CustomerCode);
                            if (customerImport.CustomerCode == null || customerImport.CustomerCode == "")
                            {
                                customerImport.ImportInvalidErrors.Add(
                                $"Mã khách hàng không được để trống");
                            }
                            if (isDuplicate)
                            {
                                customerImport.ImportInvalidErrors.Add(
                                $"Mã khách hàng {customerImport.CustomerCode} đã tồn tại trong hệ thống");
                            }
                            if (customerImport.ImportInvalidErrors == null || customerImport.ImportInvalidErrors?.Count == 0)
                            {
                                var customer = _mapper.Map<Customer>(customerImport);
                                //Thực hiện thêm mới
                                var res = await _unitOfWork.Customers.InsertAsync(customer);
                                if (res)
                                {
                                    customerImport.IsImport = true;
                                }
                            }
                            customers.Add(customerImport);
                        }
                    }
                    _unitOfWork.Commit();
                }
            }
            return customers;
        }

        private async Task<Guid> ConvertCustomerGroupId(string? customerGroupName)
        {
            var customerGroups = await _customerGroupRepository.GetAllAsync();

            foreach (var customerGroup in customerGroups)
            {
                if (customerGroup.CustomerGroupName.Equals(customerGroupName))
                {
                    return customerGroup.CustomerGroupId;
                }
            }
            return Guid.Empty;
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

        public override byte[] ExportExcelAsync(List<Customer> data)
        {
            //danh sách properties
            var properties = typeof(Customer).GetProperties();

            //danh sách các cột export vào excel
            var columns = new List<string>();

            foreach (var property in properties)
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
                columns.Add(displayNameAttribute?.DisplayName);
            }

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
                ws.Cells[1, 1].Value = "Danh sách khách hàng";
                // merge các column lại từ column 1 đến số column header và set style
                ws.Cells[1, 1, 2, countColHeader + 1].Merge = true;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.Font.Bold = true;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.Font.Size = 24;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerCol = 2;
                var headerRow = 3;
                ws.Cells[headerRow, 1].Value = "STT";

                //tạo các header từ column header đã tạo từ bên trên
                foreach (var item in columns)
                {
                    //gán giá trị
                    ws.Cells[headerRow, headerCol].Value = item;
                    headerCol++;
                }

                //set style cho header
                var cells = ws.Cells[headerRow, 1, headerRow, countColHeader + 1];
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
                //gán giá trị cho từng dòng và cột
                foreach (var item in data)
                {
                    //var itemDto = mapDto(item);
                    ws.Cells[currentRow, 1].Value = colSTT;
                    var currentCol = 2;

                    foreach (var property in properties)
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
    }

}
