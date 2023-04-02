using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class Visit
{
    public int VisitId { get; set; }

    public int ServiceId { get; set; }

    public int ClientId { get; set; }

    public DateTime Date { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
