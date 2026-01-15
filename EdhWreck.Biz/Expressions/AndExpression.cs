using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class AndExpression(ExpressionBase left, ExpressionBase right) : ExpressionBase
    {
        public override string GetRawText() => left.GetRawText() + " " + right.GetRawText();
    }
}
