using System.Diagnostics.CodeAnalysis;

namespace EdhWreck.Biz.Models
{
    [ExcludeFromCodeCoverage]
    public class CardRequest
    {
        public decimal? MaxCardCost { get; set; }
        public string? Colors { get; set; }
        public List<string>? IncludedOracleText { get; set; }
        public List<string>? IncludedTypes { get; set; }
        public List<string>? IncludedRarities { get; set; }
    }
}
