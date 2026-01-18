namespace EdhWreck.Biz.Expressions
{
    public class NegatedKeyValueExpression(KeyValueExpression innerExpression) : ExpressionBase
    {
        public override string GetRawText() => "-" + innerExpression.GetRawText();
    }
}
