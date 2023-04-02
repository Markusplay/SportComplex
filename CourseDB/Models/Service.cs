using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public double Price { get; set; }

    public virtual ICollection<Visit> Visits { get; } = new List<Visit>();
}
