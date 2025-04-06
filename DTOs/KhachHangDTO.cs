using System.ComponentModel.DataAnnotations;

public class KhachHangDTO
{
    public int MaKhachHang { get; set; }
    public string Ho { get; set; } = null!;
    public string Ten { get; set; } = null!;
    public string? Email { get; set; }
    public string Sdt { get; set; } = null!;
    public string? Cccd { get; set; }
}

public class CreateKhachHangDTO
{
    [Required(ErrorMessage = "Họ là bắt buộc")]
    [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự")]
    public string Ho { get; set; } = null!;

    [Required(ErrorMessage = "Tên là bắt buộc")]
    [StringLength(20, ErrorMessage = "Tên không vượt quá 20 ký tự")]
    public string Ten { get; set; } = null!;

    [StringLength(50, ErrorMessage = "Email không vượt quá 50 ký tự")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [StringLength(20, ErrorMessage = "Số điện thoại không vượt quá 20 ký tự")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
    public string Sdt { get; set; } = null!;

    [Required(ErrorMessage = "CCCD là bắt buộc")]
    [StringLength(20, ErrorMessage = "CCCD không vượt quá 20 ký tự")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "CCCD chỉ chứa số")]
    public string? Cccd { get; set; }
}

public class UpdateKhachHangDTO
{
    [StringLength(50, ErrorMessage = "Họ không vượt quá 50 ký tự")]
    public string? Ho { get; set; }

    [StringLength(20, ErrorMessage = "Tên không vượt quá 20 ký tự")]
    public string? Ten { get; set; }

    [StringLength(50, ErrorMessage = "Email không vượt quá 50 ký tự")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }

    [StringLength(20, ErrorMessage = "Số điện thoại không vượt quá 20 ký tự")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
    public string? Sdt { get; set; }

    [StringLength(20, ErrorMessage = "CCCD không vượt quá 20 ký tự")]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "CCCD chỉ chứa số")]
    public string? Cccd { get; set; }
}