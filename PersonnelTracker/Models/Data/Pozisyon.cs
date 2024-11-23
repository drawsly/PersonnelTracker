using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class Pozisyon
{
    public int PozisyonId { get; set; }

    public string Ad { get; set; } = null!;

    public string? Aciklama { get; set; }

    public int? DepartmanId { get; set; }

    public virtual Departman? Departman { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
}
