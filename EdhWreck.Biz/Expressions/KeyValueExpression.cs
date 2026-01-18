using EdhWreck.Biz.Extensions;

namespace EdhWreck.Biz.Expressions
{
    public class KeyValueExpression : ExpressionBase
    {
        private readonly string _key;
        private readonly ValueOperator _oper;
        private readonly string _value;

        public KeyValueExpression(string key, ValueOperator oper, string value)
        {
            _key = key;
            _oper = oper;
            _value = value;

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key missing.", nameof(key));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value missing.", nameof(value));
            }
        }

        public override string GetRawText()
        {
            return $"{_key}{_oper.ToSymbol()}{_value}";
        }
    }
}
