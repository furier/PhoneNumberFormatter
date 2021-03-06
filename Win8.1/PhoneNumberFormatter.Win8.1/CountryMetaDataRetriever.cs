﻿#region File Header

// // ***********************************************************************
// // Author           : Sander Struijk
// // ***********************************************************************

#endregion

#region Using statements

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

#endregion

namespace PhoneNumberFormatter
{
    /// <summary>   A country meta data retriever. </summary>
    /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
    public static class CountryMetaDataRetriever
    {
        /// <summary>   The countries meta data. </summary>
        private static readonly IDictionary<string, CountryMetaData> CountryMetaDatas;

        /// <summary>   true if this object has been initialized. </summary>
        private static bool _hasBeenInitialized;

        /// <summary>   true to in progress. </summary>
        private static bool _inProgress;

        /// <summary>   Default constructor. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        static CountryMetaDataRetriever()
        {
            CountryMetaDatas = new Dictionary<string, CountryMetaData>();
            _inProgress = false;
            _hasBeenInitialized = false;
        }

        /// <summary>   Initializes the country meta data retriever. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        public static async Task InitializeAsync()
        {
            if(_hasBeenInitialized || _inProgress) return;

            _inProgress = true;
            await ReadTextFileAsync("CountryCodes.txt").ContinueWith(task =>
            {
                foreach(var line in task.Result)
                {
                    if(line.StartsWith("#") || string.IsNullOrEmpty(line)) continue;

                    var countryData = line.Split('|');
                    var countryMetaData = new CountryMetaData(countryData[0], countryData[1], countryData[2]);
                    for(var i = 3; i < countryData.Length; i++)
                    {
                        var key = countryData[i].ToLower();
                        if(CountryMetaDatas.ContainsKey(key)) continue;
                        CountryMetaDatas.Add(key, countryMetaData);
                    }
                }
            }).ContinueWith(task =>
            {
                _hasBeenInitialized = true;
                _inProgress = false;
            });
        }

        /// <summary>   Reads text file. </summary>
        /// <remarks>   Sander.struijk, 25.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="path"> Full pathname of the file. </param>
        /// <returns>   The text file. </returns>
        private static async Task<IList<string>> ReadTextFileAsync(string path)
        {
            if(string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            var folder = Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync(path);
            return await FileIO.ReadLinesAsync(file);
        }

        /// <summary>   Gets country meta data. </summary>
        /// <remarks>   Sander.struijk, 26.02.2014. </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="Exception">                Thrown when no country meta data can be found for the requested country. </exception>
        /// <param name="country">  The country name. </param>
        /// <returns>   The country meta data. </returns>
        public static CountryMetaData GetCountryMetaData(string country)
        {
            if(!_hasBeenInitialized && !_inProgress) throw new Exception("Country meta data retriever has not been initialized.");
            if(!_hasBeenInitialized && _inProgress) throw new Exception("Country meta data retriever is initializing, initialization has to finish before country meta data can be retrieved.");
            if(string.IsNullOrEmpty(country)) throw new ArgumentNullException("country");
            country = country.ToLower();
            if(CountryMetaDatas.ContainsKey(country))
                return CountryMetaDatas[country];
            throw new Exception("The country code dictionary does not contain a country code for the supplied country.");
        }
    }
}