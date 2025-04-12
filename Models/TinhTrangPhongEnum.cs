using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class TinhTrangPhongEnum
{
    public byte Id { get; set; }

    public string TenTinhTrang { get; set; } = null!;

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
