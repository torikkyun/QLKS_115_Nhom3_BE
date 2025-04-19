using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class ChiTietDichVu
{
    public int Phong { get; set; }

    public int DatPhong { get; set; }

    public int DichVu { get; set; }

    public DateOnly NgaySuDung { get; set; }

    public virtual ChiTietDatPhong ChiTietDatPhong { get; set; } = null!;

    public virtual DichVu DichVuNavigation { get; set; } = null!;
}
