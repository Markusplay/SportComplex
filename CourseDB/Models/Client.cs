using System;
using System.Collections.Generic;

namespace CourseDB.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FullName { get; set; } = null!;

    public int PhoneNumber { get; set; }

    public bool Sex { get; set; }

    public string Adress { get; set; } = null!;

    public virtual ICollection<ClubCard> ClubCards { get; } = new List<ClubCard>();

    public virtual ICollection<Visit> Visits { get; } = new List<Visit>();
}
