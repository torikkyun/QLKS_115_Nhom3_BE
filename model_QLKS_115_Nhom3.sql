USE data_QLKS_115_Nhom3;
GO

CREATE TABLE LoaiDichVuEnum (
    Id TINYINT PRIMARY KEY,
    TenLoai NVARCHAR(50) NOT NULL
);

CREATE TABLE KieuKhuyenMaiEnum (
    Id TINYINT PRIMARY KEY,
    TenKieu NVARCHAR(50) NOT NULL
);

CREATE TABLE TinhTrangPhongEnum (
    Id TINYINT PRIMARY KEY,
    TenTinhTrang NVARCHAR(50) NOT NULL
);

CREATE TABLE TinhTrangThanhToanEnum (
    Id TINYINT PRIMARY KEY,
    TenTinhTrang NVARCHAR(50) NOT NULL
);

CREATE TABLE KhachHang(
    MaKhachHang INT IDENTITY(1, 1) PRIMARY KEY,
    Ho NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NULL,
    SDT VARCHAR(20) NOT NULL,
    CCCD VARCHAR(20) UNIQUE
);

CREATE TABLE VaiTro(
    MaVaiTro INT IDENTITY(1, 1) PRIMARY KEY,
    TenVaiTro NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(100) NULL
);

CREATE TABLE LoaiPhong(
    MaLoaiPhong INT IDENTITY(1, 1) PRIMARY KEY,
    SoGiuong INT NOT NULL,
    GhiChu NVARCHAR(100) NULL
);

CREATE TABLE DichVu(
    MaDichVu INT IDENTITY(1, 1) PRIMARY KEY,
    TenDichVu NVARCHAR(50) NOT NULL,
    LoaiDichVu TINYINT NOT NULL,
    GhiChu NVARCHAR(100) NULL,
    CONSTRAINT FK_DichVu_LoaiDichVu FOREIGN KEY (LoaiDichVu)
    REFERENCES LoaiDichVuEnum(Id)
);

CREATE TABLE KhuyenMai(
    MaKhuyenMai INT IDENTITY(1, 1) PRIMARY KEY,
    TenKhuyenMai NVARCHAR(50) NOT NULL,
    KieuKhuyenMai TINYINT NOT NULL,
    MoTaKhuyenMai NVARCHAR(100) NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    GiaTriKhuyenMai INT NOT NULL,
    GhiChu NVARCHAR(100) NULL,
    CONSTRAINT FK_KhuyenMai_KieuKhuyenMai FOREIGN KEY (KieuKhuyenMai)
    REFERENCES KieuKhuyenMaiEnum(Id)
);

CREATE TABLE NhanVien(
    MaNhanVien INT IDENTITY(1, 1) PRIMARY KEY,
    Ho NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NULL,
    SDT VARCHAR(20) NOT NULL,
    CCCD VARCHAR(20) UNIQUE,
    MatKhau NVARCHAR(50) NOT NULL,
    VaiTro INT NOT NULL,
    CONSTRAINT FK_NhanVien_VaiTro FOREIGN KEY (VaiTro)
    REFERENCES VaiTro(MaVaiTro)
);

CREATE TABLE Phong(
    MaPhong INT IDENTITY(1, 1) PRIMARY KEY,
    SoPhong VARCHAR(5) NOT NULL,
    TinhTrangPhong TINYINT NOT NULL,
    LoaiPhong INT,
    CONSTRAINT FK_Phong_LoaiPhong FOREIGN KEY (LoaiPhong)
    REFERENCES LoaiPhong(MaLoaiPhong),
    CONSTRAINT FK_Phong_TinhTrangPhong FOREIGN KEY (TinhTrangPhong)
    REFERENCES TinhTrangPhongEnum(Id)
);

CREATE TABLE DatPhong(
    MaDatPhong INT IDENTITY(1, 1) PRIMARY KEY,
    NgayDatPhong DATE DEFAULT(GETDATE()) NOT NULL,
    SoPhongDat INT NOT NULL,
    GhiChu NVARCHAR(100) NULL,
    NhanVien INT,
    KhachHang INT,
    CONSTRAINT FK_DatPhong_NhanVien FOREIGN KEY (NhanVien)
    REFERENCES NhanVien(MaNhanVien),
    CONSTRAINT FK_DatPhong_KhachHang FOREIGN KEY (KhachHang)
    REFERENCES KhachHang(MaKhachHang)
);

