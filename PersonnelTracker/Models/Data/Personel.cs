using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class Personel
{
    public int PersonelId { get; set; }

    public string? Tckn { get; set; } = null!;

    public string? Ad { get; set; } = null!;

    public string? Soyad { get; set; } = null!;

    public DateOnly DogumTarihi { get; set; }

    public string? Cinsiyet { get; set; } = null!;

    public int DepartmanId { get; set; }

    public int PozisyonId { get; set; }

    public string? Telefon { get; set; }

    public string? Eposta { get; set; }

    public string? Iban { get; set; }

    public DateOnly IseGirisTarihi { get; set; }

    public decimal SabitMaas { get; set; }

    public virtual Departman Departman { get; set; } = null!;

    public virtual ICollection<Izin> Izins { get; set; } = new List<Izin>();

    public virtual ICollection<MaasBordro> MaasBordros { get; set; } = new List<MaasBordro>();

    public virtual Pozisyon Pozisyon { get; set; } = null!;

    public virtual ICollection<Rapor> Rapors { get; set; } = new List<Rapor>();

    public string AdSoyad => $"{Ad} {Soyad}";
    public string CinsiyetStr => Cinsiyet == "E" ? "Erkek" : Cinsiyet == "K" ? "Kadın" : "Bilinmiyor";

}


