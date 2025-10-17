using Microsoft.AspNetCore.Mvc;
using MHKCafe.Models;
using System.Collections.Generic;

namespace MHKCafe.Controllers
{
    public class mhkBlogController : Controller
    {
        public IActionResult Index()
        {
            var blogs = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    TieuDe = "Giá cà phê thế giới tăng mạnh trong tháng 10",
                    MoTa = "Giá cà phê Arabica và Robusta tiếp tục tăng do nguồn cung hạn chế từ Brazil và Việt Nam.",
                    NoiDung = "Theo báo cáo mới nhất, giá cà phê Arabica tăng 4.2% trong tháng 10, trong khi Robusta tăng 6.8% do nhu cầu tiêu thụ mạnh ở châu Âu và Mỹ.",
                    HinhAnh = "/images/blog1.jpg",
                    NgayDang = "17/10/2025"
                },
                new BlogPost
                {
                    Id = 2,
                    TieuDe = "Xu hướng cà phê sạch và hữu cơ tại Việt Nam",
                    MoTa = "Người tiêu dùng Việt Nam ngày càng ưa chuộng cà phê hữu cơ, mang lại lợi ích sức khỏe và bảo vệ môi trường.",
                    NoiDung = "Các chuỗi cà phê lớn như MHKCafe, The Coffee House, và Highlands đang mở rộng nguồn cung cà phê sạch, truy xuất nguồn gốc rõ ràng từ nông trại đến ly cà phê.",
                    HinhAnh = "/images/blog2.jpg",
                    NgayDang = "12/10/2025"
                },
                new BlogPost
                {
                    Id = 3,
                    TieuDe = "Thị trường cà phê hòa tan Việt Nam đạt mốc mới",
                    MoTa = "Do nhu cầu tiện lợi, cà phê hòa tan nội địa đã tăng trưởng 15% so với cùng kỳ năm trước.",
                    NoiDung = "Theo hiệp hội cà phê Việt Nam, các thương hiệu nội địa như MHKCafe Instant đang dần chiếm lĩnh thị trường trong nước và mở rộng xuất khẩu sang châu Á.",
                    HinhAnh = "/images/blog3.jpg",
                    NgayDang = "05/10/2025"
                }
            };

            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            // Tạm thời dữ liệu tĩnh, có thể thay bằng database
            var blogs = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    TieuDe = "Giá cà phê thế giới tăng mạnh trong tháng 10",
                    MoTa = "Giá cà phê tăng do nguồn cung hạn chế.",
                    NoiDung = "Chi tiết: Giá cà phê Arabica tăng mạnh...",
                    HinhAnh = "/images/blog1.jpg",
                    NgayDang = "17/10/2025"
                }
            };

            var blog = blogs.Find(b => b.Id == id);
            return View(blog);
        }
    }
}
