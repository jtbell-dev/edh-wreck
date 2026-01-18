using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class EncapsulatedExpressionTests
    {
        [TestMethod]
        public void EncapsulatedExpression_ShouldReturnCorrectRawText()
        {
            // Arrange
            var innerExpression = new ColorExpression(ValueOperator.Equal, Colors.Green);
            var encapsulatedExpression = new EncapsulatedExpression(innerExpression);
            // Act
            var rawText = encapsulatedExpression.GetRawText();
            // Assert
            Assert.AreEqual("(c=g)", rawText);
        }

        [TestMethod]
        public void EncapsulatedExpression_Nested_ShouldReturnCorrectRawText()
        {
            // Arrange
            var innerMostExpression = new TypeExpression("artifact");
            var innerEncapsulated = new EncapsulatedExpression(innerMostExpression);
            var outerEncapsulated = new EncapsulatedExpression(innerEncapsulated);
            // Act
            var rawText = outerEncapsulated.GetRawText();
            // Assert
            Assert.AreEqual("((t:artifact))", rawText);
        }

        [TestMethod]
        public void EncapsulatedExpression_ComplexInnerExpression_ShouldReturnCorrectRawText()
        {
            // Arrange
            var left = new ColorExpression(ValueOperator.Equal, Colors.Blue);
            var right = new UsdPriceExpression(ValueOperator.LessThanOrEqual, 10.00m);
            var andExpression = new OrExpression(left, right);
            var encapsulatedExpression = new EncapsulatedExpression(andExpression);
            // Act
            var rawText = encapsulatedExpression.GetRawText();
            // Assert
            Assert.AreEqual("(c=u or usd<=10.00)", rawText);
        }
    }
}
