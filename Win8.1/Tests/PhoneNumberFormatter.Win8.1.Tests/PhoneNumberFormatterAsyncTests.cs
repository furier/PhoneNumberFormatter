#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

#endregion

namespace PhoneNumberFormatter.Tests
{
    /// <summary>   Phone number formatter async tests. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    [TestClass]
    public class PhoneNumberFormatterAsyncTests
    {
        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_Only8Numbers()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("33369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber1()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3 3369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber2()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("33 369999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber3()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("333 69999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber4()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3336 9999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber5()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("33369 999", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber6()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("333699 99", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OneSpaceInNumber7()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3336999 9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_SpacesBetweenEveryNumber()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3 3 3 6 9 9 9 9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_MultipleSpacesInNumber()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3  3 3    699   99", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_NoneDigitCharsBetweenEveryNumber()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("3-3.3-6.9-9.9-9", "", "33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorge1()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("33369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorge2()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("004733369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorge3()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("+4733369999", "Norge", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_PhoneNumberWithNorwegianCountryCode1()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("+4733369999", "", "+47 33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_PhoneNumberWithNorwegianCountryCode2()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("004733369999", "", "+47 33 36 99 99");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorway1()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("33369999", "Norway", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorway2()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("004733369999", "Norway", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorway3()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("+4733369999", "Norway", "+47 333 69 999");
        }

        [TestMethod]
        public void PhoneNumberFormatterAsyncTest_OnlyNumbers_WithCountryNorway4()
        {
            ParameterizedPhoneNumberFormatterAsyncTest("+4633369999", "Norway", "+47 333 69 999");
        }

        /// <summary>   parameterized phone number formatter async test method. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumberInput">             The phone number. </param>
        /// <param name="countryInput">                 The country. </param>
        /// <param name="phoneNumberExpectedResult">    The expected phone number result. </param>
        private static void ParameterizedPhoneNumberFormatterAsyncTest(string phoneNumberInput, string countryInput, string phoneNumberExpectedResult)
        {
            var result = PhoneNumberFormatter.FormatAsync(phoneNumberInput, countryInput).Result;
            result.Should().Be(phoneNumberExpectedResult);
        }
    }
}