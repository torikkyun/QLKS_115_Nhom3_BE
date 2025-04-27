using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class TinhTrangThanhToanEnum
{
    public byte Id { get; set; }

    public string TenTinhTrang { get; set; } = null!;

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
