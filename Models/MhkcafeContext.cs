using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MHKCafe.Models;

public partial class MhkcafeContext : DbContext
{
    public MhkcafeContext()
    {
    }

    public MhkcafeContext(DbContextOptions<MhkcafeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<ThucDon> ThucDons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IGIO92E,1433;Database=MHKCafe;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__719FE4E8F44E57F1");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534494CA6B4").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quyen)
                .HasMaxLength(50)
                .HasDefaultValue("Manager");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietH__B117E9EA0CA63CF9");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.ChiTietId).HasColumnName("ChiTietID");
            entity.Property(e => e.DonGia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HoaDonId).HasColumnName("HoaDonID");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(21, 2)");
            entity.Property(e => e.ThucDonId).HasColumnName("ThucDonID");

            entity.HasOne(d => d.HoaDon).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.HoaDonId)
                .HasConstraintName("FK__ChiTietHo__HoaDo__47DBAE45");

            entity.HasOne(d => d.ThucDon).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.ThucDonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__ThucD__48CFD27E");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.HoaDonId).HasName("PK__HoaDon__6956CE6975696C3B");

            entity.ToTable("HoaDon");

            entity.Property(e => e.HoaDonId).HasColumnName("HoaDonID");
            entity.Property(e => e.DiaChiGiaoHang).HasMaxLength(255);
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.PhuongThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
            entity.Property(e => e.TongTien).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xác nhận");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__HoaDon__NguoiDun__44FF419A");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.NguoiDungId).HasName("PK__NguoiDun__C4BBA4DDDB65D484");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D10534061B2088").IsUnique();

            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
        });

        modelBuilder.Entity<ThucDon>(entity =>
        {
            entity.HasKey(e => e.ThucDonId).HasName("PK__ThucDon__447EBEDB8BC65BA4");

            entity.ToTable("ThucDon");

            entity.Property(e => e.ThucDonId).HasColumnName("ThucDonID");
            entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenMon).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
