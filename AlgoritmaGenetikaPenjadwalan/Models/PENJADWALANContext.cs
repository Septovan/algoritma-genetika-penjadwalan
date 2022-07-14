using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AlgoritmaGenetikaPenjadwalan.Models
{
    public partial class PENJADWALANContext : DbContext
    {
        public PENJADWALANContext()
        {
        }

        public PENJADWALANContext(DbContextOptions<PENJADWALANContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgpDosen> AgpDosen { get; set; }
        public virtual DbSet<AgpJadwal> AgpJadwal { get; set; }
        public virtual DbSet<AgpKelas> AgpKelas { get; set; }
        public virtual DbSet<AgpMatakuliah> AgpMatakuliah { get; set; }
        public virtual DbSet<AgpRuang> AgpRuang { get; set; }
        public virtual DbSet<AgpSettingsDosen> AgpSettingsDosen { get; set; }
        public virtual DbSet<AgpSettingsHari> AgpSettingsHari { get; set; }
        public virtual DbSet<AgpSettingsJadwalKelas> AgpSettingsJadwalKelas { get; set; }
        public virtual DbSet<AgpSettingsJam> AgpSettingsJam { get; set; }
        public virtual DbSet<VJadwalToBeCalculated> VJadwalToBeCalculated { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PENJADWALAN;Trusted_Connection=True;User Id=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgpDosen>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Kode)
                    .IsRequired()
                    .HasColumnName("kode")
                    .HasMaxLength(10);

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnName("nama")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<AgpJadwal>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdRuang).HasColumnName("idRuang");

                entity.Property(e => e.IdSettiingDosen).HasColumnName("idSettiingDosen");

                entity.Property(e => e.IdSettingHari).HasColumnName("idSettingHari");

                entity.Property(e => e.IdSettingJadwalKelas).HasColumnName("idSettingJadwalKelas");

                entity.Property(e => e.IdSettingJam).HasColumnName("idSettingJam");

                entity.HasOne(d => d.IdRuangNavigation)
                    .WithMany(p => p.AgpJadwal)
                    .HasForeignKey(d => d.IdRuang)
                    .HasConstraintName("FK_AgpJadwal_AgpRuang");

                entity.HasOne(d => d.IdSettiingDosenNavigation)
                    .WithMany(p => p.AgpJadwal)
                    .HasForeignKey(d => d.IdSettiingDosen)
                    .HasConstraintName("FK_AgpJadwal_AgpSettingsDosen");

                entity.HasOne(d => d.IdSettingHariNavigation)
                    .WithMany(p => p.AgpJadwal)
                    .HasForeignKey(d => d.IdSettingHari)
                    .HasConstraintName("FK_AgpJadwal_AgpSettingsHari");

                entity.HasOne(d => d.IdSettingJadwalKelasNavigation)
                    .WithMany(p => p.AgpJadwal)
                    .HasForeignKey(d => d.IdSettingJadwalKelas)
                    .HasConstraintName("FK_AgpJadwal_AgpSettingsJadwalKelas");

                entity.HasOne(d => d.IdSettingJamNavigation)
                    .WithMany(p => p.AgpJadwal)
                    .HasForeignKey(d => d.IdSettingJam)
                    .HasConstraintName("FK_AgpJadwal_AgpSettingsJam");
            });

            modelBuilder.Entity<AgpKelas>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdMatakuliah).HasColumnName("idMatakuliah");

                entity.Property(e => e.Kode)
                    .IsRequired()
                    .HasColumnName("kode")
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdMatakuliahNavigation)
                    .WithMany(p => p.AgpKelas)
                    .HasForeignKey(d => d.IdMatakuliah)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgpMatakuliah_AgpKelas");
            });

            modelBuilder.Entity<AgpMatakuliah>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Kapasitas).HasColumnName("kapasitas");

                entity.Property(e => e.Kode)
                    .IsRequired()
                    .HasColumnName("kode")
                    .HasMaxLength(10);

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasColumnName("nama")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AgpRuang>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Jenis)
                    .IsRequired()
                    .HasColumnName("jenis")
                    .HasMaxLength(25);

                entity.Property(e => e.Kapasitas).HasColumnName("kapasitas");

                entity.Property(e => e.Kode)
                    .IsRequired()
                    .HasColumnName("kode")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<AgpSettingsDosen>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdDosen).HasColumnName("idDosen");

                entity.Property(e => e.IdKelas).HasColumnName("idKelas");

                entity.HasOne(d => d.IdDosenNavigation)
                    .WithMany(p => p.AgpSettingsDosen)
                    .HasForeignKey(d => d.IdDosen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgpSettingsDosen_AgpDosen");

                entity.HasOne(d => d.IdKelasNavigation)
                    .WithMany(p => p.AgpSettingsDosen)
                    .HasForeignKey(d => d.IdKelas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgpSettingsDosen_AgpKelas");
            });

            modelBuilder.Entity<AgpSettingsHari>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Hari)
                    .IsRequired()
                    .HasColumnName("hari")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<AgpSettingsJadwalKelas>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Durasi).HasColumnName("durasi");

                entity.Property(e => e.IdKelas).HasColumnName("idKelas");

                entity.Property(e => e.Iterasi).HasColumnName("iterasi");

                entity.Property(e => e.Tipe)
                    .IsRequired()
                    .HasColumnName("tipe")
                    .HasMaxLength(25);

                entity.HasOne(d => d.IdKelasNavigation)
                    .WithMany(p => p.AgpSettingsJadwalKelas)
                    .HasForeignKey(d => d.IdKelas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgpSettingsJadwalKelas_AgpKelas");
            });

            modelBuilder.Entity<AgpSettingsJam>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnName("endTime");

                entity.Property(e => e.StartTime).HasColumnName("startTime");

                entity.Property(e => e.Tipe).HasColumnName("tipe");
            });

            modelBuilder.Entity<VJadwalToBeCalculated>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_JadwalToBeCalculated");

                entity.Property(e => e.DurasiKelas).HasColumnName("DURASI KELAS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDosen).HasColumnName("ID DOSEN");

                entity.Property(e => e.IdKelas).HasColumnName("ID KELAS");

                entity.Property(e => e.IterasiKelas).HasColumnName("ITERASI KELAS");

                entity.Property(e => e.KapasitasKelas).HasColumnName("KAPASITAS KELAS");

                entity.Property(e => e.TipeKelas)
                    .IsRequired()
                    .HasColumnName("TIPE KELAS")
                    .HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
