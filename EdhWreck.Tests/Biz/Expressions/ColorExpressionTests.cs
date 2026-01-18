using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class ColorExpressionTests
    {
        [TestMethod]
        public void ColorExpression_Numeric_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorExpression(ValueOperator.Equal, 2);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("c=2", rawText);
        }

        [TestMethod]
        public void ColorExpression_ColorName_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorExpression(ValueOperator.Equal, Colors.Rainbow);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("c=bgruw", rawText);
        }
    }
}
