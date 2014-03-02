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
