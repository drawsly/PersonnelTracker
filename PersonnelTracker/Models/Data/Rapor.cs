using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class Rapor
{
    public int RaporId { get; set; }

    public int PersonelId { get; set; }

    public string RaporTuru { get; set; } = null!;

    public DateOnly RaporTarihi { get; set; }

    public string? Aciklama { get; set; }

    public virtual Personel Personel { get; set; } = null!;
}
