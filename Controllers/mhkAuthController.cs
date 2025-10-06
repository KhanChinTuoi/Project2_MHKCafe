using Microsoft.AspNetCore.Mvc;
using MHKCafe.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MHKCafe.Controllers
{
    public class mhkAuthController : Controller
    {
        private readonly MhkcafeContext _context;

        public mhkAuthController(MhkcafeContext context)
        {
            _context = context;
        }

        // GET: mhkAuth/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Hiển thị thông báo thành công từ đăng ký nếu có
            if (TempData["Success"] != null)
            {
                ViewBag.Success = TempData["Success"];
            }
            return View();
        }

        // POST: mhkAuth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string matKhau)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email && a.MatKhau == matKhau);

            if (admin != null)
            {
                HttpContext.Session.SetInt32("AdminId", admin.AdminId);
                HttpContext.Session.SetString("HoTen", admin.HoTen);
                HttpContext.Session.SetString("Quyen", admin.Quyen ?? "Manager");

                return RedirectToAction("Index", "mhkAdmins");
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng!";
            return View();
        }

        // GET: mhkAuth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: mhkAuth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string hoTen, string email, string matKhau, string xacNhanMatKhau, string quyen = "User")
        {
            // Giữ lại giá trị khi có lỗi
            ViewBag.HoTen = hoTen;
            ViewBag.Email = email;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMatKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(email))
            {
                ViewBag.Error = "Email không hợp lệ!";
                return View();
            }

            // Kiểm tra mật khẩu có khớp nhau không
            if (matKhau != xacNhanMatKhau)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp!";
                return View();
            }

            // Kiểm tra độ mạnh của mật khẩu (ít nhất 6 ký tự)
            if (matKhau.Length < 6)
            {
                ViewBag.Error = "Mật khẩu phải có ít nhất 6 ký tự!";
                return View();
            }

            // Kiểm tra email đã tồn tại chưa
            var existingAdmin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email);

            if (existingAdmin != null)
            {
                ViewBag.Error = "Email đã được sử dụng! Vui lòng chọn email khác.";
                return View();
            }

            try
            {
                // Tạo tài khoản mới
                var newAdmin = new Admin
                {
                    HoTen = hoTen.Trim(),
                    Email = email.Trim(),
                    MatKhau = matKhau,
                    Quyen = quyen,
                    NgayTao = DateTime.Now,
                    TrangThai = "Active"
                };

                _context.Admins.Add(newAdmin);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi xảy ra khi đăng ký: " + ex.Message;
                return View();
            }
        }

        // GET: mhkAuth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "mhkAuth");
        }

        // Phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}