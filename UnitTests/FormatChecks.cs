using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorNET;
using ValidatorNET.Enums;

namespace UnitTests
{
    [TestClass]
    public class FormatChecks
    {
        [TestMethod]
        public void PostalCodeFormatCheck()
        {
            Assert.AreEqual(true, Validator.CheckPostalCode("75008", PostalFormat.France));
            Assert.AreEqual(true, Validator.CheckPostalCode("02801", PostalFormat.Germany));
            Assert.AreEqual(true, Validator.CheckPostalCode("102-8166", PostalFormat.Japan));
            Assert.AreEqual(true, Validator.CheckPostalCode("847283", PostalFormat.China));

            // American 9 digit zip code
            Assert.AreEqual(true, Validator.CheckPostalCode("33701-4313", PostalFormat.America));

            // American 5 digit zip code
            Assert.AreEqual(true, Validator.CheckPostalCode("55416", PostalFormat.America));
            Assert.AreEqual(true, Validator.CheckPostalCode("4200", PostalFormat.Denmark));
            Assert.AreEqual(true, Validator.CheckPostalCode("384230", PostalFormat.India));
            Assert.AreEqual(true, Validator.CheckPostalCode("1792", PostalFormat.Swiss));
            Assert.AreEqual(true, Validator.CheckPostalCode("984283", PostalFormat.Russian));
            Assert.AreEqual(true, Validator.CheckPostalCode("5343", PostalFormat.Australia));
            Assert.AreEqual(true, Validator.CheckPostalCode("K1A0B1", PostalFormat.Canada));
            Assert.AreEqual(true, Validator.CheckPostalCode("SW1A 0AA", PostalFormat.UnitedKingdom));
            Assert.AreEqual(true, Validator.CheckPostalCode("04552-050", PostalFormat.Brazil));
            Assert.AreEqual(true, Validator.CheckPostalCode("3823 AB", PostalFormat.Dutch));

            // Better than nothing
            Assert.AreEqual(true, Validator.CheckPostalCode("test1234", PostalFormat.Invariant));
        }

        [TestMethod]
        public void PhoneNumberFormatCheck()
        {
            Assert.AreEqual(true, Validator.CheckPhoneNumber("0494778899", PhoneFormat.France));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("04 94 77 88 99", PhoneFormat.France));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("0594 123 456", PhoneFormat.France));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+594 594 123 456", PhoneFormat.France));
            Assert.AreEqual(false, Validator.CheckPhoneNumber("1494778899", PhoneFormat.France));
            Assert.AreEqual(false, Validator.CheckPhoneNumber("123456789", PhoneFormat.France));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("(099)12 34 56", PhoneFormat.Germany));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+49 (0) 123 456789", PhoneFormat.Germany));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("(031234) 111111", PhoneFormat.Germany));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("01211-1111111", PhoneFormat.Germany));
            Assert.AreEqual(false, Validator.CheckPhoneNumber("1", PhoneFormat.Germany));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("01-2345-6789", PhoneFormat.Japan));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("011-81-3-9999-9999", PhoneFormat.Japan));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+81-3-9999-9999", PhoneFormat.Japan));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("82-486 1234", PhoneFormat.Japan));
            Assert.AreEqual(false, Validator.CheckPhoneNumber("1", PhoneFormat.Japan));
            Assert.AreEqual(false, Validator.CheckPhoneNumber("123456", PhoneFormat.Japan));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("234-14242424", PhoneFormat.China));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("(999)99999999", PhoneFormat.China));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("999-99999999", PhoneFormat.China));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("555-383-3344", PhoneFormat.America));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("1-234-567-8901 91234", PhoneFormat.America));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("12345678901", PhoneFormat.America));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("1 (234) 567-8901", PhoneFormat.America));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("99999-999999", PhoneFormat.India));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("9999-9999999", PhoneFormat.India));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("972-367087", PhoneFormat.Spain));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("999999999", PhoneFormat.Spain));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("99-9999999", PhoneFormat.Spain));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("999-999999", PhoneFormat.Spain));

            Assert.AreEqual(true, Validator.CheckPhoneNumber("+44 333 333 333", PhoneFormat.UnitedKingdom));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("(23)343-4343", PhoneFormat.Brazil));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+31(0)235256677", PhoneFormat.Dutch));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("(02)12341234", PhoneFormat.Australia));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("2342523452", PhoneFormat.Israel));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("(038)8383748", PhoneFormat.NewZealand));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+7(916)9985670", PhoneFormat.Russia));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("22737458", PhoneFormat.Invariant));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("023-55116", PhoneFormat.Sweden));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("3486543653", PhoneFormat.Italy));
            
            Assert.AreEqual(true, Validator.CheckPhoneNumber("20293822", PhoneFormat.Denmark));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("+45 20293822", PhoneFormat.Denmark));
            Assert.AreEqual(true, Validator.CheckPhoneNumber("45 20293822", PhoneFormat.Denmark));
        }

        [TestMethod]
        public void SSNFormatCheck()
        {
            Assert.AreEqual(true, Validator.CheckSocialSecurityNumber("19238433928394829X", SocialSecurityNumberFormat.China));
            Assert.AreEqual(true, Validator.CheckSocialSecurityNumber("145470191", SocialSecurityNumberFormat.America));
            Assert.AreEqual(true, Validator.CheckSocialSecurityNumber("120384-2833", SocialSecurityNumberFormat.Denmark));
            Assert.AreEqual(true, Validator.CheckSocialSecurityNumber("1 81 04 95 201 569 62", SocialSecurityNumberFormat.France));
        }

        [TestMethod]
        public void CheckURI()
        {
            Assert.AreEqual(true, Validator.CheckUri("http://google.net/"));
            Assert.AreEqual(true, Validator.CheckUri("http://google.com"));
            Assert.AreEqual(true, Validator.CheckUri("http://google.com/page.aspx"));
            Assert.AreEqual(true, Validator.CheckUri("http://google.com/page.aspx?var=value"));
            Assert.AreEqual(true, Validator.CheckUri("http://google.com/page.aspx?var=value&var2=value2"));
            Assert.AreEqual(true, Validator.CheckUri("http://www.google.com/"));
            Assert.AreEqual(false, Validator.CheckUri("google.com/path???/file"));
        }

        [TestMethod]
        public void CheckEmail()
        {
            Assert.AreEqual(true, Validator.CheckEmail("ian@google.com"));
            Assert.AreEqual(true, Validator.CheckEmail("a1@a.aa"));
            Assert.AreEqual(false, Validator.CheckEmail("@ad.com"));
            Assert.AreEqual(false, Validator.CheckEmail("asd@.com"));
            Assert.AreEqual(false, Validator.CheckEmail("asd@asd."));
        }
    }
}
