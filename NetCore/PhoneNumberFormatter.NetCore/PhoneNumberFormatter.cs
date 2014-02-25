#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Text.RegularExpressions;

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
        public static string Format(string phoneNumber, string country = "")
        {
            CountryCodeRetriever.Initialize();
            if(string.IsNullOrEmpty(phoneNumber)) throw new Exception("The phoneNumber passed to formatter was null or empty.");
            var replaceAnyNoneDigit = new Regex(@"\D");
            var strippedPhoneNumber = replaceAnyNoneDigit.Replace(phoneNumber, string.Empty);
            if(PhoneNumberIsMissingCountryCode(strippedPhoneNumber))
            {
                var phoneNumberGroups = Regex.Split(strippedPhoneNumber, @"^(\d{3}).*(\d{2}).*(\d{3})$");
                phoneNumber = string.Format("{0} {1} {2}", phoneNumberGroups[1], phoneNumberGroups[2], phoneNumberGroups[3]);
                phoneNumber = AddCountryCode(phoneNumber, country);
            }
            return phoneNumber;
        }

        /// <summary>   Adds a country code to the phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumber">  The phone number. </param>
        /// <param name="country">      The country. </param>
        /// <returns>   The phone number with the country code attached to it. </returns>
        private static string AddCountryCode(string phoneNumber, string country)
        {
            return string.IsNullOrEmpty(country) ? phoneNumber : string.Format("{0} {1}", CountryCodeRetriever.GetCountryCode(country), phoneNumber);
        }

        /// <summary>   Check if phone number is missing country code. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="strippedPhoneNumber">  The stripped phone number with no space or other chars, only digits. </param>
        /// <returns>   true if phone number does not have a country code, false if it does. </returns>
        private static bool PhoneNumberIsMissingCountryCode(string strippedPhoneNumber)
        {
            return strippedPhoneNumber.Length == 8;
        }
    }
}