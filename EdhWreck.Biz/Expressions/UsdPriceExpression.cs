namespace EdhWreck.Biz.Expressions
{
    public class UsdPriceExpression : KeyValueExpression
    {
        public UsdPriceExpression(decimal value) : base("usd", ValueOperator.LessThanOrEqual, value.ToString("F2")) { }
        public UsdPriceExpression(ValueOperator oper, decimal value) : base("usd", oper, value.ToString("F2")) { }
    }
}
