using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneNumberFormatter
{
    /// <summary>   A country meta data. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public class CountryMetaData
    {
        public string CountryCode { get; set; }

        public int PhoneNumberDigitCount { get; set; }

        public List<int> PhoneNumberFormat { get; set; }

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
            if (string.IsNullOrEmpty(phoneNumberFormat) || !phoneNumberFormat.Contains(".")) return;
            PhoneNumberFormat.AddRange(phoneNumberFormat.Split('.').Select(s => Convert.ToInt32((string)s)));
        }

        /// <summary>   Default constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        public CountryMetaData()
        {
            PhoneNumberFormat = new List<int>();
        }
    }
}