CREATE TABLE HoaDon(
    DatPhong INT PRIMARY KEY,
    NgayXuatHoaDon DATE DEFAULT(GETDATE()) NOT NULL,
    TinhTrangThanhToan BIT NOT NULL,
    TongTien INT NOT NULL,
    TongTienPhong INT NOT NULL,
    TongTienDichVu INT NOT NULL,
    GhiChu NVARCHAR(100) NULL,
    NhanVien INT,
    CONSTRAINT FK_HoaDon_DatPhong FOREIGN KEY (DatPhong)
    REFERENCES DatPhong(MaDatPhong),
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (NhanVien)
    REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE ChiTietDatPhong(
    Phong INT,
    DatPhong INT,
    KhuyenMai INT NULL,
    NgayTraPhong DATE NOT NULL,
    NgayNhanPhong DATE NOT NULL,
    GiaPhong INT NOT NULL,
    CONSTRAINT PK_ChiTietDatPhong PRIMARY KEY (Phong, DatPhong),
    CONSTRAINT FK_ChiTietDatPhong_Phong FOREIGN KEY (Phong)
    REFERENCES Phong(MaPhong),
    CONSTRAINT FK_ChiTietDatPhong_DatPhong FOREIGN KEY (DatPhong)
    REFERENCES DatPhong(MaDatPhong),
    CONSTRAINT FK_ChiTietDatPhong_KhuyenMai FOREIGN KEY (KhuyenMai)
    REFERENCES KhuyenMai(MaKhuyenMai)
);


CREATE TABLE ChiTietDichVu(
    Phong INT,
    DatPhong INT,
    DichVu INT,
    GiaDichVu INT NOT NULL,
    NgaySuDung DATE DEFAULT(GETDATE()) NOT NULL,
    CONSTRAINT PK_ChiTietDichVu PRIMARY KEY (Phong, DatPhong, DichVu),
    CONSTRAINT FK_ChiTietDichVu_ChiTietDatPhong FOREIGN KEY (Phong, DatPhong)
    REFERENCES ChiTietDatPhong(Phong, DatPhong),
    CONSTRAINT FK_ChiTietDichVu_DichVu FOREIGN KEY (DichVu)
    REFERENCES DichVu(MaDichVu)
);

ALTER TABLE ChiTietDatPhong DROP COLUMN GiaPhong
ALTER TABLE ChiTietDichVu DROP COLUMN GiaDichVu

ALTER TABLE HoaDon
ALTER COLUMN TinhTrangThanhToan TINYINT NOT NULL;
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_TinhTrangThanhToan
FOREIGN KEY (TinhTrangThanhToan)
REFERENCES TinhTrangThanhToanEnum(Id);

-- Script xóa toàn bộ ràng buộc và bảng trong CSDL
-- DECLARE @sql NVARCHAR(MAX) = N'';
-- SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
--               + N'.' + QUOTENAME(OBJECT_NAME(parent_object_id))
--               + N' DROP CONSTRAINT ' + QUOTENAME(name) + N';'
-- FROM sys.foreign_keys;
-- EXEC sp_executesql @sql;
-- SET @sql = N'';
-- SELECT @sql += N'DROP TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(object_id))
--               + N'.' + QUOTENAME(name) + N';'
-- FROM sys.objects
-- WHERE type = 'U';
-- EXEC sp_executesql @sql;

-- DECLARE @TableName NVARCHAR(128)
-- DECLARE @SQL NVARCHAR(MAX)

-- DECLARE TableCursor CURSOR FOR
-- SELECT t.name
-- FROM sys.tables t
-- JOIN sys.columns c ON t.object_id = c.object_id
-- WHERE c.is_identity = 1

-- OPEN TableCursor
-- FETCH NEXT FROM TableCursor INTO @TableName

-- WHILE @@FETCH_STATUS = 0
-- BEGIN
--     SET @SQL = 'DBCC CHECKIDENT (''' + @TableName + ''', RESEED, 0)'
--     EXEC sp_executesql @SQL
--     FETCH NEXT FROM TableCursor INTO @TableName
-- END

-- CLOSE TableCursor
-- DEALLOCATE TableCursor
