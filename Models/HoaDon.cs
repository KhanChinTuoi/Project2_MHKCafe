using System;
using System.Collections.Generic;

namespace MHKCafe.Models;

public partial class HoaDon
{
    public int HoaDonId { get; set; }

    public int? NguoiDungId { get; set; }

    public string? TenKhachHang { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public DateTime? NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual NguoiDung? NguoiDung { get; set; }
}
