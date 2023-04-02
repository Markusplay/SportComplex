using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class ClubCard
{
    public int ClubCardId { get; set; }

    public int? ClientId { get; set; }

    public int VisitModeId { get; set; }

    public int Discount { get; set; }

    public virtual Client? Client { get; set; }

    public virtual VisitMode VisitMode { get; set; } = null!;
}
