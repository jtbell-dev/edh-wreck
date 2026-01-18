using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class UsdPriceExpressionTests
    {
        [TestMethod]
        public void UsePriceExpression_Value_WithOperator_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new UsdPriceExpression(ValueOperator.GreaterThanOrEqual, 3.50m);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("usd>=3.50", rawText);
        }

        [TestMethod]
        public void UsePriceExpression_Value_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new UsdPriceExpression(3.50m);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("usd<=3.50", rawText);
        }
    }
}
