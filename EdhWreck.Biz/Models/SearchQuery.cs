using EdhWreck.Biz.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Biz.Models
{
    public class SearchQuery : ExpressionBase
    {
        public required ExpressionBase RootExpression { get; set; }

        public override string GetRawText() => RootExpression.GetRawText();
    }
}
