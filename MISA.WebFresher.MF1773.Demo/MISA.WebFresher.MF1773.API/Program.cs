using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MISA.WebFresher.MF1773.Demo.API;
using MISA.WebFresher.MF1773.Demo.Application;
using MISA.WebFresher.MF1773.Demo.Application.IService;
using MISA.WebFresher.MF1773.Demo.Application.Service;
using MISA.WebFresher.MF1773.Demo.Domain;
using MISA.WebFresher.MF1773.Demo.Domain.Repository;
using MISA.WebFresher.MF1773.Demo.Infractructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    /*.ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors);

            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = "Lỗi nhập từ người dùng",
                DevMessage = "Lỗi nhập từ người dùng",
                TraceId = "",
                MoreInfo = "",
                Error = errors
            }.ToString() ?? "");
        };
    }
    )*/
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Khai báo dêpndency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IDbContext, DbContext>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerValidate, CustomerValidate>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();

builder.Services.AddScoped<ICustomerGroupService, CustomerGroupService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IEmployeeValidate, EmployeeValidate>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");

app.Run();
