using EdhWreck.Biz.Extensions;

namespace EdhWreck.Biz.Expressions
{
    public class RarityExpression(Rarity rarity) : KeyValueExpression("r", ValueOperator.Default, rarity.ToSymbol())
    {
        public RarityExpression(string rarity) : this(Enum.Parse<Rarity>(rarity, true))
        {
            if (!Enum.TryParse<Rarity>(rarity, true, out _))
            {
                throw new ArgumentException("Invalid rarity value.", nameof(rarity));
            }
        }
    }

}
