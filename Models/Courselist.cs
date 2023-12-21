using System;
using System.Collections.Generic;

namespace Labb_3___Skol_Databas.Models;

public partial class Courselist
{
    public int? FkstudentId { get; set; }

    public int CourseListId { get; set; }

    public int? FkcourseId { get; set; }

    public int? GradeInfo { get; set; }

    public DateTime? GradeDate { get; set; }

    public virtual Course? Fkcourse { get; set; }

    public virtual Student? Fkstudent { get; set; }
}
