using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
