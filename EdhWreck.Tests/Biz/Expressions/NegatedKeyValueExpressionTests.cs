using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class NegatedKeyValueExpressionTests
    {
        [TestMethod]
        public void NegatedKeyValueExpression_And_ShouldReturnCorrectRawText()
        {
            // arrange
            var innerExpression = new KeyValueExpression("foo", ValueOperator.Equal, "bar");
            var negatedExpression = new NegatedKeyValueExpression(innerExpression);
            // act
            var rawText = negatedExpression.GetRawText();
            // assert
            Assert.AreEqual("-foo=bar", rawText);
        }
    }
}
