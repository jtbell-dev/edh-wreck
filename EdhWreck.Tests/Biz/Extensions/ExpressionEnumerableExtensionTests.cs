using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class ExpressionEnumerableExtensionTests
    {
        [TestMethod]
        public void ExpressionEnumerableExtensions_OrAll_EmptyList_ShouldThrowException()
        {
            // arrange
            var expressions = new List<NullExpression>();
            // act & assert
            Assert.Throws<ArgumentException>(expressions.OrAll);
        }

        [TestMethod]
        public void ExpressionEnumerableExtensions_OrAll_SingleItem_ShouldReturnCorrectRawText()
        {
            // arrange
            var expressions = new List<OracleTextExpression>
            {
                new OracleTextExpression("flying")
            };
            // act
            var result = expressions.OrAll();
            var rawText = result.GetRawText();
            // assert
            Assert.AreEqual("(o:\"flying\")", rawText);
        }


        [TestMethod]
        public void ExpressionEnumerableExtensions_OrAll_MultipleItems_ShouldReturnCorrectRawText()
        {
            // arrange
            var expressions = new List<ExpressionBase>
            {
                new OracleTextExpression("flying"),
                new RarityExpression("rare"),
                new TypeExpression("creature")
            };
            // act
            var result = expressions.OrAll();
            var rawText = result.GetRawText();
            // assert
            Assert.AreEqual("(o:\"flying\" or r:rare or t:creature)", rawText);
        }

        [TestMethod]
        public void ExpressionEnumerableExtensions_AndAll_EmptyList_ShouldThrowException()
        {
            // arrange
            var expressions = new List<NullExpression>();
            // act & assert
            Assert.Throws<ArgumentException>(expressions.AndAll);
        }

        [TestMethod]
        public void ExpressionEnumerableExtensions_AndAll_SingleItem_ShouldReturnCorrectRawText()
        {
            // arrange
            var expressions = new List<OracleTextExpression>
            {
                new OracleTextExpression("flying")
            };
            // act
            var result = expressions.AndAll();
            var rawText = result.GetRawText();
            // assert
            Assert.AreEqual("(o:\"flying\")", rawText);
        }


        [TestMethod]
        public void ExpressionEnumerableExtensions_AndAll_MultipleItems_ShouldReturnCorrectRawText()
        {
            // arrange
            var expressions = new List<ExpressionBase>
            {
                new OracleTextExpression("flying"),
                new RarityExpression("rare"),
                new TypeExpression("creature")
            };
            // act
            var result = expressions.AndAll();
            var rawText = result.GetRawText();
            // assert
            Assert.AreEqual("(o:\"flying\" r:rare t:creature)", rawText);
        }
    }
}
