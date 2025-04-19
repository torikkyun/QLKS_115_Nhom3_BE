using System;
using System.Collections.Generic;

namespace QLKS_115_Nhom3_BE.Models;

public partial class ChiTietDatPhong
{
    public int Phong { get; set; }

    public int DatPhong { get; set; }

    public int? KhuyenMai { get; set; }

    public DateOnly NgayTraPhong { get; set; }

    public DateOnly NgayNhanPhong { get; set; }

    public virtual ICollection<ChiTietDichVu> ChiTietDichVus { get; set; } = new List<ChiTietDichVu>();

    public virtual DatPhong DatPhongNavigation { get; set; } = null!;

    public virtual KhuyenMai? KhuyenMaiNavigation { get; set; }

    public virtual Phong PhongNavigation { get; set; } = null!;
}
