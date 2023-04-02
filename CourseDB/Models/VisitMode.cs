using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class VisitMode
{
    public int VisitModeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClubCard> ClubCards { get; } = new List<ClubCard>();
}
