#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   A phone number formatter. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public static class PhoneNumberFormatter
    {
        /// <summary>   Formats an unformatted phone number, and adds country code if country is
        ///             supplied. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="phoneNumber">  The phone number. </param>
        /// <param name="countryName">      (Optional) The country. </param>
        /// <returns>   The formatted value. </returns>
        public static string Format(string phoneNumber, string countryName = "")
        {
            CountryMetaDataRetriever.Initialize();
            if(string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("phoneNumber");
            var countryMetaData = GetCountryMetaData(countryName);
            var formattedPhoneNumber = new PhoneNumber(countryMetaData, phoneNumber).ToString();
            return formattedPhoneNumber;
        }

        /// <summary>   Gets country meta data. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryName">  The country. </param>
        /// <returns>   The country meta data. </returns>
        private static CountryMetaData GetCountryMetaData(string countryName)
        {
            return string.IsNullOrEmpty(countryName) ? new CountryMetaData() : CountryMetaDataRetriever.GetCountryMetaData(countryName);
        }

        /// <summary>   Splits a string into parts. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
        /// <param name="s">            The s to act on. </param>
        /// <param name="partLength">   Length of the part. </param>
        /// <returns>   An enumerator that allows foreach to be used to process the split string parts. </returns>
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
}