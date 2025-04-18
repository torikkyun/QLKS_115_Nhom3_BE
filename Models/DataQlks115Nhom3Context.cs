﻿using System;
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

    public virtual DbSet<KieuKhuyenMaiEnum> KieuKhuyenMaiEnums { get; set; }

    public virtual DbSet<LoaiDichVuEnum> LoaiDichVuEnums { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    public virtual DbSet<TinhTrangPhongEnum> TinhTrangPhongEnums { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=data_QLKS_115_Nhom3;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDatPhong>(entity =>
        {
            entity.HasKey(e => new { e.Phong, e.DatPhong });

            entity.ToTable("ChiTietDatPhong");

            entity.HasOne(d => d.DatPhongNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.DatPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDatPhong_DatPhong");

            entity.HasOne(d => d.KhuyenMaiNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.KhuyenMai)
                .HasConstraintName("FK_ChiTietDatPhong_KhuyenMai");

            entity.HasOne(d => d.PhongNavigation).WithMany(p => p.ChiTietDatPhongs)
                .HasForeignKey(d => d.Phong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDatPhong_Phong");
        });

        modelBuilder.Entity<ChiTietDichVu>(entity =>
        {
            entity.HasKey(e => new { e.Phong, e.DatPhong, e.DichVu });

            entity.ToTable("ChiTietDichVu");

            entity.Property(e => e.NgaySuDung).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DichVuNavigation).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => d.DichVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDichVu_DichVu");

            entity.HasOne(d => d.ChiTietDatPhong).WithMany(p => p.ChiTietDichVus)
                .HasForeignKey(d => new { d.Phong, d.DatPhong })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDichVu_ChiTietDatPhong");
        });

        modelBuilder.Entity<DatPhong>(entity =>
        {
            entity.HasKey(e => e.MaDatPhong).HasName("PK__DatPhong__6344ADEA96C7205A");

            entity.ToTable("DatPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NgayDatPhong).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.KhachHangNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.KhachHang)
                .HasConstraintName("FK_DatPhong_KhachHang");

            entity.HasOne(d => d.NhanVienNavigation).WithMany(p => p.DatPhongs)
                .HasForeignKey(d => d.NhanVien)
                .HasConstraintName("FK_DatPhong_NhanVien");
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.MaDichVu).HasName("PK__DichVu__C0E6DE8F983DA9A2");

            entity.ToTable("DichVu");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.TenDichVu).HasMaxLength(50);

            entity.HasOne(d => d.LoaiDichVuNavigation).WithMany(p => p.DichVus)
                .HasForeignKey(d => d.LoaiDichVu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DichVu_LoaiDichVu");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.DatPhong).HasName("PK__HoaDon__2192D071FD61AA11");

            entity.ToTable("HoaDon");

            entity.Property(e => e.DatPhong).ValueGeneratedNever();
            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.NgayXuatHoaDon).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DatPhongNavigation).WithOne(p => p.HoaDon)
                .HasForeignKey<HoaDon>(d => d.DatPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_DatPhong");

            entity.HasOne(d => d.NhanVienNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.NhanVien)
                .HasConstraintName("FK_HoaDon_NhanVien");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E5D9D90CF1");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Cccd, "UQ__KhachHan__A955A0AADAB78EF2").IsUnique();

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
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__6F56B3BDE607568A");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.MoTaKhuyenMai).HasMaxLength(100);
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(50);

            entity.HasOne(d => d.KieuKhuyenMaiNavigation).WithMany(p => p.KhuyenMais)
                .HasForeignKey(d => d.KieuKhuyenMai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KhuyenMai_KieuKhuyenMai");
        });

        modelBuilder.Entity<KieuKhuyenMaiEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KieuKhuy__3214EC07A49492E7");

            entity.ToTable("KieuKhuyenMaiEnum");

            entity.Property(e => e.TenKieu).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiDichVuEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoaiDich__3214EC07E7EA1D82");

            entity.ToTable("LoaiDichVuEnum");

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLoaiPhong).HasName("PK__LoaiPhon__23021217BC753241");

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA4764D6630D");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AAC92FBBE8").IsUnique();

            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(20);

            entity.HasOne(d => d.VaiTroNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.VaiTro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanVien_VaiTro");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5BDB1162E7");

            entity.ToTable("Phong");

            entity.Property(e => e.SoPhong)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.LoaiPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.LoaiPhong)
                .HasConstraintName("FK_Phong_LoaiPhong");

            entity.HasOne(d => d.TinhTrangPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.TinhTrangPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phong_TinhTrangPhongEnum");
        });

        modelBuilder.Entity<TinhTrangPhongEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TinhTran__3214EC07E39F4734");

            entity.ToTable("TinhTrangPhongEnum");

            entity.Property(e => e.TenTinhTrang).HasMaxLength(50);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CFFDFEB611");

            entity.ToTable("VaiTro");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
