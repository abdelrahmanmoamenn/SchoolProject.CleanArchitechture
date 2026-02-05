using Microsoft.EntityFrameworkCore;
using SchoolProject.Core;
using SchoolProject.Infrastrcture;
using SchoolProject.Infrastrcture.Data;
using SchoolProject.Infrastrcture.IRepoistories;
using SchoolProject.Infrastrcture.Repoistories;
using SchoolProject.Service;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(option => {

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
