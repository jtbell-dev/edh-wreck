using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class RarityExpressionTests
    {
        [TestMethod]
        public void RarityExpression_RarityEnum_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new RarityExpression(Rarity.Mythic);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("r:mythic", rawText);
        }

        [TestMethod]
        public void RarityExpression_CasedString_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new RarityExpression("rArE");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("r:rare", rawText);
        }

        [TestMethod]
        public void RarityExpression_ValidString_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new RarityExpression("uncommon");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("r:uncommon", rawText);
        }

        [TestMethod]
        public void RarityExpression_InvalidString_ShouldThrowException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new RarityExpression("legen-waitforit-dary"));
        }
    }
}
