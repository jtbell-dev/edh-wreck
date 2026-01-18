using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class RarityExtensionsTests
    {
        [TestMethod]
        public void RarityExtensions_ToSymbol_ShouldReturnCorrectRawText()
        {
            // arrange
            var rarity = Rarity.Mythic;
            // act
            var result = rarity.ToSymbol();
            // assert
            Assert.AreEqual("mythic", result);
        }
    }
}
