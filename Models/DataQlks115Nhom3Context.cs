using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLKS_115_Nhom3_BE.Models;

public partial class DataQlks115Nhom3Context : DbContext
{
    public DataQlks115Nhom3Context()
    {
    }

    public DataQlks115Nhom3Context(DbContextOptions<DataQlks115Nhom3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDatPhong> ChiTietDatPhongs { get; set; }

    public virtual DbSet<ChiTietDichVu> ChiTietDichVus { get; set; }

    public virtual DbSet<DatPhong> DatPhongs { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Duc");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDatPhong>(entity =>
        {
            entity.HasKey(e => new { e.Phong, e.DatPhong }).HasName("pk_ChiTietDatPhong");

            entity.ToTable("ChiTietDatPhong");

            entity.HasOne(d => d.DatPhongNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.DatPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChiTietDatPhong_DatPhong");

            entity.HasOne(d => d.KhuyenMaiNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.KhuyenMai)
                .HasConstraintName("fk_ChiTietDatPhong_KhuyenMai");

            entity.HasOne(d => d.PhongNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.Phong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChiTietDatPhong_Phong");
        });

        modelBuilder.Entity<ChiTietDichVu>(entity =>
        {
            entity.HasKey(e => new { e.Phong, e.DatPhong, e.DichVu }).HasName("pk_ChiTietDichVu");

            entity.ToTable("ChiTietDichVu");

            entity.Property(e => e.NgaySuDung).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DichVuNavigation).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => d.DichVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChiTietDichVu_DichVu");

            entity.HasOne(d => d.ChiTietDatPhong).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => new { d.Phong, d.DatPhong })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ChiTietDichVu_ChiTietDatPhong");
        });

        modelBuilder.Entity<DatPhong>(entity =>
        {
            entity.HasKey(e => e.MaDatPhong).HasName("PK__DatPhong__6344ADEABDFF16EC");

            entity.ToTable("DatPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NgayDatPhong).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.KhachHangNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.KhachHang)
                .HasConstraintName("fk_DatPhong_KhachHang");

            entity.HasOne(d => d.NhanVienNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.NhanVien)
                .HasConstraintName("fk_DatPhong_NhanVien");
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.MaDichVu).HasName("PK__DichVu__C0E6DE8F9DCAEE8C");

            entity.ToTable("DichVu");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.LoaiDichVu).HasMaxLength(50);
            entity.Property(e => e.TenDichVu).HasMaxLength(50);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.DatPhong).HasName("PK__HoaDon__2192D07158EF572B");

            entity.ToTable("HoaDon");

            entity.Property(e => e.DatPhong).ValueGeneratedNever();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NgayXuatHoaDon).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DatPhongNavigation).WithOne(p => p.HoaDon)
                .HasForeignKey<HoaDon>(d => d.DatPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HoaDon_DatPhong");

            entity.HasOne(d => d.NhanVienNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.NhanVien)
                .HasConstraintName("fk_HoaDon_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E519AAEE67");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Cccd, "UQ__KhachHan__A955A0AAD0D20FA4").IsUnique();

            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(20);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__6F56B3BD60BDBACB");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.KieuKhuyenMai).HasMaxLength(50);
            entity.Property(e => e.MoTaKhuyenMai).HasMaxLength(100);
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLoaiPhong).HasName("PK__LoaiPhon__230212175F04B557");

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA4709196A77");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AA5316D183").IsUnique();

            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(20);

            entity.HasOne(d => d.VaiTroNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.VaiTro)
                .HasConstraintName("fk_NhanVien_VaiTro");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5BE3EC29D7");

            entity.ToTable("Phong");

            entity.Property(e => e.SoPhong)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.LoaiPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.LoaiPhong)
                .HasConstraintName("fk_Phong_LoaiPhong");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CFC5E6834D");

            entity.ToTable("VaiTro");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
