using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using ProfitAPI;
using ProfitAPI.Models;
using ProfitAPI.Services;

var builder = WebApplication.CreateBuilder(args);

Database.CheckDatabaseConnection();

// Регистрация DbContext с правильным типом
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        "Server=my-mysql;Port=3306;Database=products;User=root;Password=example;", 
        new MySqlServerVersion(new Version(8, 0, 26))
    )
);

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

builder.Services.AddSingleton<IProductService, ProductService>(); 
builder.Services.AddSingleton<MoySkladService>(sp => 
    new MoySkladService("YOUR_ACCESS_TOKEN"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Создание области и вызов миграций с правильным типом контекста
using (var scope = app.Services.CreateScope())
{
    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();  // Используем конкретный тип контекста

    _context.Database.Migrate();  // Применение миграций
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();