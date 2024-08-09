using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Style
{
    public string StyleId { get; set; } = null!;

    public string StyleName { get; set; } = null!;

    public string StyleDescription { get; set; } = null!;

    public string? OriginalCountry { get; set; }
    [JsonIgnore]

    public virtual ICollection<WatercolorsPainting> WatercolorsPaintings { get; set; } = new List<WatercolorsPainting>();
}
