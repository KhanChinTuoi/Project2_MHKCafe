using System;
using System.Collections.Generic;

namespace MHKCafe.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? Quyen { get; set; }

    public DateTime? NgayTao { get; set; }

    public string TrangThai { get; set; }
}
