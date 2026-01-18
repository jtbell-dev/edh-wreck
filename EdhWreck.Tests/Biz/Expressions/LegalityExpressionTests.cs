using EdhWreck.Biz.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void LegalityExpression_ValidName_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression("modern");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("f:modern", rawText);
        }

        [TestMethod]
        public void LegalityExpression_ValidStatusName_ValidFormatName_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression("banned", "commander");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("banned:commander", rawText);
        }

        [TestMethod]
        public void LegalityExpression_InvalidStatusName_ValidFormatName_ShouldReturnCorrectRawText()
        {
            // arrange, act, and assert
            const string invalidStatusName = "justplainawful";
            Assert.Throws<Exception>(() => new LegalityExpression(invalidStatusName, "commander"));
        }

        [TestMethod]
        public void LegalityExpression_ValidStatusName_InvalidFormatName_ShouldReturnCorrectRawText()
        {
            // arrange, act, and assert
            const string invalidFormatName = "homebrew";
            Assert.Throws<Exception>(() => new LegalityExpression("banned", invalidFormatName));
        }

        [TestMethod]
        public void LegalityExpression_ValidStatus_ValidFormatName_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new LegalityExpression(LegalStatus.Restricted, "paupercommander");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("restricted:paupercommander", rawText);
        }
    }
}
