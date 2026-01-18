using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class ExpressionExtensions
    {
        public static OrExpression Or(this ExpressionBase left, ExpressionBase right)
        {
            return new OrExpression(left, right);
        }

        public static AndExpression And(this ExpressionBase left, ExpressionBase right)
        {
            return new AndExpression(left, right);
        }

        public static NegatedKeyValueExpression Not(this KeyValueExpression expression)
        {
            return new NegatedKeyValueExpression(expression);
        }

        public static EncapsulatedExpression Encapsulate(this ExpressionBase expression)
        {
            return new EncapsulatedExpression(expression);
        }
    }
}
