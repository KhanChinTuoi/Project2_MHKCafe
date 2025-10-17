using Microsoft.AspNetCore.Mvc;
using MHKCafe.Models;
using System.Collections.Generic;

namespace MHKCafe.Controllers
{
    public class mhkChiNhanhController : Controller
    {
        public IActionResult Index()
        {
            var chiNhanhs = new List<ChiNhanh>
            {
                new ChiNhanh
                {
                    Ten = "MHKCafe - Trung Tâm TP.HCM",
                    DiaChi = "123 Nguyễn Huệ, Quận 1, TP.HCM",
                    SoDienThoai = "0909 123 456",
                    HinhAnh = "/images/chinhanh1.jpg"
                },
                new ChiNhanh
                {
                    Ten = "MHKCafe - Hà Nội",
                    DiaChi = "45 Lý Thường Kiệt, Hoàn Kiếm, Hà Nội",
                    SoDienThoai = "0912 456 789",
                    HinhAnh = "/images/chinhanh2.jpg"
                },
                new ChiNhanh
                {
                    Ten = "MHKCafe - Đà Nẵng",
                    DiaChi = "78 Bạch Đằng, Quận Hải Châu, Đà Nẵng",
                    SoDienThoai = "0977 333 888",
                    HinhAnh = "/images/chinhanh3.jpg"
                }
            };

            return View(chiNhanhs);
        }
    }
}
