using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorNET;
using ValidatorNET.Enums;

namespace UnitTests
{
    [TestClass]
    public class LengthAndRangeChecks
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
        public void RangeChecks()
        {
            bool check = Validator.CheckRange(DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Assert.AreEqual(true, check);

            bool check2 = Validator.CheckRange(4, 2, 5);
            Assert.AreEqual(true, check2);

            bool check3 = Validator.CheckRange("asd", 2, 6);
            Assert.AreEqual(true, check3);
        }

        [TestMethod]
        public void LengthChecks()
        {
            bool check = Validator.CheckLength("test", 10, LengthOperator.LargerThan);

            Assert.AreEqual(false, check);

            bool check2 = Validator.CheckLength("test", 10, LengthOperator.LessThan);

            Assert.AreEqual(true, check2);
        }
    }
}
