namespace QLKS_115_Nhom3_BE.DTOs
{
    public class UpdatePhongDTO
    {
        public string SoPhong { get; set; } = null!;

        public int? MaLoaiPhong { get; set; }
    }
    public class CreatePhongDTO : UpdatePhongDTO
    {
        public byte TinhTrangPhong { get; set; }
    }
    // public class PhongDTO : CreatePhongDTO
    // {
    //     public int MaPhong { get; set; }

    //     public int SoGiuong { get; set; }

    //     public string? GhiChu { get; set; }
    // }
    public class PhongDTO
    {
        public int MaPhong { get; set; }

        public string SoPhong { get; set; } = null!;

        public string? TenTinhTrang { get; set; }

        public int SoGiuong { get; set; }

        public string? GhiChu { get; set; }

        public int GiaPhong { get; set; }
    }
    public class LocPhongDtoResponse : PhongDTO
    {
        public int GiaPhong { get; set; }
    }

    public class LocPhongRequest
    {
       // public byte TinhTrangPhong { get; set; }
        public int? GiaPhongMin { get; set; }
        public int? GiaPhongMax { get; set; }
    }


}
