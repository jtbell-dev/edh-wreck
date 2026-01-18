using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Tests.Biz.Models
{
    [TestClass]
    public class SearchQueryTests
    {
        [TestMethod]
        public void SearchQuery_GetRawText_ShouldReturnCorrectRawText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            var searchQuery = new SearchQuery(expression);
            // act
            var rawText = searchQuery.GetRawText();
            // assert
            Assert.AreEqual("o:\"flying\"", rawText);
        }
    }
}
