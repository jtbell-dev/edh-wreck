using EdhWreck.Biz.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class KeyValueExpression(string key, ValueOperator oper, string value) : ExpressionBase
    {
        public override string GetRawText()
        {
            return $"{key}{oper.ToSymbol()}{value}";
        }
    }
}
