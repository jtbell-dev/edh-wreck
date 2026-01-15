using EdhWreck.Biz.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Tests.Biz
{
    [TestClass]
    public class SearchQueryExpressionTests
    {
        [TestMethod]
        public void AndExpression_ShouldReturnCombinedRawText()
        {
            // Arrange
            var left = new OracleTextExpression("flying");
            var right = new ColorExpression(ValueOperator.GreaterThanOrEqual, 3);
            var andExpression = new AndExpression(left, right);
            // Act
            var rawText = andExpression.GetRawText();
            // Assert
            Assert.AreEqual("o:\"flying\" c>=3", rawText);
        }
    }
}
