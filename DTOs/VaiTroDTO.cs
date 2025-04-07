using System.ComponentModel.DataAnnotations;

public class VaiTroDTO
{
    public int MaVaiTro { get; set; }
    public string TenVaiTro { get; set; } = null!;
    public string? GhiChu { get; set; }
}

public class CreateVaiTroDTO
{
    [Required(ErrorMessage = "Tên vai trò là bắt buộc")]
    [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự")]
    public string TenVaiTro { get; set; } = null!;

    [StringLength(100, ErrorMessage = "Ghi chú không vượt quá 100 ký tự")]
    public string? GhiChu { get; set; }
}

public class UpdateVaiTroDTO
{
    [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự")]
    public string? TenVaiTro { get; set; }

    [StringLength(100, ErrorMessage = "Ghi chú không vượt quá 100 ký tự")]
    public string? GhiChu { get; set; }
}