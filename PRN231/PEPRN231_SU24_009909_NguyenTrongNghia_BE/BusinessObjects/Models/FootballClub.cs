using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class FootballClub
{
    public string FootballClubId { get; set; } = null!;

    public string ClubName { get; set; } = null!;

    public string ClubShortDescription { get; set; } = null!;

    public string SoccerPracticeField { get; set; } = null!;

    public string Mascos { get; set; } = null!;

    public virtual ICollection<FootballPlayer> FootballPlayers { get; set; } = new List<FootballPlayer>();
}
