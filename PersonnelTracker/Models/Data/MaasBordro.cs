using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class MaasBordro
{
    public int BordroId { get; set; }

    public int PersonelId { get; set; }

    public string Donem { get; set; } = null!;

    public decimal BrutMaas { get; set; }

    public decimal NetMaas { get; set; }

    public decimal EkOdemeler { get; set; }

    public decimal Kesintiler { get; set; }

    public string? Aciklama { get; set; }

    public virtual Personel Personel { get; set; } = null!;
}
