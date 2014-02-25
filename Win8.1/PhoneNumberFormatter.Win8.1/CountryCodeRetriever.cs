#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   The country code retriever. </summary>
    /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
    public static class CountryCodeRetriever
    {
        /// <summary>   The country codes. </summary>
        private static readonly IDictionary<string, CountryMetaData> CountryCodes;

        /// <summary>   true if this object has been initialized. </summary>
        private static bool _hasBeenInitialized;

        /// <summary>   Default constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        static CountryCodeRetriever()
        {
            CountryCodes = new Dictionary<string, CountryMetaData>();
            _hasBeenInitialized = false;
        }

        /// <summary>   Initializes the Country Code Retriever. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        public static async Task Initialize()
        {
            if(_hasBeenInitialized) return;

            var readCountryCodesFile = await ReadTextFile("CountryCodes.txt");
            foreach(var codeAndContryAliases in readCountryCodesFile.Where(line => !line.StartsWith("#") || string.IsNullOrEmpty(line)).Select(line => line.Split('|')))
            {
                var countryMetaData = new CountryMetaData(codeAndContryAliases[0], codeAndContryAliases[1], codeAndContryAliases[2]);
                for(var i = 3; i < codeAndContryAliases.Length; i++)
                {
                    CountryCodes.Add(codeAndContryAliases[i].ToLower(), countryMetaData);
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
        private static async Task<IList<string>> ReadTextFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            var folder = Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync(path);
            return await FileIO.ReadLinesAsync(file);
        }

        public static CountryMetaData GetCountryMetaData(string country)
        {
            if (string.IsNullOrEmpty(country)) throw new ArgumentNullException("country");
            country = country.ToLower();
            if(CountryCodes.ContainsKey(country))
                return CountryCodes[country];
            throw new Exception("The country code dictionary does not contain a country code for the supplied country.");
        }
    }
}