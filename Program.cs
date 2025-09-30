using MHKCafe.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký DbContext và kết nối SQL Server
builder.Services.AddDbContext<MhkcafeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MHKCafeConnection")));

// THÊM CẤU HÌNH SESSION - QUAN TRỌNG
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "MHKCafe.Session";
});

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

// THÊM DÒNG NÀY - QUAN TRỌNG (phải đặt sau UseRouting và trước UseAuthorization)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=mhkHome}/{action=Index}/{id?}");

app.Run();