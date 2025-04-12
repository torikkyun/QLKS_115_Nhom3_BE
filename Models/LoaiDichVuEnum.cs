using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class LoaiDichVuEnum
{
    public byte Id { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<DichVu> DichVus { get; set; } = new List<DichVu>();
}
