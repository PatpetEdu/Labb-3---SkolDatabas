using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int? FkpersonId { get; set; }

    public int? FkcourseId { get; set; }

    public int? FkclassId { get; set; }

    public virtual ICollection<Courselist> Courselists { get; set; } = new List<Courselist>();

    public virtual Class? Fkclass { get; set; }

    public virtual Course? Fkcourse { get; set; }

    public virtual Personalinfo? Fkperson { get; set; }
}
