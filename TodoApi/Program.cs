using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<ToDoItemService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql("server=localhost;user=root;password=1234;database=todolist", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.44-mysql"))
);

var app = builder.Build();

// Use CORS
app.UseCors("AllowAnyOrigin");

app.MapGet("/items", (ToDoItemService service) => service.GetAllItems());
app.MapGet("/items/{id}", (ToDoItemService service, int id) => service.GetItemById(id));
app.MapPost("/items", (ToDoItemService service, Item item) => { service.AddItem(item); return "Item added."; });
app.MapPut("/items/{id}", (ToDoItemService service, int id, Item item) => { item.Id = id; service.UpdateItem(item); return "Item updated."; });
app.MapDelete("/items/{id}", (ToDoItemService service, int id) => { service.DeleteItem(id); return "Item deleted."; });
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.Run();


