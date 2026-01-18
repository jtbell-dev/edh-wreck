namespace EdhWreck.Biz.Expressions
{
    public class GameExpression(string game) : KeyValueExpression("game", ValueOperator.Default, game)
    {
    }
}
