using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class ColorIdentityExpressionTests
    {
        [TestMethod]
        public void ColorIdentityExpression_Default_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression();
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id:0", rawText);
        }


        [TestMethod]
        public void ColorIdentityExpression_Numeric_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression(5);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id:5", rawText);
        }


        [TestMethod]
        public void ColorIdentityExpression_NumericGreaterThan5_ShouldThrowException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ColorIdentityExpression(6));
        }


        [TestMethod]
        public void ColorIdentityExpression_NumericGreaterThan5_WithOperator_ShouldThrowException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ColorIdentityExpression(6, ValueOperator.LessThanOrEqual));
        }


        [TestMethod]
        public void ColorIdentityExpression_String_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression("rb");
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id:rb", rawText);
        }


        [TestMethod]
        public void ColorIdentityExpression_String_WithOperator_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression("rb", ValueOperator.GreaterThanOrEqual);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id>=rb", rawText);
        }


        [TestMethod]
        public void ColorIdentityExpression_Colors_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression(Colors.Orzhov);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id:bw", rawText);
        }

        [TestMethod]
        public void ColorIdentityExpression_Colors_WithOperator_ShouldReturnCorrectRawText()
        {
            // Arrange
            var expression = new ColorIdentityExpression(Colors.Jund, ValueOperator.LessThan);
            // Act
            var rawText = expression.GetRawText();
            // Assert
            Assert.AreEqual("id<bgr", rawText);
        }
    }
}
