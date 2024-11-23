using System.Configuration;
using PersonnelTracker.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace PersonnelTracker.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departman>? Departmen { get; set; }

    public virtual DbSet<Izin>? Izins { get; set; }

    public virtual DbSet<MaasBordro>? MaasBordros { get; set; }

    public virtual DbSet<Personel>? Personels { get; set; }

    public virtual DbSet<Pozisyon>? Pozisyons { get; set; }

    public virtual DbSet<Rapor>? Rapors { get; set; }

    readonly string _connString = ConfigurationManager.ConnectionStrings["PersonelDatabase"].ConnectionString;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(_connString, ServerVersion.AutoDetect(_connString));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Departman>(entity =>
        {
            entity.HasKey(e => e.DepartmanId).HasName("PRIMARY");

            entity.ToTable("departman");

            entity.Property(e => e.DepartmanId)
                .HasColumnType("int(11)")
                .HasColumnName("departman_id");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("aciklama");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .HasColumnName("ad");
        });

        modelBuilder.Entity<Izin>(entity =>
        {
            entity.HasKey(e => e.IzinId).HasName("PRIMARY");

            entity.ToTable("izin");

            entity.HasIndex(e => e.PersonelId, "FK_izin_personel_personel_id");

            entity.Property(e => e.IzinId)
                .HasColumnType("int(11)")
                .HasColumnName("izin_id");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("aciklama");
            entity.Property(e => e.BaslangicTarihi).HasColumnName("baslangic_tarihi");
            entity.Property(e => e.BitisTarihi).HasColumnName("bitis_tarihi");
            entity.Property(e => e.IzinTuru)
                .HasColumnType("enum('Yıllık','Hastalık','Diğer')")
                .HasColumnName("izin_turu");
            entity.Property(e => e.PersonelId)
                .HasColumnType("int(11)")
                .HasColumnName("personel_id");

            entity.HasOne(d => d.Personel).WithMany(p => p.Izins)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MaasBordro>(entity =>
        {
            entity.HasKey(e => e.BordroId).HasName("PRIMARY");

            entity.ToTable("maas_bordro");

            entity.HasIndex(e => e.PersonelId, "FK_maas_bordro_personel_personel_id");

            entity.Property(e => e.BordroId)
                .HasColumnType("int(11)")
                .HasColumnName("bordro_id");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("aciklama");
            entity.Property(e => e.BrutMaas)
                .HasPrecision(10, 2)
                .HasColumnName("brut_maas");
            entity.Property(e => e.Donem)
                .HasMaxLength(7)
                .HasColumnName("donem");
            entity.Property(e => e.EkOdemeler)
                .HasPrecision(10, 2)
                .HasColumnName("ek_odemeler");
            entity.Property(e => e.Kesintiler)
                .HasPrecision(10, 2)
                .HasColumnName("kesintiler");
            entity.Property(e => e.NetMaas)
                .HasPrecision(10, 2)
                .HasColumnName("net_maas");
            entity.Property(e => e.PersonelId)
                .HasColumnType("int(11)")
                .HasColumnName("personel_id");

            entity.HasOne(d => d.Personel).WithMany(p => p.MaasBordros)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Personel>(entity =>
        {
            entity.HasKey(e => e.PersonelId).HasName("PRIMARY");

            entity.ToTable("personel");

            entity.HasIndex(e => e.DepartmanId, "FK_personel_departman_departman_id");

            entity.HasIndex(e => e.PozisyonId, "FK_personel_pozisyon_pozisyon_id");

            entity.Property(e => e.PersonelId)
                .HasColumnType("int(11)")
                .HasColumnName("personel_id");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .HasColumnName("ad");
            entity.Property(e => e.Cinsiyet)
                .HasColumnType("enum('E','K')")
                .HasColumnName("cinsiyet");
            entity.Property(e => e.DepartmanId)
                .HasColumnType("int(11)")
                .HasColumnName("departman_id");
            entity.Property(e => e.DogumTarihi).HasColumnName("dogum_tarihi");
            entity.Property(e => e.Iban)
                .HasMaxLength(30)
                .HasColumnName("iban");
            entity.Property(e => e.Telefon)
                .HasMaxLength(15)
                .HasColumnName("telefon");
            entity.Property(e => e.Eposta)
                .HasMaxLength(100)
                .HasColumnName("eposta");
            entity.Property(e => e.IseGirisTarihi).HasColumnName("ise_giris_tarihi");
            entity.Property(e => e.PozisyonId)
                .HasColumnType("int(11)")
                .HasColumnName("pozisyon_id");
            entity.Property(e => e.SabitMaas)
                .HasPrecision(10, 2)
                .HasColumnName("sabit_maas");
            entity.Property(e => e.Soyad)
                .HasMaxLength(50)
                .HasColumnName("soyad");
            entity.Property(e => e.Tckn)
                .HasMaxLength(11)
                .HasColumnName("tckn");

            entity.HasOne(d => d.Departman).WithMany(p => p.Personels)
                .HasForeignKey(d => d.DepartmanId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Pozisyon).WithMany(p => p.Personels)
                .HasForeignKey(d => d.PozisyonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Pozisyon>(entity =>
        {
            entity.HasKey(e => e.PozisyonId).HasName("PRIMARY");

            entity.ToTable("pozisyon");

            entity.HasIndex(e => e.DepartmanId, "FK_pozisyon_departman_departman_id");

            entity.Property(e => e.PozisyonId)
                .HasColumnType("int(11)")
                .HasColumnName("pozisyon_id");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("aciklama");
            entity.Property(e => e.Ad)
                .HasMaxLength(50)
                .HasColumnName("ad");
            entity.Property(e => e.DepartmanId)
                .HasColumnType("int(11)")
                .HasColumnName("departman_id");

            entity.HasOne(d => d.Departman).WithMany(p => p.Pozisyons)
                .HasForeignKey(d => d.DepartmanId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Rapor>(entity =>
        {
            entity.HasKey(e => e.RaporId).HasName("PRIMARY");

            entity.ToTable("rapor");

            entity.HasIndex(e => e.PersonelId, "FK_rapor_personel_personel_id");

            entity.Property(e => e.RaporId)
                .HasColumnType("int(11)")
                .HasColumnName("rapor_id");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("aciklama");
            entity.Property(e => e.PersonelId)
                .HasColumnType("int(11)")
                .HasColumnName("personel_id");
            entity.Property(e => e.RaporTarihi).HasColumnName("rapor_tarihi");
            entity.Property(e => e.RaporTuru)
                .HasColumnType("enum('Devamsızlık','Performans','Diğer')")
                .HasColumnName("rapor_turu");

            entity.HasOne(d => d.Personel).WithMany(p => p.Rapors)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
