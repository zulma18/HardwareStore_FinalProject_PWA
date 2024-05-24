using FluentValidation;
using HardwareStore.Data;
using HardwareStore.Models;
using HardwareStore.Repositories.Employees;
using HardwareStore.Repositories.Sales;
using HardwareStore.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// conection database
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

// validations
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();

// repositories
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
