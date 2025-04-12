using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class DichVu
{
    public int MaDichVu { get; set; }

    public string TenDichVu { get; set; } = null!;

    public byte LoaiDichVu { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    public virtual LoaiDichVuEnum LoaiDichVuNavigation { get; set; } = null!;
}
