using Microsoft.AspNetCore.Mvc;
using Pos.BusinessLogic;
using Pos.DataAccess;
using Proj.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use builder.Configuration to get the value FROM the json file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Now pass the actual connection string (Host=localhost...) to the helper
builder.Services.AddScoped<PostgreHelper>(sp => new PostgreHelper(connectionString));



builder.Services.AddScoped<ProductTypeDataAx>();
builder.Services.AddScoped<ProductTypeBizz>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
