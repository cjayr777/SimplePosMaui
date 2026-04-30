using Pos.BusinessLogic;
using Pos.DataAccess;
using Proj.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<PostgreHelper>(sp => new PostgreHelper("DefaultConnection"));
builder.Services.AddScoped<ProductTypeDataAx>();
builder.Services.AddScoped<ProductTypeBizz>();

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
