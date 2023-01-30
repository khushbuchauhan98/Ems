using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var connectionString = builder.Configuration.GetConnectionString("default") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<Employees>().AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDBContext>();

// Add services to the container.
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "Account/Login";
//    });
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<IAmountRepository, AmountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
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
app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
