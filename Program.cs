using MHKCafe.Models;
using Microsoft.EntityFrameworkCore;
using MHKCafe.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký DbContext và kết nối SQL Server
builder.Services.AddDbContext<MhkcafeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MHKCafeConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/mhkHome/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=mhkHome}/{action=Index}/{id?}");

app.Run();
