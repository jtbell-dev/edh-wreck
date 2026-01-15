using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class RawTextExpression(string rawText) : ExpressionBase
    {
        public override string GetRawText() => rawText;
    }
}
