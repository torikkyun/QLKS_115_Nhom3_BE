using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class DatPhong
{
    public int MaDatPhong { get; set; }

    public DateOnly NgayDatPhong { get; set; }

    public int SoPhongDat { get; set; }

    public string? GhiChu { get; set; }

    public int? NhanVien { get; set; }

    public int? KhachHang { get; set; }

    public virtual ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; } = new List<ChiTietDatPhong>();

    public virtual HoaDon? HoaDon { get; set; }

    public virtual KhachHang? KhachHangNavigation { get; set; }

    public virtual NhanVien? NhanVienNavigation { get; set; }
}
