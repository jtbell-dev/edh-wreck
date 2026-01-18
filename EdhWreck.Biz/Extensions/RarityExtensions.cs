using EdhWreck.Biz.Expressions;

namespace EdhWreck.Biz.Extensions
{
    public static class RarityExtensions
    {
        public static string ToSymbol(this Rarity rarity)
        {
            var enumName = Enum.GetName(rarity)!;
            return enumName.ToLower();
        }
    }
}
