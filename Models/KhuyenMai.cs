using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    public string TenKhuyenMai { get; set; } = null!;

    public byte KieuKhuyenMai { get; set; }

    public string MoTaKhuyenMai { get; set; } = null!;

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public int GiaTriKhuyenMai { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();

    public virtual KieuKhuyenMaiEnum KieuKhuyenMaiNavigation { get; set; } = null!;
}
