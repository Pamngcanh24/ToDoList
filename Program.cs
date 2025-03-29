using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ vào container
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoApi.Models.TodoContext>(options => 
    options.UseInMemoryDatabase("TodoList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình pipeline HTTP
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();