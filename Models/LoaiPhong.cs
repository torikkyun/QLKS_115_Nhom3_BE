using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class LoaiPhong
{
    public int MaLoaiPhong { get; set; }

    public int SoGiuong { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
