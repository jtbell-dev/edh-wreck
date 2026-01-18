using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class KeyValueExpressionTests
    {
        [TestMethod]
        public void KeyValueExpression_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new KeyValueExpression("foo", ValueOperator.GreaterThanOrEqual, "bar");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("foo>=bar", rawText);
        }

        [TestMethod]
        public void KeyValueExpression_MissingKey_ShouldThrowException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new KeyValueExpression("", ValueOperator.Equal, "bar"));
        }

        [TestMethod]
        public void KeyValueExpression_MissingValue_ShouldThrowException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new KeyValueExpression("foo", ValueOperator.Equal, ""));
        }
    }
}
