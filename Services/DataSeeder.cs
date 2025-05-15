
using QLKS_115_Nhom3_BE.Models;
using QLKS_115_Nhom3_BE.Helpers;

namespace QLKS_115_Nhom3_BE.Services
{
    public static class DataSeeder
    {
      public static void Seed(DataQlks115Nhom3Context context)
      {
          if (!context.LoaiDichVuEnums.Any())
          {
              context.LoaiDichVuEnums.AddRange(
                  new LoaiDichVuEnum { Id = 1, TenLoai = "Dịch vụ phòng", GiaDichVu = 100000 },
                  new LoaiDichVuEnum { Id = 2, TenLoai = "Dịch vụ ăn uống", GiaDichVu = 200000 },
                  new LoaiDichVuEnum { Id = 3, TenLoai = "Dịch vụ giải trí", GiaDichVu = 300000 },
                  new LoaiDichVuEnum { Id = 4, TenLoai = "Dịch vụ spa", GiaDichVu = 400000 },
                  new LoaiDichVuEnum { Id = 5, TenLoai = "Dịch vụ giặt ủi", GiaDichVu = 150000 },
                  new LoaiDichVuEnum { Id = 6, TenLoai = "Dịch vụ khác", GiaDichVu = 50000 }
              );
              context.SaveChanges();
          }

          if (!context.DichVus.Any())
          {
              context.DichVus.AddRange(
                  new DichVu
                  {
                      TenDichVu = "Dọn phòng",
                      LoaiDichVu = 1,
                      GhiChu = "Dịch vụ dọn dẹp phòng"
                  },
                  new DichVu
                  {
                      TenDichVu = "Giặt ủi",
                      LoaiDichVu = 5,
                      GhiChu = "Dịch vụ giặt ủi quần áo"
                  },
                  new DichVu
                  {
                      TenDichVu = "Đồ ăn sáng",
                      LoaiDichVu = 2,
                      GhiChu = "Bữa sáng tự chọn"
                  },
                  new DichVu
                  {
                      TenDichVu = "Đồ ăn trưa",
                      LoaiDichVu = 2,
                      GhiChu = "Bữa trưa tự chọn"
                  },
                  new DichVu
                  {
                      TenDichVu = "Đồ ăn tối",
                      LoaiDichVu = 2,
                      GhiChu = "Bữa tối tự chọn"
                  },
                  new DichVu
                  {
                      TenDichVu = "Massage",
                      LoaiDichVu = 4,
                      GhiChu = "Dịch vụ massage thư giãn"
                  },
                  new DichVu
                  {
                      TenDichVu = "Karaoke",
                      LoaiDichVu = 3,
                      GhiChu = "Dịch vụ karaoke"
                  },
                  new DichVu
                  {
                      TenDichVu = "Thuê xe",
                      LoaiDichVu = 6,
                      GhiChu = "Dịch vụ thuê xe tự lái"
                  }
              );
              context.SaveChanges();
          }

          if (!context.KieuKhuyenMaiEnums.Any())
          {
              context.KieuKhuyenMaiEnums.AddRange(
                  new KieuKhuyenMaiEnum { Id = 1, TenKieu = "Phần trăm" },
                  new KieuKhuyenMaiEnum { Id = 2, TenKieu = "Giảm giá trực tiếp"}
              );
              context.SaveChanges();
          }

          if (!context.TinhTrangPhongEnums.Any())
          {
              context.TinhTrangPhongEnums.AddRange(
                  new TinhTrangPhongEnum { Id = 0, TenTinhTrang = "Đang sử dụng" },
                  new TinhTrangPhongEnum { Id = 1, TenTinhTrang = "Trống" },
                  new TinhTrangPhongEnum { Id = 2, TenTinhTrang = "Bảo trì"}
              );
              context.SaveChanges();
          }

          if (!context.TinhTrangThanhToanEnums.Any())
          {
              context.TinhTrangThanhToanEnums.AddRange(
                  new TinhTrangThanhToanEnum { Id = 1, TenTinhTrang = "Chưa thanh toán" },
                  new TinhTrangThanhToanEnum { Id = 2, TenTinhTrang = "Đã thanh toán" },
                  new TinhTrangThanhToanEnum { Id = 3, TenTinhTrang = "Đang xử lý" },
                  new TinhTrangThanhToanEnum { Id = 4, TenTinhTrang = "Hoàn tiền" },
                  new TinhTrangThanhToanEnum { Id = 5, TenTinhTrang = "Hủy bỏ" }
              );
              context.SaveChanges();
          }

          if (!context.VaiTros.Any())
          {
            context.VaiTros.AddRange(
                new VaiTro {  TenVaiTro = "Quản lý" },
                new VaiTro {  TenVaiTro = "Lễ tân" },
                new VaiTro {  TenVaiTro = "Phục vụ" }
            );
            context.SaveChanges();
          }

          if (!context.LoaiPhongs.Any())
          {
              context.LoaiPhongs.AddRange(
                  new LoaiPhong { GhiChu = "Phòng đơn", SoGiuong = 1, GiaPhong = 500000 },
                  new LoaiPhong { GhiChu = "Phòng đôi", SoGiuong = 2, GiaPhong = 700000 },
                  new LoaiPhong { GhiChu = "Phòng gia đình", SoGiuong = 3, GiaPhong = 1000000 }
              );
              context.SaveChanges();
          }

          if (!context.Phongs.Any())
          {
              context.Phongs.AddRange(
                  new Phong {  SoPhong = "101", TinhTrangPhong = 0, LoaiPhong = 1 },
                  new Phong {  SoPhong = "102", TinhTrangPhong = 0, LoaiPhong = 1 },
                  new Phong {  SoPhong = "103", TinhTrangPhong = 0, LoaiPhong = 1 },
                  new Phong {  SoPhong = "104", TinhTrangPhong = 0, LoaiPhong = 2 },
                  new Phong {  SoPhong = "105", TinhTrangPhong = 0, LoaiPhong = 2 },
                  new Phong {  SoPhong = "106", TinhTrangPhong = 0, LoaiPhong = 2 },
                  new Phong {  SoPhong = "107", TinhTrangPhong = 0, LoaiPhong = 2 },
                  new Phong {  SoPhong = "108", TinhTrangPhong = 0, LoaiPhong = 1 },
                  new Phong {  SoPhong = "109", TinhTrangPhong = 0, LoaiPhong = 3 },
                  new Phong {  SoPhong = "110", TinhTrangPhong = 0, LoaiPhong = 3 }
              );
              context.SaveChanges();
          }

          if (!context.KhachHangs.Any())
          {
              context.KhachHangs.AddRange(
                  new KhachHang
                  {
                      Ho = "Nguyễn",
                      Ten = "Văn An",
                      Email = "nguyenvanan@gmail.com",
                      Sdt = "0901234567",
                      Cccd = "079201001234"
                  },
                  new KhachHang
                  {
                      Ho = "Trần",
                      Ten = "Thị Bình",
                      Email = "tranbinh@gmail.com",
                      Sdt = "0912345678",
                      Cccd = "079201002345"
                  },
                  new KhachHang
                  {
                      Ho = "Lê",
                      Ten = "Hoàng Cường",
                      Email = "lehoangcuong@gmail.com",
                      Sdt = "0923456789",
                      Cccd = "079201003456"
                  },
                  new KhachHang
                  {
                      Ho = "Phạm",
                      Ten = "Minh Đức",
                      Email = "phamminhduc@gmail.com",
                      Sdt = "0934567890",
                      Cccd = "079201004567"
                  },
                  new KhachHang
                  {
                      Ho = "Hoàng",
                      Ten = "Thị Em",
                      Email = "hoangem@gmail.com",
                      Sdt = "0945678901",
                      Cccd = "079201005678"
                  }
              );
              context.SaveChanges();
          }

          if (!context.NhanViens.Any())
          {
              var passwordHasher = new PasswordHasher();

              context.NhanViens.AddRange(
                  new NhanVien
                  {
                      Ho = "Nguyễn",
                      Ten = "Văn Quản",
                      Email = "admin@gmail.com",
                      Sdt = "0901234567",
                      Cccd = "079201001234",
                      MatKhau = passwordHasher.HashPassword("123456"),
                      VaiTro = 1
                  },
                  new NhanVien
                  {
                      Ho = "Trần",
                      Ten = "Thị Tân",
                      Email = "letan@gmail.com",
                      Sdt = "0912345678",
                      Cccd = "079201002345",
                      MatKhau = passwordHasher.HashPassword("123456"),
                      VaiTro = 2
                  },
                  new NhanVien
                  {
                      Ho = "Lê",
                      Ten = "Thị Phục",
                      Email = "phucvu@gmail.com",
                      Sdt = "0923456789",
                      Cccd = "079201003456",
                      MatKhau = passwordHasher.HashPassword("123456"),
                      VaiTro = 3
                  }
              );
              context.SaveChanges();
          }

          if (!context.KhuyenMais.Any())
          {
              context.KhuyenMais.AddRange(
                  new KhuyenMai
                  {
                      TenKhuyenMai = "Khuyến mãi mùa hè",
                      KieuKhuyenMai = 1,
                      MoTaKhuyenMai = "Giảm giá 20% cho tất cả các phòng trong mùa hè",
                      NgayBatDau = new DateOnly(2025, 6, 1),
                      NgayKetThuc = new DateOnly(2025, 8, 31),
                      GiaTriKhuyenMai = 20,
                      GhiChu = "Áp dụng cho tất cả các loại phòng"
                  },
                  new KhuyenMai
                  {
                      TenKhuyenMai = "Ưu đãi đặc biệt cuối tuần",
                      KieuKhuyenMai = 2,
                      MoTaKhuyenMai = "Giảm trực tiếp 200,000 VNĐ cho đặt phòng cuối tuần",
                      NgayBatDau = new DateOnly(2025, 1, 1),
                      NgayKetThuc = new DateOnly(2025, 12, 31),
                      GiaTriKhuyenMai = 200000,
                      GhiChu = "Chỉ áp dụng cho đặt phòng vào thứ 6, thứ 7, chủ nhật"
                  },
                  new KhuyenMai
                  {
                      TenKhuyenMai = "Khuyến mãi đặt sớm",
                      KieuKhuyenMai = 1,
                      MoTaKhuyenMai = "Giảm 15% cho đặt phòng trước 30 ngày",
                      NgayBatDau = new DateOnly(2025, 1, 1),
                      NgayKetThuc = new DateOnly(2025, 12, 31),
                      GiaTriKhuyenMai = 15,
                      GhiChu = "Áp dụng khi đặt phòng trước ngày check-in ít nhất 30 ngày"
                  },
                  new KhuyenMai
                  {
                      TenKhuyenMai = "Ưu đãi đặt phòng dài hạn",
                      KieuKhuyenMai = 2,
                      MoTaKhuyenMai = "Giảm 500,000 VNĐ cho đặt phòng từ 7 đêm trở lên",
                      NgayBatDau = new DateOnly(2025, 1, 1),
                      NgayKetThuc = new DateOnly(2025, 12, 31),
                      GiaTriKhuyenMai = 500000,
                      GhiChu = "Áp dụng cho đặt phòng từ 7 đêm trở lên"
                  },
                  new KhuyenMai
                  {
                      TenKhuyenMai = "Khuyến mãi Tết 2025",
                      KieuKhuyenMai = 1,
                      MoTaKhuyenMai = "Giảm 25% cho đặt phòng trong dịp Tết",
                      NgayBatDau = new DateOnly(2025, 2, 1),
                      NgayKetThuc = new DateOnly(2025, 2, 28),
                      GiaTriKhuyenMai = 25,
                      GhiChu = "Áp dụng cho đặt phòng trong tháng 2/2025"
                  }
              );
              context.SaveChanges();
          }

      }
  }
}
