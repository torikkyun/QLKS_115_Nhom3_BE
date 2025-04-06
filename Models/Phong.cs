using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class Phong
{
    public int MaPhong { get; set; }

    public string SoPhong { get; set; } = null!;

    public bool TinhTrangPhong { get; set; }

    public int? LoaiPhong { get; set; }

    public virtual ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();

    public virtual LoaiPhong? LoaiPhongNavigation { get; set; }
}
