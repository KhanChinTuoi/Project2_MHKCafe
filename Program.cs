using MHKCafe.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// 🧱 1️⃣ Cấu hình dịch vụ (Services)
// ========================================

// Kích hoạt MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Kết nối CSDL SQL Server
builder.Services.AddDbContext<MhkcafeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MHKCafeConnection")));

// 🔹 Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại session
    options.Cookie.HttpOnly = true;                 // Chỉ truy cập session qua server
    options.Cookie.IsEssential = true;              // Bắt buộc cookie hoạt động
});

// 🔹 Cho phép truy cập HttpContext từ View (Razor)
builder.Services.AddHttpContextAccessor();


// ========================================
// 🚀 2️⃣ Cấu hình Pipeline (Middleware)
// ========================================
var app = builder.Build();

// Xử lý lỗi và bảo mật HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/mhkHome/Error");
    app.UseHsts();
}

// 🔹 Bắt buộc HTTPS
app.UseHttpsRedirection();

// 🔹 Cho phép truy cập file tĩnh (CSS, JS, hình ảnh)
app.UseStaticFiles();

// 🔹 Kích hoạt định tuyến (Routing)
app.UseRouting();

// 🔹 Kích hoạt Session (phải nằm giữa UseRouting và UseAuthorization)
app.UseSession();

// 🔹 Phân quyền (nếu bạn dùng Authentication)
app.UseAuthorization();

// 🔹 Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=mhkHome}/{action=Index}/{id?}");

// Chạy ứng dụng
app.Run();
