namespace OneTimePassword.Shared.Utils;

public enum StringsOfLetters : short
{
    /// <summary>
    /// <![CDATA[ abcdefghijklmnopqrstuvwxyz  ]]> 
    /// </summary>
    Lower,

    /// <summary>
    /// <![CDATA[ ABCDEFGHIJKLMNOPQRSTUVWXYZ  ]]> 
    /// </summary>
    Upper,

    /// <summary>
    /// <![CDATA[ 0123456789  ]]>
    /// </summary>
    Number,

    /// <summary>
    /// <![CDATA[ ~!@#$%^&*()_-+=/\|. ]]>
    /// </summary>
    Symbol,

    /// <summary>
    /// <![CDATA[ AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz  ]]> 
    /// </summary>
    Alphabet,

    /// <summary>
    /// <![CDATA[ 0123456789abcdefghijklmnopqrstuvwxyz  ]]> 
    /// </summary>
    NumberAndLower,

    /// <summary>
    /// <![CDATA[ 0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ  ]]> 
    /// </summary>
    NumberAndUpper,

    /// <summary>
    /// <![CDATA[ ~!@#$%^&*()_-+=/\|.0123456789  ]]> 
    /// </summary>
    SymbolAndNumber,

    /// <summary>
    /// <![CDATA[ ~!@#$%^&*()_-+=/\|.0123456789abcdefghijklmnopqrstuvwxyz  ]]> 
    /// </summary>
    SymbolAndNumberAndLower,

    /// <summary>
    /// <![CDATA[ ~!@#$%^&*()_-+=/\|.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ  ]]> 
    /// </summary>
    SymbolAndNumberAndUpper,

    /// <summary>
    /// <![CDATA[ 0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz  ]]> 
    /// </summary>
    NumberAndAlphabet,

    /// <summary>
    /// <![CDATA[ ~!@#$%^&*()_-+=/\|.0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz  ]]> 
    /// </summary>
    SymbolAndNumberAndAlphabet,
}