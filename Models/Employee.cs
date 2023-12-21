using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Salary { get; set; }

    public int? FkpersonId { get; set; }

    public DateTime? EmploymentDate { get; set; }

    public int? FkpositionId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Personalinfo? Fkperson { get; set; }

    public virtual Position? Fkposition { get; set; }
}
