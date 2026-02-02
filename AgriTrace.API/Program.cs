using AgriTrace.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ================= 1. ĐĂNG KÝ SERVICES =================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Cấu hình Swagger: Sử dụng đường dẫn đầy đủ để tránh lỗi namespace Models
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "AgriTrace Blockchain API", 
        Version = "v1",
        Description = "Hệ thống truy xuất nguồn gốc nông sản sử dụng Blockchain Sepolia"
    });
});

// Đăng ký các Service xử lý logic
builder.Services.AddScoped<BlockchainService>();
builder.Services.AddScoped<QRCodeService>();

// Cấu hình CORS: Cho phép Frontend gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ================= 2. CẤU HÌNH PIPELINE =================

// Luôn bật Swagger để kiểm tra
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AgriTrace API v1");
    options.RoutePrefix = "swagger"; 
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();