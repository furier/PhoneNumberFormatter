#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************
// // <copyright file="PhoneNumber.cs" company="Bouvet ASA">
// //     Copyright (c) Bouvet ASA. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   A phone number. </summary>
    /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
    public class PhoneNumber
    {
        /// <summary>   Constructor. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryMetaData">          Information describing the country meta data. </param>
        /// <param name="unformattedPhoneNumber">   The unformatted phone number. </param>
        public PhoneNumber(CountryMetaData countryMetaData, string unformattedPhoneNumber)
        {
            NumberGroups = new List<string>();

            var cleanedPhoneNumber = CleanPhoneNumber(unformattedPhoneNumber);

            InitializePhoneNumberFields(countryMetaData, cleanedPhoneNumber);
            InitializePhoneNumberFormatNumberGroups(countryMetaData);
        }

        /// <summary>   Initializes the phone number fields. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country meta data. </param>
        /// <param name="cleanedPhoneNumber">   The cleaned phone number. </param>
        private void InitializePhoneNumberFields(CountryMetaData countryMetaData, string cleanedPhoneNumber)
        {
            var countryCode = cleanedPhoneNumber.Substring(0, cleanedPhoneNumber.Length - countryMetaData.PhoneNumberDigitCount);
            var phoneNumber = cleanedPhoneNumber.Substring(cleanedPhoneNumber.Length - countryMetaData.PhoneNumberDigitCount);

            if(PhoneNumberAlreadyHasCountryCode(countryMetaData, cleanedPhoneNumber))
            {
                CountryCode = countryCode;
                Number = phoneNumber;
            }
            else if(CountryMetaDataHasCountryCode(countryMetaData))
            {
                CountryCode = countryMetaData.CountryCode;
                Number = cleanedPhoneNumber;
            }
            else
            {
                CountryCode = string.Empty;
                Number = cleanedPhoneNumber;
            }
        }

        /// <summary>   Country meta data has country code. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryMetaData">  Information describing the country meta data. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        private static bool CountryMetaDataHasCountryCode(CountryMetaData countryMetaData)
        {
            return !string.IsNullOrEmpty(countryMetaData.CountryCode);
        }

        /// <summary>   Phone number already has country code. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryMetaData">      Information describing the country meta data. </param>
        /// <param name="cleanedPhoneNumber">   The cleaned phone number. </param>
        /// <returns>   true if it succeeds, false if it fails. </returns>
        private static bool PhoneNumberAlreadyHasCountryCode(CountryMetaData countryMetaData, string cleanedPhoneNumber)
        {
            return countryMetaData.PhoneNumberDigitCount > 0 && countryMetaData.PhoneNumberDigitCount < cleanedPhoneNumber.Length;
        }

        /// <summary>   Initializes the phone number format number groups. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryMetaData">  Information describing the country meta data. </param>
        private void InitializePhoneNumberFormatNumberGroups(CountryMetaData countryMetaData)
        {
            var startIndex = 0;
            foreach(var groupCount in countryMetaData.PhoneNumberFormat)
            {
                var numberGroup = Number.Substring(startIndex, groupCount);
                startIndex += groupCount;
                NumberGroups.Add(numberGroup);
            }
        }

        /// <summary>   Gets or sets the country code. </summary>
        /// <value> country code. </value>
        public string CountryCode { get; set; }

        /// <summary>   Gets or sets the number groups. </summary>
        /// <value> number groups. </value>
        public List<string> NumberGroups { get; set; }

        /// <summary>   The phone number. </summary>
        /// <value> phone number. </value>
        public string Number { get; set; }

        /// <summary>   Returns a string that represents the current object. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <returns>   A string that represents the current object. </returns>
        public override string ToString()
        {
            var countryCode = FormatCountryCode(CountryCode);
            var formattedPhoneNumber = NumberGroups.Any()
                    ? AggregateNumberGroups(countryCode)
                    : DefaultFormatting(countryCode);
            return formattedPhoneNumber.Trim();
        }

        /// <summary>   Clean phone number. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="unformattedPhoneNumber">   The unformatted phone number. </param>
        /// <returns>   A string. </returns>
        private string CleanPhoneNumber(string unformattedPhoneNumber)
        {
            if (string.IsNullOrEmpty(unformattedPhoneNumber)) throw new ArgumentNullException("unformattedPhoneNumber");
            var replaceAnyNoneDigitPattern = new Regex(@"\D");
            var cleanedPhoneNumber = replaceAnyNoneDigitPattern.Replace(unformattedPhoneNumber, string.Empty);
            cleanedPhoneNumber = AddPlussSignIfItWasStripped(unformattedPhoneNumber, cleanedPhoneNumber);
            return cleanedPhoneNumber;
        }

        /// <summary>   Adds the pluss sign at the start of the phone number, 
        ///             if it was stripped while cleaning the phone number. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="phoneNumber">          The phone number. </param>
        /// <param name="strippedPhoneNumber">
        ///     The stripped phone number with no space or other chars,
        ///     only digits.
        /// </param>
        /// <returns>   A string. </returns>
        private string AddPlussSignIfItWasStripped(string phoneNumber, string strippedPhoneNumber)
        {
            return phoneNumber.StartsWith("+") ? string.Format("+{0}", strippedPhoneNumber) : strippedPhoneNumber;
        }

        /// <summary>   Default formatting. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryCode">  The country code. </param>
        /// <returns>   A string. </returns>
        private string DefaultFormatting(string countryCode)
        {
            if(string.IsNullOrEmpty(countryCode))
                if(Number.StartsWith("+"))
                {
                    CountryCode = FormatCountryCode(Number.Substring(0, 3));
                    Number = Number.Substring(3);
                }
                else if(Number.StartsWith("00"))
                {
                    CountryCode = FormatCountryCode(Number.Substring(0, 4));
                    Number = Number.Substring(4);
                }
            var numberParts = Number.SplitInParts(2);
            var number = string.Join(" ", numberParts);
            return string.IsNullOrEmpty(CountryCode)
                    ? number
                    : string.Format("{0} {1}", CountryCode, number);
        }

        /// <summary>   Format country code. </summary>
        /// <param name="countryCode"></param>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <returns>   The formatted country code. </returns>
        private string FormatCountryCode(string countryCode)
        {
            return string.IsNullOrEmpty(countryCode)
                    ? string.Empty
                    : countryCode.StartsWith("+")
                            ? countryCode
                            : countryCode.StartsWith("00")
                                    ? string.Format("+{0}", countryCode.Substring(2)) //TODO: not all country codes have only two digits...
                                    : string.Format("+{0}", countryCode);
        }

        /// <summary>   Aggregate number groups. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <param name="countryCode">  The country code. </param>
        /// <returns>   The phone number. </returns>
        private string AggregateNumberGroups(string countryCode)
        {
            return NumberGroups.Aggregate(countryCode, (current, next) => string.Format("{0} {1}", current, next));
        }
    }
}