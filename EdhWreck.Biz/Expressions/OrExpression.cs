namespace EdhWreck.Biz.Expressions
{
    public class OrExpression(ExpressionBase left, ExpressionBase right) : ExpressionBase
    {
        public override string GetRawText() => left.GetRawText() + " or " + right.GetRawText();
    }
}
