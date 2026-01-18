using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class ExpressionEnumerableExtensions
    {
        public static EncapsulatedExpression OrAll(this IEnumerable<ExpressionBase> expressions)
        {
            using var enumerator = expressions.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("The collection must contain at least one expression.", nameof(expressions));
            }
            ExpressionBase combinedExpression = enumerator.Current;
            while (enumerator.MoveNext())
            {
                combinedExpression = combinedExpression.Or(enumerator.Current);
            }
            return new EncapsulatedExpression(combinedExpression);
        }

        public static EncapsulatedExpression AndAll(this IEnumerable<ExpressionBase> expressions)
        {
            using var enumerator = expressions.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException("The collection must contain at least one expression.", nameof(expressions));
            }
            ExpressionBase combinedExpression = enumerator.Current;
            while (enumerator.MoveNext())
            {
                combinedExpression = combinedExpression.And(enumerator.Current);
            }
            return new EncapsulatedExpression(combinedExpression);
        }
    }
}
