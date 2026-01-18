namespace EdhWreck.Biz.Expressions
{
    public class TypeExpression(string value) : KeyValueExpression("t", ValueOperator.Default, value) { }
}
