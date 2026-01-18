using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class ColorsExtensionsTests
    {
        [TestMethod]
        public void ColorExtensions_ToQueryString_ShouldReturnCorrectCode()
        {
            // Arrange & Act & Assert
            Assert.AreEqual("r", ColorsExtensions.ToQueryString(Colors.Red));
            Assert.AreEqual("g", ColorsExtensions.ToQueryString(Colors.Green));
            Assert.AreEqual("b", ColorsExtensions.ToQueryString(Colors.Black));
            Assert.AreEqual("u", ColorsExtensions.ToQueryString(Colors.Blue));
            Assert.AreEqual("w", ColorsExtensions.ToQueryString(Colors.White));
            Assert.AreEqual("0", ColorsExtensions.ToQueryString(Colors.None));
            Assert.AreEqual("gu", ColorsExtensions.ToQueryString(Colors.Blue | Colors.Green));
            Assert.AreEqual("bw", ColorsExtensions.ToQueryString(Colors.Orzhov));
            Assert.AreEqual("bgruw", ColorsExtensions.ToQueryString(Colors.Rainbow));
        }
    }
}
