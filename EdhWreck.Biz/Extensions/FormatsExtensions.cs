using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class FormatsExtensions
    {
        extension(Formats formats)
        {
            /// <summary>
            /// Convert the flagged Formats value to a comma-separated string of format names.
            /// Example: Formats.Standard | Formats.Modern => "Standard,Modern"
            /// </summary>
            public string ToQueryString(LegalStatus legalStatus)
            {
                if (formats == 0)
                    return string.Empty;
                var formatExpressions = new List<KeyValueExpression>();
                foreach (Formats format in Enum.GetValues<Formats>())
                {
                    if (format != 0 && formats.HasFlag(format))
                    {
                        var key = legalStatus.ToQueryString();
                        var oper = ValueOperator.Default;
                        var value = format.ToString().ToLower();
                        formatExpressions.Add(new KeyValueExpression(key, oper, value));
                    }
                }
                return string.Join(" ", formatExpressions.Select(e => e.GetRawText()));
            }
        }
    }
}