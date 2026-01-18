using EdhWreck.Biz.Extensions;

namespace EdhWreck.Biz.Expressions
{
    public class LegalityExpression(LegalStatus legalStatus, Formats format) : ExpressionBase
    {
        public LegalityExpression(string formatName) : this(LegalStatus.Legal, formatName) { }

        public LegalityExpression(LegalStatus legalStatus, string formatName) : this(legalStatus, Enum.Parse<Formats>(formatName, true)) { }

        public LegalityExpression(string legalStatusName, string formatName) : this(Enum.Parse<LegalStatus>(legalStatusName, true), formatName) { }

        public override string GetRawText() => format.ToQueryString(legalStatus);
    }
}
