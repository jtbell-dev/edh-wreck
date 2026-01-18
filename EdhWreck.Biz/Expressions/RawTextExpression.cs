namespace EdhWreck.Biz.Expressions
{
    public class RawTextExpression(string rawText) : ExpressionBase
    {
        public override string GetRawText() => rawText;
    }
}
