#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   A phone number formatter. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public static class PhoneNumberFormatter
    {
        /// <summary>   Formats an unformatted phone number, and adds country code if country is supplied. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="phoneNumber">  The phone number. </param>
        /// <param name="country">      The country. </param>
        /// <returns>   The formatted value. </returns>
        public static async Task<string> FormatAsync(string phoneNumber, string country = "")
        {
            await CountryCodeRetriever.Initialize();
            if(string.IsNullOrEmpty(phoneNumber)) throw new Exception("The phoneNumber passed to formatter was null or empty.");
            var countryMetaData = GetCountryMetaData(country);
            var strippedPhoneNumber = CleanPhoneNumber(phoneNumber);
            var formattedPhoneNumber = FormatPhoneNumber(countryMetaData, strippedPhoneNumber);
            return formattedPhoneNumber;
        }

        /// <summary>   Gets country meta data. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="country">  The country. </param>
        /// <returns>   The country meta data. </returns>
        private static CountryMetaData GetCountryMetaData(string country)
        {
            return string.IsNullOrEmpty(country) ? new CountryMetaData() : CountryCodeRetriever.GetCountryMetaData(country);
        }

        /// <summary>   Clean phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumber">  The phone number. </param>
        /// <returns>   A string. </returns>
        private static string CleanPhoneNumber(string phoneNumber)
        {
            var replaceAnyNoneDigitPattern = new Regex(@"\D");
            var cleanedPhoneNumber = replaceAnyNoneDigitPattern.Replace(phoneNumber, string.Empty);
            cleanedPhoneNumber = AddPlussSignIfItWasStripped(phoneNumber, cleanedPhoneNumber);
            return cleanedPhoneNumber;
        }

        /// <summary>   Adds the pluss sign if iterator was stripped to 'strippedPhoneNumber'. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumber">          The phone number. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   A string. </returns>
        private static string AddPlussSignIfItWasStripped(string phoneNumber, string strippedPhoneNumber)
        {
            return phoneNumber.StartsWith("+") ? string.Format("+{0}", strippedPhoneNumber) : strippedPhoneNumber;
        }

        /// <summary>   Format phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   The formatted phone number. </returns>
        private static string FormatPhoneNumber(CountryMetaData countryMetaData, string strippedPhoneNumber)
        {
            var phoneNumber = PhoneNumberHasCountryCode(countryMetaData, strippedPhoneNumber)
                    ? ProcessPhoneNumberWithCountryCode(countryMetaData, strippedPhoneNumber)
                    : ProcessPhoneNumberWithoutCountryCode(countryMetaData, strippedPhoneNumber);
            return phoneNumber;
        }

        /// <summary>   Process the phone number with country code. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   A string. </returns>
        private static string ProcessPhoneNumberWithCountryCode(CountryMetaData countryMetaData, string strippedPhoneNumber)
        {
            var phoneNumber = SplitFormatPhoneNumber(countryMetaData, strippedPhoneNumber);
            phoneNumber = AddCountryCode(countryMetaData, phoneNumber);
            return phoneNumber;
        }

        /// <summary>   Phone number has country code. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        private static bool PhoneNumberHasCountryCode(CountryMetaData countryMetaData, string strippedPhoneNumber)
        {
            return strippedPhoneNumber.Length > countryMetaData.PhoneNumberDigitCount && (strippedPhoneNumber.StartsWith("00") || strippedPhoneNumber.StartsWith("+"));
        }

        /// <summary>   Processes a phone number without a country code. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   A string. </returns>
        private static string ProcessPhoneNumberWithoutCountryCode(CountryMetaData countryMetaData, string strippedPhoneNumber)
        {
            var phoneNumber = SplitFormatPhoneNumber(countryMetaData, strippedPhoneNumber);
            phoneNumber = AddCountryCode(countryMetaData, phoneNumber);
            return phoneNumber;
        }

        /// <summary>   Split formats the phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryMetaData"></param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   A string. </returns>
        private static string SplitFormatPhoneNumber(CountryMetaData countryMetaData, string strippedPhoneNumber) {}

        /// <summary>   Adds a country code to the phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumber">  The phone number. </param>
        /// <param name="country">      The country. </param>
        /// <returns>   The phone number with the country code attached to it. </returns>
        private static string AddCountryCode(CountryMetaData country, string phoneNumber)
        {
            return string.IsNullOrEmpty(country.CountryCode) ? phoneNumber : string.Format("+{0} {1}", country.CountryCode, phoneNumber);
        }
    }
}