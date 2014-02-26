#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace PhoneNumberFormatter.Tests
{
    /// <summary>   Phone number formatter tests. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    [TestClass]
    public class PhoneNumberFormatterTests
    {
        [TestMethod]
        public void PhoneNumberFormatterTest_Only8Numbers()
        {
            ParameterizedPhoneNumberFormatterTest("33369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber1()
        {
            ParameterizedPhoneNumberFormatterTest("3 3369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber2()
        {
            ParameterizedPhoneNumberFormatterTest("33 369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber3()
        {
            ParameterizedPhoneNumberFormatterTest("333 69999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber4()
        {
            ParameterizedPhoneNumberFormatterTest("3336 9999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber5()
        {
            ParameterizedPhoneNumberFormatterTest("33369 999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber6()
        {
            ParameterizedPhoneNumberFormatterTest("333699 99", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OneSpaceInNumber7()
        {
            ParameterizedPhoneNumberFormatterTest("3336999 9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_SpacesBetweenEveryNumber()
        {
            ParameterizedPhoneNumberFormatterTest("3 3 3 6 9 9 9 9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_MultipleSpacesInNumber()
        {
            ParameterizedPhoneNumberFormatterTest("3  3 3    699   99", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_NoneDigitCharsBetweenEveryNumber()
        {
            ParameterizedPhoneNumberFormatterTest("3-3.3-6.9-9.9-9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorge1()
        {
            ParameterizedPhoneNumberFormatterTest("33369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorge2()
        {
            ParameterizedPhoneNumberFormatterTest("004733369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorge3()
        {
            ParameterizedPhoneNumberFormatterTest("+4733369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_PhoneNumberWithNorwegianCountryCode1()
        {
            ParameterizedPhoneNumberFormatterTest("+4733369999", "", "+47 33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_PhoneNumberWithNorwegianCountryCode2()
        {
            ParameterizedPhoneNumberFormatterTest("004733369999", "", "+47 33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorway1()
        {
            ParameterizedPhoneNumberFormatterTest("33369999", "Norway", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorway2()
        {
            ParameterizedPhoneNumberFormatterTest("004733369999", "Norway", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterTest_OnlyNumbers_WithCountryNorway3()
        {
            ParameterizedPhoneNumberFormatterTest("+4733369999", "Norway", "+47 333 69 999");
        }

        /// <summary>   parameterized phone number formatter test method. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumberInput">             The phone number. </param>
        /// <param name="countryInput">                 The country. </param>
        /// <param name="phoneNumberExpectedResult">    The expected phone number result. </param>
        private static void ParameterizedPhoneNumberFormatterTest(string phoneNumberInput, string countryInput, string phoneNumberExpectedResult)
        {
            var result = PhoneNumberFormatter.Format(phoneNumberInput, countryInput);
            result.Should().Be(phoneNumberExpectedResult);
        }
    }
}