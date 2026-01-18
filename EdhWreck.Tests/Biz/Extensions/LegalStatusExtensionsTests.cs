using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class LegalStatusExtensionsTests
    {
        [TestMethod]
        public void LegalStatusExtensions_ToQueryString_ShouldReturnCorrectRawText()
        {
            // arrange
            var status = LegalStatus.Banned;
            // act
            var result = status.ToQueryString();
            // assert
            Assert.AreEqual("banned", result);
        }
    }
}
