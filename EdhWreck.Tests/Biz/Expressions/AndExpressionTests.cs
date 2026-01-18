using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class AndExpressionTests
    {
        [TestMethod]
        public void AndExpression_ShouldReturnCombinedRawText()
        {
            // Arrange
            var left = new OracleTextExpression("flying");
            var right = new ColorExpression(ValueOperator.GreaterThanOrEqual, 3);
            var andExpression = new AndExpression(left, right);
            // Act
            var rawText = andExpression.GetRawText();
            // Assert
            Assert.AreEqual("o:\"flying\" c>=3", rawText);
        }

        [TestMethod]
        public void AndExpression_NestedExpressions_ShouldReturnCombinedRawText()
        {
            // Arrange
            var first = new TypeExpression("creature");
            var second = new ColorExpression(ValueOperator.Equal, Colors.Red);
            var third = new UsdPriceExpression(ValueOperator.LessThan, 5.00m);
            var firstAndSecond = new AndExpression(first, second);
            var nestedAndExpression = new AndExpression(firstAndSecond, third);
            // Act
            var rawText = nestedAndExpression.GetRawText();
            // Assert
            Assert.AreEqual("t:creature c=r usd<5.00", rawText);
        }
    }
}
