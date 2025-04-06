using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string Ho { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? Email { get; set; }

    public string Sdt { get; set; } = null!;

    public string? Cccd { get; set; }

    public string MatKhau { get; set; } = null!;

    public int? VaiTro { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual VaiTro? VaiTroNavigation { get; set; }
}
