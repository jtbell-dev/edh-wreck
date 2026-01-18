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
        public void SearchQuery_GetEncodedParameterString_SingleExpression_ShouldReturnCorrectParameterText()
        {
            // arrange
            var expression = new OracleTextExpression("flying");
            var searchQuery = new SearchQuery(expression);
            // act
            var rawText = searchQuery.GetEncodedParameterString();
            // assert
            Assert.AreEqual("q=o%3A%22flying%22&order=edhrec", rawText);
        }

        [TestMethod]
        public void SearchQuery_GetEncodedParameterString_NullExpression_ShouldReturnCorrectParameterText()
        {
            // arrange
            var expression = new NullExpression();
            var searchQuery = new SearchQuery(expression);
            // act
            var rawText = searchQuery.GetEncodedParameterString();
            // assert
            Assert.AreEqual("order=edhrec", rawText);
        }
    }
}
