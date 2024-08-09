using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Style
{
    public string StyleId { get; set; } = null!;

    public string StyleName { get; set; } = null!;

    public string StyleDescription { get; set; } = null!;

    public string? OriginalCountry { get; set; }

    public virtual ICollection<WatercolorsPainting> WatercolorsPaintings { get; set; } = new List<WatercolorsPainting>();
}
