using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class ValueOperatorExtensions
    {
        extension(ValueOperator op)
        {
            public string ToSymbol() => op switch
            {
                ValueOperator.Default => ":",
                ValueOperator.Equal => "=",
                ValueOperator.NotEqual => "!=",
                ValueOperator.GreaterThan => ">",
                ValueOperator.LessThan => "<",
                ValueOperator.GreaterThanOrEqual => ">=",
                ValueOperator.LessThanOrEqual => "<=",
                _ => throw new ArgumentOutOfRangeException(nameof(op), $"Not expected operator value: {op}"),
            };
        }
    }
}
