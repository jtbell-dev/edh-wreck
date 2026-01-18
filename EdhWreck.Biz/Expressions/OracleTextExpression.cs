namespace EdhWreck.Biz.Expressions
{
    public class OracleTextExpression : KeyValueExpression
    {
        public OracleTextExpression(string value)
            : base("o", ValueOperator.Default, $"\"{value.Trim('\"')}\"")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Oracle text value cannot be null.", nameof(value));
            }

            var trimmedValue = value.Trim('\"');
            if (trimmedValue.IndexOf('\"') > 0)
            {
                throw new ArgumentException("Invalid oracle text.", nameof(value));
            }
        }
    }
}
