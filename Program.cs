using Neuroglia.AsyncApi;
using Neuroglia.AsyncApi.AspNetCore;
using Neuroglia.AsyncApi.AspNetCore.UI;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAsyncApi(); // Đăng ký dịch vụ AsyncAPI

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseRouting();

// Ánh xạ tài liệu AsyncAPI và UI
//app.MapAsyncApiDocuments(); // Cung cấp JSON AsyncAPI tại /asyncapi.json

app.UseAuthorization();
app.MapControllers();

app.Run();