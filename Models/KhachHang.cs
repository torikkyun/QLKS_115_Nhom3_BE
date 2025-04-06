using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string Ho { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? Email { get; set; }

    public string Sdt { get; set; } = null!;

    public string? Cccd { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
}
