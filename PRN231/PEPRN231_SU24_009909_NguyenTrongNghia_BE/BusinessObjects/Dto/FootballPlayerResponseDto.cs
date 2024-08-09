using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto
{
    public class FootballPlayerResponseDto
    {
        public string FootballPlayerId { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Achievements { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        public string PlayerExperiences { get; set; } = null!;

        public string Nomination { get; set; } = null!;

        public string? ClubName { get; set; }
    }
}
