#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   A country meta data. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public class CountryMetaData
    {
        /// <summary>   Constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <param name="countryCode">              The country code. </param>
        /// <param name="phoneNumberDigitCount">    Number of phone number digits. </param>
        /// <param name="phoneNumberFormat">        The phone number format. </param>
        public CountryMetaData(string countryCode, string phoneNumberDigitCount, string phoneNumberFormat)
        {
            CountryCode = countryCode;
            PhoneNumberDigitCount = Convert.ToInt32(phoneNumberDigitCount);
            PhoneNumberFormat = new List<int>();
            if(string.IsNullOrEmpty(phoneNumberFormat) || !phoneNumberFormat.Contains(".")) return;
            PhoneNumberFormat.AddRange(phoneNumberFormat.Split('.').Select(s => Convert.ToInt32(s)));
        }

        /// <summary>   Default constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        public CountryMetaData()
        {
            CountryCode = string.Empty;
            PhoneNumberDigitCount = 0;
            PhoneNumberFormat = new List<int>();
        }

        /// <summary>   Gets or sets the country code. </summary>
        /// <value> country code. </value>
        public string CountryCode { get; set; }

        /// <summary>   Gets or sets the number of digits in phone number. </summary>
        /// <value> The number of digits in phone number. </value>
        public int PhoneNumberDigitCount { get; set; }

        /// <summary>   Gets or sets the phone number format. </summary>
        /// <value> The phone number format. </value>
        public List<int> PhoneNumberFormat { get; set; }
    }
}