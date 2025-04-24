ALTER TABLE LoaiPhong ADD GiaPhong INT NOT NULL DEFAULT 0;
UPDATE LoaiPhong
SET LoaiPhong.GiaPhong = 
    CASE MaLoaiPhong
        WHEN 1 THEN 500000
        WHEN 2 THEN 700000
        WHEN 3 THEN 1000000
        ELSE 0
    END;

ALTER TABLE LoaiDichVuEnum ADD GiaDichVu INT NOT NULL DEFAULT 0;
UPDATE LoaiDichVuEnum
SET LoaiDichVuEnum.GiaDichVu =
    CASE Id
        WHEN 1 THEN 100000
        WHEN 2 THEN 200000
        WHEN 3 THEN 300000
        WHEN 4 THEN 400000
        WHEN 5 THEN 150000
        WHEN 6 THEN 50000 
        ELSE 0
    END;

ALTER TABLE Phong
ADD CONSTRAINT UQ_Phong_SoPhong UNIQUE (SoPhong);

ALTER TABLE Phong
ADD DEFAULT 1 FOR TinhTrangPhong;
ALTER TABLE Phong
ADD DEFAULT 1 FOR LoaiPhong;

-- CREATE TRIGGER trg_CapNhatTinhTrangPhong
-- ON ChiTietDatPhong
-- AFTER INSERT, DELETE
-- AS
-- BEGIN
--     SET NOCOUNT ON;

--     -- Khi thêm dòng => set TinhTrangPhong = 0 (đã đặt)
--     UPDATE p
--     SET p.TinhTrangPhong = 0
--     FROM Phong p
--     INNER JOIN inserted i ON p.MaPhong = i.Phong;

--     -- Khi xóa dòng => set TinhTrangPhong = 1 (trống)
--     UPDATE p
--     SET p.TinhTrangPhong = 1
--     FROM Phong p
--     INNER JOIN deleted d ON p.MaPhong = d.Phong;
-- END;

CREATE PROCEDURE [dbo].[sp_CapNhatNhanVien]
    @Id INT,
    @Ho NVARCHAR(50) = NULL,
    @Ten NVARCHAR(20) = NULL,
    @Email NVARCHAR(50) = NULL,
    @Sdt VARCHAR(20) = NULL,
    @Cccd VARCHAR(20) = NULL,
    @MaVaiTro INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE NhanVien
    SET 
        Ho = ISNULL(@Ho, Ho),
        Ten = ISNULL(@Ten, Ten),
        Email = ISNULL(@Email, Email),
        Sdt = ISNULL(@Sdt, Sdt),
        CCCD = ISNULL(@Cccd, CCCD),
        VaiTro = CASE
					WHEN @MaVaiTro IS NOT NULL AND EXISTS (SELECT 1 FROM VaiTro WHERE MaVaiTro = @MaVaiTro) 
					THEN @MaVaiTro
					ELSE VaiTro
				END
    WHERE MaNhanVien = @Id;
END


CREATE PROCEDURE [dbo].[sp_CapNhatPhong]
    @Id INT,
    @SoPhong VARCHAR(5) = NULL,
    @LoaiPhong INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Phong
    SET 
        SoPhong = ISNULL(@SoPhong, SoPhong),
        LoaiPhong = CASE
					WHEN @LoaiPhong IS NOT NULL AND EXISTS (SELECT 1 FROM LoaiPhong WHERE MaLoaiPhong = @LoaiPhong) 
					THEN @LoaiPhong
					ELSE LoaiPhong
				END
    WHERE MaPhong = @Id;
END

CREATE PROCEDURE [dbo].[sp_LayDanhSachNhanVien]
    @Page INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính offset
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    -- Lấy dữ liệu phân trang
    SELECT MaNhanVien,
           Ho,
           Ten,
           Email,
           SDT,
           CCCD,
           vt.TenVaiTro,
           vt.GhiChu
    FROM [dbo].[NhanVien] nv
    JOIN [dbo].[VaiTro] vt ON nv.VaiTro = vt.MaVaiTro
    ORDER BY MaNhanVien
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

    -- Trả tổng số bản ghi (nếu cần)
    SELECT COUNT(*) AS TotalRecords
    FROM [dbo].[NhanVien];
END

CREATE PROCEDURE [dbo].[sp_LayDanhSachPhong]
    @Page INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    -- Tính offset
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    -- Lấy dữ liệu phân trang
	SELECT SoPhong, TinhTrangPhong, LoaiPhong, SoGiuong, GhiChu 
	FROM Phong AS p JOIN LoaiPhong AS lp ON lp.MaLoaiPhong = p.LoaiPhong 
    ORDER BY MaPhong
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

    -- Trả tổng số bản ghi (nếu cần)
    SELECT COUNT(*) AS TotalRecords
    FROM [dbo].[Phong];
END
SELECT SoPhong, TinhTrangPhong, LoaiPhong, SoGiuong, GhiChu FROM Phong AS p JOIN LoaiPhong AS lp on lp.MaLoaiPhong = p.LoaiPhong 

CREATE PROCEDURE [dbo].[sp_ThemNhanVien]
	@Ho NVARCHAR(50),
	@Ten NVARCHAR(20),
	@Email NVARCHAR(50),
	@SDT VARCHAR(20),
	@CCCD VARCHAR(20),
	@MatKhau NVARCHAR(255),
	@MaVaiTro INT,
	@MaNhanVien INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO NhanVien (
        Ho,
        Ten,
        Email,
        Sdt,
        CCCD,
        MatKhau,
        VaiTro
    ) VALUES (
        @Ho,
        @Ten,
        @Email,
        @Sdt,
        @Cccd,
        @MatKhau,
        @MaVaiTro
    );
    
    SET @MaNhanVien = SCOPE_IDENTITY();
    
    RETURN 1;
END


CREATE PROCEDURE [dbo].[sp_ThemPhong] 
	@SoPhong VARCHAR(5),
	@TinhTrangPhong INT,
	@LoaiPhong INT,
	@MaPhong INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Phong (SoPhong, TinhTrangPhong, LoaiPhong) VALUES (@SoPhong, @TinhTrangPhong, @LoaiPhong);
	SET @MaPhong = SCOPE_IDENTITY();
    RETURN 1;
END
