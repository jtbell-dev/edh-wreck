using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class TypeExpressionTests
    {
        [TestMethod]
        public void TypeExpression_String_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new TypeExpression("artifact");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("t:artifact", rawText);
        }
    }
}
