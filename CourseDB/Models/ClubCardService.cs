using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class ClubCardService
{
    public int ServiceId { get; set; }

    public int ClubCardId { get; set; }

    public bool HasDovidka { get; set; }

    public virtual ClubCard ClubCard { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
