using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Expressions
{
    public class OrExpression : ExpressionBase
    {
        private readonly ExpressionBase _left;
        private readonly ExpressionBase _right;
        public OrExpression(ExpressionBase left, ExpressionBase right)
        {
            _left = left;
            _right = right;
        }

        public override string GetRawText() => _left.GetRawText() + " or " + _right.GetRawText();
    }
}
