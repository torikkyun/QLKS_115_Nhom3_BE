using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class KieuKhuyenMaiEnum
{
    public byte Id { get; set; }

    public string TenKieu { get; set; } = null!;

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();
}
