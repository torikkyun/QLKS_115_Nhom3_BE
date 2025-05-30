﻿namespace QLKS_115_Nhom3_BE.DTOs
{
    public class PhongDichVuDTO
    {
        public int MaPhong { get; set; }
        public List<int>? DichVuIds { get; set; }
        public DateOnly NgayNhanPhong { get; set; }
        public DateOnly NgayTraPhong { get; set; }
    }

    public class DatPhongRequestDTO
    {
        public List<PhongDichVuDTO> PhongDichVus { get; set; }
        public int? KhuyenMaiId { get; set; }
        public string? GhiChu { get; set; }
        public int MaKhachHang { get; set; }
    }
    public class TraPhongRequestDTO
    {
        public int MaPhong { get; set; }
        public int MaDatPhong { get; set; }
    }
    public class DatPhongFullDTO
    {
        public int MaDatPhong { get; set; }
        public DateOnly NgayDatPhong { get; set; }
        public int SoPhongDat { get; set; }
        public string? GhiChu { get; set; }
        public List<MaPhongSoPhong> DanhSachPhong { get; set; }
        public NhanVienDTO NhanVien { get; set; } = new();
        public KhachHangDTO KhachHang { get; set; } = new();
    }

}
