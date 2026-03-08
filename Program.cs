using Microsoft.EntityFrameworkCore;
using OnlineStoreApi.Data;
using OnlineStoreApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=store.db"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

// Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product
            {
                Name = "iPhone 15",
                Price = 599,
                ImageUrl = "https://m.media-amazon.com/images/I/61eEYLATF9L._AC_SL1500_.jpg"
            },

            new Product
            {
                Name = "MacBook Air",
                Price = 886,
                ImageUrl = "https://m.media-amazon.com/images/I/71ZdDLeldzL._AC_SL1500_.jpg"
            },
            new Product
            {
                Name = "PlayStation 5",
                Price = 499,
                ImageUrl = "https://m.media-amazon.com/images/I/51VZNrNDhOL._AC_SL1500_.jpg"
            },
            new Product
            {
                Name = "Xbox Series X",
                Price = 499,
                ImageUrl = "https://m.media-amazon.com/images/I/61wekx1UKAL._SL1500_.jpg"
            },
            new Product
            {
                Name = "Nintendo Switch",
                Price = 299,
                ImageUrl = "https://m.media-amazon.com/images/I/71n+F6bHXGL._SL1500_.jpg"
            },
            new Product
            {
                Name = "iPhone SE",
                Price = 299,
                ImageUrl = "https://m.media-amazon.com/images/I/61Z2d-xWB0L._AC_SY300_SX300_QL70_ML2_.jpg"
            }
        );

        db.SaveChanges();
    }
}

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();