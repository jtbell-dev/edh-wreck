using EdhWreck.Biz.Extensions;

namespace EdhWreck.Biz.Expressions
{
    public class LegalityExpression(LegalStatus legalStatus, Formats legality) : ExpressionBase
    {
        public override string GetRawText() => legality.ToQueryString(legalStatus);
    }
}
