using System.Diagnostics.CodeAnalysis;

namespace EdhWreck.Biz.Models
{
    [ExcludeFromCodeCoverage]
    public class CardRequest
    {
        public string? Format { get; set; }
        public string? Legality { get; set; }
        public decimal? MaxCardCost { get; set; }
        public string? Colors { get; set; }
        public List<string>? IncludedOracleText { get; set; }
        public List<string>? IncludedTypes { get; set; }
        public List<string>? IncludedRarities { get; set; }
    }
}
