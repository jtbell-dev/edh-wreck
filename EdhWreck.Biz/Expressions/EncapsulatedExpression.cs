namespace EdhWreck.Biz.Expressions
{
    public class EncapsulatedExpression(ExpressionBase innerExpression) : ExpressionBase
    {
        public override string GetRawText() => "(" + innerExpression.GetRawText() + ")";
    }
}
