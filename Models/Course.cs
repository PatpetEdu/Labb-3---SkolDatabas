using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int? FkemployeeId { get; set; }

    public virtual ICollection<Courselist> Courselists { get; set; } = new List<Courselist>();

    public virtual Employee? Fkemployee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
