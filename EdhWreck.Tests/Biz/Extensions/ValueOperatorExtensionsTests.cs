using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class ValueOperatorExtensionsTests
    {
        [TestMethod]
        public void ValueOperatorExtensions_ToSymbol_ShouldReturnCorrectRawText()
        {
            // arrange
            var testCases = new Dictionary<ValueOperator, string>
            {
                { ValueOperator.Default, ":" },
                { ValueOperator.Equal, "=" },
                { ValueOperator.NotEqual, "!=" },
                { ValueOperator.GreaterThan, ">" },
                { ValueOperator.LessThan, "<" },
                { ValueOperator.GreaterThanOrEqual, ">=" },
                { ValueOperator.LessThanOrEqual, "<=" }
            };
            // act & assert
            foreach (var testCase in testCases)
            {
                var result = testCase.Key.ToSymbol();
                Assert.AreEqual(testCase.Value, result, $"Failed for ValueOperator: {testCase.Key}");
            }
        }
    }
}
