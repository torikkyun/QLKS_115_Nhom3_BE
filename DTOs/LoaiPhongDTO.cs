using System.ComponentModel.DataAnnotations;

namespace QLKS_115_Nhom3_BE.DTOs
{
    public class LoaiPhongDTO
    {
        public int MaLoaiPhong { get; set; }
        public int SoGiuong { get; set; }
        public string? GhiChu { get; set; }
        public int GiaPhong { get; set; }
    }
    public class CreateLoaiPhongDTO
    {
        [Required(ErrorMessage = "Số giường là bắt buộc")]
        public int SoGiuong { get; set; }

        [StringLength(100, ErrorMessage = "Ghi chú không vượt quá 100 ký tự")]
        public string? GhiChu { get; set; }

        public int GiaPhong { get; set; }
    }

    public class UpdateLoaiPhongDTO
    {
        public int? SoGiuong { get; set; }

        [StringLength(100, ErrorMessage = "Ghi chú không vượt quá 100 ký tự")]
        public string? GhiChu { get; set; }

        public int GiaPhong { get; set; }
    }
}