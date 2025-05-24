using Manufacturing.Application.Services;
using Manufacturing.Domain.Repositories;
using Manufacturing.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// SQL Server 2019 connection string from appsettings.json
builder.Services.AddScoped<IInventoryRepository, DapperInventoryRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>(); // assuming EF for orders
builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();
