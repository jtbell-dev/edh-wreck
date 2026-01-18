using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Models
{
    public class SearchQuery : ExpressionBase
    {
        private readonly ExpressionBase _expression;
        public SearchQuery(ExpressionBase expression)
        {
            _expression = expression;
        }

        public override string GetRawText() => _expression.GetRawText();
    }
}
