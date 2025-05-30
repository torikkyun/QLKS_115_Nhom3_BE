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

    public virtual DbSet<TinhTrangThanhToanEnum> TinhTrangThanhToanEnums { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDatPhong>(entity =>
        {
            entity.HasKey(e => new { e.Phong, e.DatPhong });

            entity.ToTable("ChiTietDatPhong", tb =>
                {
                    tb.HasTrigger("trg_ChiTietDatPhong_AfterInsert");
                    tb.HasTrigger("trg_ChiTietDatPhong_BeforeInsert");
                });

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
            entity.HasKey(e => e.MaDatPhong).HasName("PK__DatPhong__6344ADEAF41A1D46");

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
            entity.HasKey(e => e.MaDichVu).HasName("PK__DichVu__C0E6DE8F59680969");

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
            entity.HasKey(e => e.DatPhong).HasName("PK__HoaDon__2192D07110F83630");

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

            entity.HasOne(d => d.TinhTrangThanhToanNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.TinhTrangThanhToan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_TinhTrangThanhToan");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E567826912");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Cccd, "UQ__KhachHan__A955A0AA971B67C9").IsUnique();

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
            entity.HasKey(e => e.MaKhuyenMai).HasName("PK__KhuyenMa__6F56B3BD4327F502");

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
            entity.HasKey(e => e.Id).HasName("PK__KieuKhuy__3214EC077B07D2BA");

            entity.ToTable("KieuKhuyenMaiEnum");

            entity.Property(e => e.TenKieu).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiDichVuEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoaiDich__3214EC078B2CF4A6");

            entity.ToTable("LoaiDichVuEnum");

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLoaiPhong).HasName("PK__LoaiPhon__23021217FD7C3899");

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA478B954729");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AA6D3854E8").IsUnique();

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanVien_VaiTro");
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5B78B55026");

            entity.ToTable("Phong");

            entity.HasIndex(e => e.SoPhong, "UQ_Phong_SoPhong").IsUnique();

            entity.Property(e => e.LoaiPhong).HasDefaultValue(1);
            entity.Property(e => e.SoPhong)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.TinhTrangPhong).HasDefaultValue((byte)1);

            entity.HasOne(d => d.LoaiPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.LoaiPhong)
                .HasConstraintName("FK_Phong_LoaiPhong");

            entity.HasOne(d => d.TinhTrangPhongNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.TinhTrangPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phong_TinhTrangPhong");
        });

        modelBuilder.Entity<TinhTrangPhongEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TinhTran__3214EC076E98A0E3");

            entity.ToTable("TinhTrangPhongEnum");

            entity.Property(e => e.TenTinhTrang).HasMaxLength(50);
        });

        modelBuilder.Entity<TinhTrangThanhToanEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TinhTran__3214EC07F4EC5EED");

            entity.ToTable("TinhTrangThanhToanEnum");

            entity.Property(e => e.TenTinhTrang).HasMaxLength(50);
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__VaiTro__C24C41CF9AB51DCC");

            entity.ToTable("VaiTro");

            entity.Property(e => e.GhiChu).HasMaxLength(100);
            entity.Property(e => e.TenVaiTro).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
