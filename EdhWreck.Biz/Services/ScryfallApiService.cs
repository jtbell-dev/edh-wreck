using EdhWreck.Biz.Abstractions;
using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;
using EdhWreck.Biz.Models;

namespace EdhWreck.Biz.Services
{
    public class ScryfallApiService : IScryfallApiService
    {
        private const string ScryfallApiBaseUrl = "https://api.scryfall.com";
        private const string ScryfallCardEndpoint = "/cards/search";

        private readonly IHttpClientFactory _httpClientFactory;
        public ScryfallApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CardResponse> CardSearchAsync(CardRequest request)
        {
            HttpClient client = GetClient();

            ExpressionBase exp = new GameExpression("paper");

            if (request.MaxCardCost != null)
            {
                exp = exp.And(new UsdPriceExpression(ValueOperator.LessThanOrEqual, request.MaxCardCost.Value));
            }

            if (request.Colors != null && request.Colors.Length != 0)
            {
                exp = exp.And(new ColorIdentityExpression(request.Colors, ValueOperator.LessThanOrEqual));
            }

            if (request.IncludedOracleText != null && request.IncludedOracleText.Count != 0)
            {
                var oracleTextExpressions = request.IncludedOracleText
                    .Select(text => new OracleTextExpression(text))
                    .ToList();
                exp = exp.And(oracleTextExpressions.OrAll());
            }

            if (request.IncludedTypes != null && request.IncludedTypes.Count != 0)
            {
                var typeExpressions = request.IncludedTypes
                    .Select(type => new TypeExpression(type))
                    .ToList();
                exp = exp.And(typeExpressions.OrAll());
            }

           if (request.IncludedRarities != null && request.IncludedRarities.Count != 0)
            {
                var rarityExpressions = request.IncludedRarities
                    .Select(rarity => new RarityExpression(rarity))
                    .ToList();
                exp = exp.And(rarityExpressions.OrAll());
            }

            var query = new SearchQuery(exp);

            var parameters = new Dictionary<string, string>
            {
                { "q", query.GetRawText() },
                { "order", "edhrec" }
            };
            var parametersWithValue = parameters.Where(kv => !string.IsNullOrWhiteSpace(kv.Value));
            var encodedParameterString = string.Join("&", parametersWithValue.Select(kvp =>
            {
                if (!string.IsNullOrWhiteSpace(kvp.Value))
                {
                    return $"{kvp.Key}={UrlEncode(kvp.Value)}";
                }
                else
                {
                    return string.Empty;
                }
            }));
            var endpointUrl = $"{ScryfallApiBaseUrl}{ScryfallCardEndpoint}?{encodedParameterString}";

            var response = await client.GetAsync(endpointUrl);
            var content = await response.Content.ReadAsStringAsync();
            var obj = System.Text.Json.JsonSerializer.Deserialize<CardResponse>(content)!;

            return obj;
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "EdhWreck/1.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        private static string UrlEncode(string rawText)
        {
            var encodedText = System.Net.WebUtility.UrlEncode(rawText);
            return encodedText;
        }
    }
}
