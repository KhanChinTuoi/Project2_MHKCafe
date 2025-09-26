using System;
using System.Collections.Generic;

namespace MHKCafe.Models;

public partial class ThucDon
{
    public int ThucDonId { get; set; }

    public string TenMon { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal Gia { get; set; }

    public string? HinhAnh { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
}
