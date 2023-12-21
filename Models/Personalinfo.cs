using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Personalinfo
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string SocialSecurityNumber { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Address { get; set; }

    public string PhoneNumer { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
