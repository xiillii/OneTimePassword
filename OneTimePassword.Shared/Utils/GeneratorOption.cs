namespace OneTimePassword.Shared.Utils;

public class GeneratorOption
{
    public GeneratorSize Size { get; set; }
    public GeneratorCharacters Characters { get; set; }

    public GeneratorOption()
    {

    }

    public GeneratorOption(GeneratorSize size, GeneratorCharacters characters)
    {
        Size = size;
        Characters = characters;
    }

    public static string? GetCharacters(StringsOfLetters gCharacter)
    {
        return gCharacter switch
        {
            StringsOfLetters.Lower => "abcdefghijklmnopqrstuvwxyz",
            StringsOfLetters.Upper => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            StringsOfLetters.Number => "0123456789",
            StringsOfLetters.Symbol => @"~!@#$%^&*()_-+=/\|.",
            StringsOfLetters.Alphabet => "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz",
            StringsOfLetters.NumberAndLower => "0123456789abcdefghijklmnopqrstuvwxyz",
            StringsOfLetters.NumberAndUpper => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            StringsOfLetters.SymbolAndNumber => @"~!@#$%^&*()_-+=/\|.0123456789",
            StringsOfLetters.SymbolAndNumberAndLower => @"~!@#$%^&*()_-+=/\|.0123456789abcdefghijklmnopqrstuvwxyz",
            StringsOfLetters.SymbolAndNumberAndUpper => @"~!@#$%^&*()_-+=/\|.0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            StringsOfLetters.NumberAndAlphabet => "0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz",
            StringsOfLetters.SymbolAndNumberAndAlphabet =>
                @"~!@#$%^&*()_-+=/\|.0123456789AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz",
            _ => default
        };
    }
}