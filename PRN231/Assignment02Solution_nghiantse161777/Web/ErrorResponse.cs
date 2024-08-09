using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Web
{
    public class ErrorResponse
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("errors")]
        public Dictionary<string?, List<string?>?>? Errors { get; set; }
    }
}
