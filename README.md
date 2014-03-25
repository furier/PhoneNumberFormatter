**PhoneNumberFormatter**
====================

A library for formatting phone numbers.

NuGet
-----

    Install-Package PhoneNumberFormatter
    
Configurations
--------------

    ##############################################
    #
    # A: Country Code
    # B: Number of digits in phone numbers
    # C: Phone number format to be returned
    # D: Aliases
    #
    # Format => A|B|C|D....|D
    #
    ##############################################
    
    #Norwegian phone number country code metadata.
    47|8|3.2.3|norge|norway|nor|no
    
    #Swedish phone number country code metadata.
    46|9|3.3.3|sverige|sweden|swe|se
    
Comments starts with `#` and will be ignored by the configuration parser.
New countries can be added by following the convention described above or at the top in the `CountryCodes.txt` file.

Some countries have a range of digits count in their phone numbers, which is currently not supported and would have to be added.

Examples
--------

    private async void Test(dynamic phoneNumber)
    {
        var formattedPhoneNumber = await PhoneNumberFormatter.PhoneNumberFormatter
                                                             .FormatAsync(phoneNumber.Number, phoneNumber.Country);
        Console.WriteLine("Unformatted phone number: <{0}> with country <{1}> has been formatted to <{2}>", phoneNumber.Number, phoneNumber.Country, formattedPhoneNumber);
    }

    await PhoneNumberFormatter.CountryMetaDataRetriever.InitializeAsync();

    var phoneNumbers = new List<dynamic>
                       {
                               new {Number = "+4712345678", Country = ""},
                               new {Number = "+47 1 23 45 67 8", Country = ""},
                               new {Number = "12345678", Country = "Norway"},
                               new {Number = "1 23 45 67 8", Country = "No"},
                               new {Number = "1-2-3-4-5-6-7-8", Country = "Nor"},
                               new {Number = "1.2.3.4.5.6.7.8", Country = ""}
                       };
                       
    foreach(var phoneNumber in phoneNumbers)
    {
        Test(phoneNumber);
    }
    
This should output the following if default configurations are active.
    
    Unformatted phone number: <+4712345678> with country <> has been formatted to <+47 123 45 678>
    Unformatted phone number: <+47 1 23 45 67 8> with country <> has been formatted to <+47 123 45 678>
    Unformatted phone number: <12345678> with country <Norway> has been formatted to <+47 123 45 678>
    Unformatted phone number: <1 23 45 67 8> with country <No> has been formatted to <+47 123 45 678>
    Unformatted phone number: <1-2-3-4-5-6-7-8> with country <Nor> has been formatted to <+47 123 45 678>
    Unformatted phone number: <1.2.3.4.5.6.7.8> with country <> has been formatted to <12 34 56 78>
    
    
