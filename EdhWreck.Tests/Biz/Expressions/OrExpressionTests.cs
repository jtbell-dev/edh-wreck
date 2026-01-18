using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class OrExpressionTests
    {
        [TestMethod]
        public void OrExpression_Simple_ShouldReturnCorrectRawText()
        {
            // arrange
            var left = new OracleTextExpression("flying");
            var right = new ColorExpression(ValueOperator.GreaterThanOrEqual, 3);
            var orExpression = new OrExpression(left, right);
            // act
            var rawText = orExpression.GetRawText();
            // assert
            Assert.AreEqual("o:\"flying\" or c>=3", rawText);
        }
    }
}
