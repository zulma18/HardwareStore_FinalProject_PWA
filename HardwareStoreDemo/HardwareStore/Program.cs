using FluentValidation;
using HardwareStore.Data;
using HardwareStore.Models;
using HardwareStore.Repositories.Categorys;
using HardwareStore.Repositories.Clients;
using HardwareStore.Repositories.Employees;
using HardwareStore.Repositories.Products;
using HardwareStore.Repositories.Reports;
using HardwareStore.Repositories.Sales;
using HardwareStore.Repositories.Logins;
using HardwareStore.Services.Email;
using HardwareStore.Validations;
using Microsoft.AspNetCore.Authentication.Cookies;
using HardwareStore.Repositories.Roles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// conection database
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

// validations
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<Sale>, SaleValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<Supplier>, SupplierValidator>();
builder.Services.AddScoped<IValidator<RolesModel>, RolValidator>();
builder.Services.AddScoped<IValidator<Logins_Model>, LoginValidator>();


// repositories
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ILoginsRepository, LoginsRepository>();
builder.Services.AddScoped<IRolesRepository, RolesRepository>();

// services
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Login/Login";
    option.ExpireTimeSpan = TimeSpan.FromSeconds(120);
    option.SlidingExpiration = true;
});

var app = builder.Build();

// no guardar cache
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});

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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
