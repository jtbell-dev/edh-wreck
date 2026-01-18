using EdhWreck.Biz.Extensions;

namespace EdhWreck.Biz.Expressions
{
    public class ColorIdentityExpression : KeyValueExpression
    {
        public ColorIdentityExpression() : this(0) { }
        public ColorIdentityExpression(Colors colors) : this(colors.ToQueryString()) { }
        public ColorIdentityExpression(Colors colors, ValueOperator oper) : this(colors.ToQueryString(), oper) { }
        public ColorIdentityExpression(string colorIdentity) : this(colorIdentity, ValueOperator.Default) { }
        public ColorIdentityExpression(string colorIdentity, ValueOperator oper) : base("id", oper, colorIdentity) { }
        public ColorIdentityExpression(int colorCount) : this(colorCount, ValueOperator.Default) { }
        public ColorIdentityExpression(int colorCount, ValueOperator oper) : base("id", oper, colorCount.ToString())
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(colorCount, 5);
        }
    }
}
