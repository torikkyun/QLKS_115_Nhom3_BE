use data_QLKS_115_Nhom3
go

create table KhachHang(
	MaKhachHang int identity(1, 1) primary key,
	Ho nvarchar(50) not null,
	Ten nvarchar(20) not null,
	Email nvarchar(50) null,
	SDT varchar(20) not null,
	CCCD varchar(20) unique
)

create table VaiTro(
	MaVaiTro int identity(1, 1) primary key,
	TenVaiTro nvarchar(50) not null,
	GhiChu nvarchar(100) null
)

create table LoaiPhong(
	MaLoaiPhong int identity(1, 1) primary key,
	SoGiuong int not null,
	GhiChu nvarchar(100) null
)

create table DichVu(
	MaDichVu int identity(1, 1) primary key,
	TenDichVu nvarchar(50) not null,
	LoaiDichVu nvarchar(50) not null,
	GhiChu nvarchar(100) null
)

create table KhuyenMai(
	MaKhuyenMai int identity(1, 1) primary key,
	TenKhuyenMai nvarchar(50) not null,
	KieuKhuyenMai nvarchar(50) not null,
	MoTaKhuyenMai nvarchar(100) not null,
	NgayBatDau date not null,
	NgayKetThuc date not null,
	GiaTriKhuyenMai int not null,
	GhiChu nvarchar(100) null
)

create table NhanVien(
	MaNhanVien int identity(1, 1) primary key,
	Ho nvarchar(50) not null,
	Ten nvarchar(20) not null,
	Email nvarchar(50) null,
	SDT varchar(20) not null,
	CCCD varchar(20) unique,
	MatKhau nvarchar(50) not null,
	VaiTro int,
	constraint fk_NhanVien_VaiTro foreign key (VaiTro)
	references VaiTro(MaVaiTro)
)

create table Phong(
	MaPhong int identity(1, 1) primary key,
	SoPhong varchar(5) not null,
	TinhTrangPhong bit not null,
	LoaiPhong int,
	constraint fk_Phong_LoaiPhong foreign key (LoaiPhong)
	references LoaiPhong(MaLoaiPhong)
)

create table DatPhong(
	MaDatPhong int identity(1, 1) primary key,
	NgayDatPhong date default(getdate()) not null,
	SoPhongDat int not null,
	GhiChu nvarchar(100) null,
	NhanVien int,
	KhachHang int,
	constraint fk_DatPhong_NhanVien foreign key (NhanVien)
	references NhanVien(MaNhanVien),
	constraint fk_DatPhong_KhachHang foreign key (KhachHang)
	references KhachHang(MaKhachHang)
)

create table HoaDon(
	DatPhong int primary key,
	NgayXuatHoaDon date default(getdate()) not null,
	TinhTrangThanhToan bit not null,
	TongTien int not null,
	TongTienPhong int not null,
	TongTienDichVu int not null,
	GhiChu nvarchar(100) null,
	NhanVien int,
	constraint fk_HoaDon_DatPhong foreign key (DatPhong) 
	references DatPhong(MaDatPhong),
	constraint fk_HoaDon_NhanVien foreign key (NhanVien) 
	references NhanVien(MaNhanVien)
)

create table ChiTietDatPhong(
	Phong int,
	DatPhong int,
	KhuyenMai int,
	NgayTraPhong date not null,
	NgayNhanPhong date not null,
	GiaPhong int not null,
	constraint pk_ChiTietDatPhong primary key (Phong, DatPhong),
	constraint fk_ChiTietDatPhong_Phong foreign key (Phong) 
	references Phong(MaPhong),
	constraint fk_ChiTietDatPhong_DatPhong foreign key (DatPhong)
	references DatPhong(MaDatPhong),
	constraint fk_ChiTietDatPhong_KhuyenMai foreign key (KhuyenMai)
	references KhuyenMai(MaKhuyenMai)
)

create table ChiTietDichVu(
	Phong int,
	DatPhong int,
	DichVu int,
	GiaDichVu int not null,
	NgaySuDung date default(getDate()) not null,
	constraint pk_ChiTietDichVu primary key (Phong, DatPhong, DichVu),
	constraint fk_ChiTietDichVu_ChiTietDatPhong foreign key (Phong, DatPhong)
	references ChiTietDatPhong(Phong, DatPhong),
	constraint fk_ChiTietDichVu_DichVu foreign key (DichVu)
	references DichVu(MaDichVu)
)

-- Script xóa toàn bộ ràng buộc và bảng trong CSDL
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
              + N'.' + QUOTENAME(OBJECT_NAME(parent_object_id))
              + N' DROP CONSTRAINT ' + QUOTENAME(name) + N';'
FROM sys.foreign_keys;
EXEC sp_executesql @sql;
SET @sql = N'';
SELECT @sql += N'DROP TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(object_id))
              + N'.' + QUOTENAME(name) + N';'
FROM sys.objects
WHERE type = 'U';
EXEC sp_executesql @sql;
