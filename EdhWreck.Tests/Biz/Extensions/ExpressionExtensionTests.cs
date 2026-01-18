using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class ExpressionExtensionTests
    {
        [TestMethod]
        public void ExpressionExtensions_And_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            var otherExpression = new OracleTextExpression("haste");
            // act
            var combinedExpression = expression.And(otherExpression);
            var rawText = combinedExpression.GetRawText();
            // assert
            Assert.AreEqual("o:\"flying\" o:\"haste\"", rawText);
        }

        [TestMethod]
        public void ExpressionExtensions_Or_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            var otherExpression = new OracleTextExpression("haste");
            // act
            var combinedExpression = expression.Or(otherExpression);
            var rawText = combinedExpression.GetRawText();
            // assert
            Assert.AreEqual("o:\"flying\" or o:\"haste\"", rawText);
        }

        [TestMethod]
        public void ExpressionExtensions_Not_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            // act
            var notExpression = expression.Not();
            var rawText = notExpression.GetRawText();
            // assert
            Assert.AreEqual("-o:\"flying\"", rawText);
        }

        [TestMethod]
        public void ExpressionExtensions_Encapsulate_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            // act
            var parenthesizedExpression = expression.Encapsulate();
            var rawText = parenthesizedExpression.GetRawText();
            // assert
            Assert.AreEqual("(o:\"flying\")", rawText);
        }
    }
}
