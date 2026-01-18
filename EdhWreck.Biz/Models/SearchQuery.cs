using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Models
{
    public class SearchQuery
    {
        private readonly ExpressionBase _expression;

        public string? QueryText => _expression.GetRawText();

        public SearchQuery(ExpressionBase expression)
        {
            _expression = expression;
        }

        public string GetEncodedParameterString()
        {
            var parameters = new Dictionary<string, string>
            {
                { "q", _expression.GetRawText() },
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

            return encodedParameterString;
        }

        private static string UrlEncode(string rawText)
        {
            var encodedText = System.Net.WebUtility.UrlEncode(rawText);
            return encodedText;
        }
    }
}
