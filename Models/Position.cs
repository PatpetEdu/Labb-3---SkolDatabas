using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Position
{
    public int PositionId { get; set; }

    public string? PositionName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
