using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class HoaDon
{
    public int DatPhong { get; set; }

    public DateOnly NgayXuatHoaDon { get; set; }

    public byte TinhTrangThanhToan { get; set; }

    public int TongTien { get; set; }

    public int TongTienPhong { get; set; }

    public int TongTienDichVu { get; set; }

    public string? GhiChu { get; set; }

    public int? NhanVien { get; set; }

    public virtual DatPhong DatPhongNavigation { get; set; } = null!;

    public virtual NhanVien? NhanVienNavigation { get; set; }

    public virtual TinhTrangThanhToanEnum TinhTrangThanhToanNavigation { get; set; } = null!;
}
