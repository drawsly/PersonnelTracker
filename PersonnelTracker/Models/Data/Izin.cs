using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class Izin
{
    public int IzinId { get; set; }

    public int PersonelId { get; set; }

    public string IzinTuru { get; set; } = null!;

    public DateOnly BaslangicTarihi { get; set; }

    public DateOnly BitisTarihi { get; set; }

    public string? Aciklama { get; set; }

    public virtual Personel Personel { get; set; } = null!;
}
