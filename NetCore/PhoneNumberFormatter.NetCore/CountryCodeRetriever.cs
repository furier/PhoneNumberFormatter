#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   The country code retriever. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public static class CountryCodeRetriever
    {
        /// <summary>   The country codes. </summary>
        private static readonly IDictionary<string, string> CountryCodes;

        /// <summary>   true if this object has been initialized. </summary>
        private static bool _hasBeenInitialized;

        /// <summary>   Default constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        static CountryCodeRetriever()
        {
            CountryCodes = new Dictionary<string, string>();
            _hasBeenInitialized = false;
        }

        /// <summary>   Initializes the Country Code Retriever. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        public static void Initialize()
        {
            if(_hasBeenInitialized) return;

            var readCountryCodesFile = ReadTextFile("CountryCodes.txt");
            foreach (var codeAndContryAliases in readCountryCodesFile.Where(line => !line.StartsWith("#") || string.IsNullOrEmpty(line)).Select(line => line.Split('|')))
            {
                var countryCode = codeAndContryAliases[0];
                for(var i = 1; i < codeAndContryAliases.Length; i++)
                {
                    CountryCodes.Add(codeAndContryAliases[i], countryCode);
                }
            }
            _hasBeenInitialized = true;
        }

        /// <summary>   Reads text file. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="path"> Full pathname of the file. </param>
        /// <returns>   The text file. </returns>
        private static IEnumerable<string> ReadTextFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            return File.ReadLines(path);
        }

        /// <summary>   Gets country code by country name. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="Exception">                Thrown when an exception error condition occurs. </exception>
        /// <param name="country">  The country. </param>
        /// <returns>   The country code. </returns>
        public static string GetCountryCode(string country)
        {
            if (string.IsNullOrEmpty(country)) throw new ArgumentNullException("country");
            country = country.ToLower();
            if(CountryCodes.ContainsKey(country))
                return CountryCodes[country];
            throw new Exception("The country code dictionary does not contain a country code for the supplied country.");
        }
    }
}