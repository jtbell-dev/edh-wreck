using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class OracleTextExpressionTests
    {
        [TestMethod]
        public void OracleTextExpression_WithDoubleQuotes_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("\"creatures you control\"");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("o:\"creatures you control\"", rawText);
        }

        [TestMethod]
        public void OracleTextExpression_WithoutDoubleQuotes_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("o:\"flying\"", rawText);
        }

        [TestMethod]
        public void OracleTextExpression_EmptyString_ShouldThrowException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new OracleTextExpression(string.Empty));
        }

        [TestMethod]
        public void OractleTextExpression_WithInvalidOracleText_ShouldThrowException()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentException>(() => new OracleTextExpression("this string has \"inner double\" quotes"));
        }
    }
}
