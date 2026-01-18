using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class LegalityExpressionTests
    {
        [TestMethod]
        public void LegalityExpression_Banned_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression(LegalStatus.Banned, Formats.Commander);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("banned:commander", rawText);
        }


        [TestMethod]
        public void LegalityExpression_Legal_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression(LegalStatus.Legal, Formats.Penny);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("f:penny", rawText);
        }

        [TestMethod]
        public void LegalityExpression_Restricted_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression(LegalStatus.Restricted, Formats.Pauper);
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("restricted:pauper", rawText);
        }
    }
}
