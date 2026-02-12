using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SchoolProject.Core;
using SchoolProject.Core.Middleware;
using SchoolProject.Infrastrcture;
using SchoolProject.Infrastrcture.Data;
using SchoolProject.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{

    option.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb"));
});

#region Dependency Injection

builder.Services.AddInfrastructureDependices()
    .AddServiceDependices()
    .AddCoreDependices();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
