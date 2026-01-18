using EdhWreck.Biz.Expressions;
using EdhWreck.Biz.Extensions;

namespace EdhWreck.Tests.Biz.Extensions
{
    [TestClass]
    public class FormatsExtensionsTests
    {
        [TestMethod]
        public void FormatsExtensions_ToQueryString_Valid_ShouldReturnCorrectRawText()
        {
            // arrange
            var legalstatus = LegalStatus.Restricted;
            var format = Formats.PauperCommander;
            // act
            var result = format.ToQueryString(legalstatus);
            // assert
            Assert.AreEqual("restricted:paupercommander", result);
        }
    }
}
