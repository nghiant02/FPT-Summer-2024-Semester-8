namespace BusinessObject.Dto
{
    public class WatercolorsPaintingResponseDto
    {
        public string PaintingId { get; set; } = null!;

        public string PaintingName { get; set; } = null!;

        public string? PaintingDescription { get; set; }

        public string? PaintingAuthor { get; set; }

        public decimal? Price { get; set; }

        public int? PublishYear { get; set; }
        public string? StyleName { get; set; }
    }
}
