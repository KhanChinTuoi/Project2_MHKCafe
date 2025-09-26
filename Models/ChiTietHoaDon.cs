using System;
using System.Collections.Generic;

namespace MHKCafe.Models;

public partial class ChiTietHoaDon
{
    public int ChiTietId { get; set; }

    public int HoaDonId { get; set; }

    public int ThucDonId { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual HoaDon HoaDon { get; set; } = null!;

    public virtual ThucDon ThucDon { get; set; } = null!;
}
