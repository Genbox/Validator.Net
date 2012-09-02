using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorNET;

namespace UnitTests
{
    [TestClass]
    public class NormalizingChecks
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NormalizingChecksTests()
        {
            string hexUrl = Validator.ConvertHexToAscii("&#x54;&#x68;&#x69;&#x73;&#x20;&#x69;&#x73;&#x20;&#x61;&#x20;&#x74;&#x65;&#x73;&#x74;&#x20;&#x73;&#x74;&#x72;&#x69;&#x6E;&#x67;&#x20;&#x3C;&#x2D;&#x3E;");
            Assert.AreEqual("This is a test string <->", hexUrl);

            string hexHtml = Validator.ConvertHexToAscii("%54%68%69%73%20%69%73%20%61%20%74%65%73%74%20%73%74%72%69%6E%67%20%3C%2D%3E");
            Assert.AreEqual("This is a test string <->", hexHtml);

            //string unicode = Validator.EncodeAsciiToUnicode("this is a test זרו");
            //Assert.Inconclusive("Please make this test");

            string htmlEncoded = Validator.HtmlEncodeCharacters("< >");
            Assert.AreEqual("&lt; &gt;", htmlEncoded);

            string htmlDecoded = Validator.HtmlDecodeCharacters("&lt; &gt;");
            Assert.AreEqual("< >", htmlDecoded);
        }
    }
}
