using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class NullExpressionTests
    {
        [TestMethod]
        public void NullExpression_Default_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new NullExpression();
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("", rawText);
        }
    }
}
