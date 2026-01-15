using EdhWreck.Biz.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class LegalityExpression(LegalStatus legalStatus, Formats legality) : ExpressionBase
    {
        public override string GetRawText() => legality.ToQueryString(legalStatus);
    }
}
