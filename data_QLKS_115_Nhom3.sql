INSERT INTO LoaiDichVuEnum (Id, TenLoai) VALUES
(1, N'Dịch vụ phòng'),
(2, N'Dịch vụ ăn uống'),
(3, N'Dịch vụ giải trí'),
(4, N'Dịch vụ spa'),
(5, N'Dịch vụ giặt ủi'),
(6, N'Dịch vụ khác');

INSERT INTO KieuKhuyenMaiEnum (Id, TenKieu) VALUES
(1, N'Phần trăm'),
(2, N'Giảm giá trực tiếp');

INSERT INTO TinhTrangPhongEnum (Id, TenTinhTrang) VALUES
(0, N'Đang sử dụng'),
(1, N'Trống'),
(2, N'Bảo trì');

INSERT INTO TinhTrangThanhToanEnum (Id, TenTinhTrang) VALUES
(1, N'Chưa thanh toán'),
(2, N'Đã thanh toán'),
(3, N'Đang xử lý'),
(4, N'Hoàn tiền'),
(5, N'Hủy bỏ');

INSERT INTO VaiTro (TenVaiTro, GhiChu) VALUES
(N'Quản lý', NULL),
(N'Lễ tân', NULL),
(N'Phục vụ', NULL);

INSERT INTO LoaiPhong (SoGiuong, GhiChu) VALUES
(1, N'Phòng đơn'),
(2, N'Phòng đôi'),
(3, N'Phòng gia đình');

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

INSERT INTO KhuyenMai (TenKhuyenMai, KieuKhuyenMai, MoTaKhuyenMai, NgayBatDau, NgayKetThuc, GiaTriKhuyenMai, GhiChu)
VALUES
(N'Khuyến mãi mùa hè',		1,	N'Giảm giá cho đặt phòng mùa hè', '2025-05-01', '2025-08-31', 15,			N'Áp dụng cho tất cả phòng'),
(N'Khách hàng thân thiết',	2,	N'Giảm giá cho khách hàng thân thiết', '2025-01-01', '2025-12-31', 500000,	N'Áp dụng khi đặt từ 3 đêm trở lên');
