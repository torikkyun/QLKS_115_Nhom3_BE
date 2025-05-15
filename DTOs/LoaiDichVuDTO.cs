using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLKS_115_Nhom3_BE.DTOs
{
    public class LoaiDichVuDTO
    {
        public byte Id { get; set; }
        public string TenLoai { get; set; } = null!;
        public int GiaDichVu { get; set; }
    }
}
