using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class NegatedExpression : ExpressionBase
    {
        public ExpressionBase InnerExpression { get; set; }
        public override string GetRawText() => "-" + InnerExpression.GetRawText();
    }
}
