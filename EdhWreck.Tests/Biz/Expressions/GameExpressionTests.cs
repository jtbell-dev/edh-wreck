using EdhWreck.Biz.Expressions;

namespace EdhWreck.Tests.Biz.Expressions
{
    [TestClass]
    public class GameExpressionTests
    {
        [TestMethod]
        public void GameExpression_String_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new GameExpression("arena");
            // act
            var rawText = expression.GetRawText();
            // assert
            Assert.AreEqual("game:arena", rawText);
        }
    }
}
