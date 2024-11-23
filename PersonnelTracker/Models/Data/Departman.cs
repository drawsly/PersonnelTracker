using System;
using System.Collections.Generic;

namespace PersonnelTracker.Models.Data;

public partial class Departman
{
    public int DepartmanId { get; set; }

    public string Ad { get; set; } = null!;

    public string? Aciklama { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();

    public virtual ICollection<Pozisyon> Pozisyons { get; set; } = new List<Pozisyon>();
}
