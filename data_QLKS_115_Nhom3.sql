INSERT INTO LoaiDichVuEnum (Id, TenLoai) VALUES 
(1, N'Dịch vụ phòng'),
(2, N'Dịch vụ ăn uống'),
(3, N'Dịch vụ giải trí'),
(4, N'Dịch vụ spa'),
(5, N'Dịch vụ giặt ủi'),
(6, N'Dịch vụ khác');

INSERT INTO KieuKhuyenMaiEnum (Id, TenKieu) VALUES 
(1, N'Phần trăm'),
(2, N'Giảm giá trực tiếp'),
(3, N'Đặc biệt');

INSERT INTO TinhTrangPhongEnum (Id, TenTinhTrang) VALUES 
(0, N'Đang sử dụng'),
(1, N'Trống'),
(2, N'Bảo trì');

INSERT INTO VaiTro (TenVaiTro, GhiChu) VALUES 
(N'Quản lý', NULL),
(N'Lễ tân', NULL),
(N'Phục vụ', NULL);

INSERT INTO LoaiPhong (SoGiuong, GhiChu, GiaPhong) VALUES 
(1, N'Phòng đơn', 500000),
(2, N'Phòng đôi', 700000),
(3, N'Phòng gia đình', 1000000);

INSERT INTO NhanVien (Ho, Ten, Email, SDT, CCCD, MatKhau, VaiTro) VALUES 
(N'Nguyễn', N'Văn A', 'a@example.com', '0911111111', '111111111111', 'password', 1),
(N'Trần', N'Thị B', 'b@example.com', '0922222222', '222222222222', 'password', 2),
(N'Lê', N'Văn C', 'c@example.com', '0933333333', '333333333333', 'password', 2),
(N'Phạm', N'Thị D', 'd@example.com', '0944444444', '444444444444', 'password', 3),
(N'Hoàng', N'Văn E', 'e@example.com', '0955555555', '555555555555', 'password', 3);

INSERT INTO Phong (SoPhong, TinhTrangPhong, LoaiPhong) VALUES 
('101', 1, 1),
('102', 1, 1),
('201', 1, 2),
('202', 1, 2),
('301', 1, 3),
('302', 1, 3);

INSERT INTO KhachHang (Ho, Ten, Email, SDT, CCCD)
VALUES
( N'Nguyễn',N'An', 'an.nguyen@gmail.com', '0912345678', '001234567890'),
( N'Trần',	N'Bình', 'binh.tran@yahoo.com', '0987654321', '002345678901'),
( N'Lê',	N'Chi', 'chi.le@outlook.com', '0901122334', '003456789012'),
( N'Phạm',	N'Dương', 'duong.pham@gmail.com', '0933445566', '004567890123'),
( N'Hoàng',	N'Lan', 'lan.hoang@hotmail.com', '0922334455', '005678901234'),
( N'Đỗ',	N'Minh', 'minh.do@gmail.com', '0945566778', '006789012345'),
( N'Vũ',	N'Ngọc', 'ngoc.vu@live.com', '0911223344', '007890123456'),
( N'Bùi',	N'Phát', 'phat.bui@gmail.com', '0967788990', '008901234567'),
( N'Đặng',	N'Quỳnh', 'quynh.dang@gmail.com', '0978899001', '009012345678'),
( N'Lý',	N'Sơn', 'son.ly@gmail.com', '0956677889', '010123456789');

INSERT INTO DatPhong ( NgayDatPhong, SoPhongDat, GhiChu, NhanVien, KhachHang)
VALUES 
('2025-04-01',	201, 'Khách ở 2 đêm',     3, 1),
('2025-04-02',	202, 'Khách quen',        4, 2),
('2025-04-03',	203, 'Khách đoàn',        5, 3),
('2025-04-04',	204, NULL,                3, 4),
('2025-04-05',	205, NULL,                4, 5),
('2025-04-06',	206, 'Check-in trễ',      3, 6),
('2025-04-07',	207, 'Yêu cầu thêm gối',  4, 7),
('2025-04-08',	208, NULL,                5, 8),
('2025-04-09',	209, 'Khách VIP',         3, 9),
('2025-04-10',  103, NULL,                4, 10)


INSERT INTO KhuyenMai (TenKhuyenMai, KieuKhuyenMai, MoTaKhuyenMai, NgayBatDau, NgayKetThuc, GiaTriKhuyenMai, GhiChu)
VALUES
(N'Khuyến mãi mùa hè',		1,	N'Giảm giá cho đặt phòng mùa hè', '2025-05-01', '2025-08-31', 15,			N'Áp dụng cho tất cả phòng'),
(N'Khách hàng thân thiết',	2,	N'Giảm giá cho khách hàng thân thiết', '2025-01-01', '2025-12-31', 500000,	N'Áp dụng khi đặt từ 3 đêm trở lên'),
(N'Combo gia đình',			3,	N'Ưu đãi đặt phòng cho gia đình', '2025-04-01', '2025-06-30', 20,			N'Áp dụng khi đặt từ 2 phòng trở lên')


-- INSERT INTO ChiTietDatPhong (Phong, DatPhong, KhuyenMai, NgayTraPhong, NgayNhanPhong)
-- VALUES
-- (29, 11, NULL, '2025-04-03', '2025-04-01'),
-- (28, 2, 4, '2025-04-04', '2025-04-02'),
-- (27, 3, 2, '2025-04-06', '2025-04-03'),
-- (26, 4, NULL, '2025-04-06', '2025-04-04'),
-- (25, 5, NULL, '2025-04-06', '2025-04-05'),
-- (24, 6, 4, '2025-04-08', '2025-04-06'),
-- (23, 7, NULL, '2025-04-08', '2025-04-07'),
-- (22, 8, 3, '2025-04-10', '2025-04-08'),
-- (21, 9, NULL, '2025-04-11', '2025-04-09'),
-- (30, 10, 2, '2025-04-12', '2025-04-10')