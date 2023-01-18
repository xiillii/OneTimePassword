using System.Security.Cryptography;
using System.Text;

namespace OneTimePassword.Shared.Utils;

public class RandomString
{
    public static string Generate(StringsOfLetters gCharacters = StringsOfLetters.NumberAndAlphabet) =>
        Generate(new GeneratorOption(-1, gCharacters));

    public static string Generate(string characters) => Generate(new GeneratorOption(-1, characters));

    public static string
        Generate(int min, int max, StringsOfLetters gCharacters = StringsOfLetters.NumberAndAlphabet) =>
        Generate(new GeneratorOption(new GeneratorSize(min, max), gCharacters));

    public static string Generate(int size, StringsOfLetters gCharacters = StringsOfLetters.NumberAndAlphabet) =>
        Generate(new GeneratorOption(size, gCharacters));

    public static string Generate(int size, string characters) =>
        Generate(new GeneratorOption(size, new GeneratorCharacters(characters)));

    public static string Generate(GeneratorOption option)
    {
        char[]? range = default;

        if (option.Characters.Sequence() == default)
        {
            range = GeneratorOption.GetCharacters(option.Characters.Value)?.ToCharArray();
        }

        if (option.Characters.Value == default)
        {
            range = option.Characters.Sequence()?.ToCharArray();
        }

        if (option.Size.Max() == option.Size)
        {
            option.Size = new Random().Next(option.Size, option.Size.Max());
        }

        if (option.Size < 0)
        {
            option.Size = new Random().Next(4, 16);
        }

        var data = new byte[4 * option.Size];

        using var crypto = RandomNumberGenerator.Create();
        crypto.GetBytes(data);

        var result = new StringBuilder(option.Size);

        if (range != null)
        {
            for (var i = 0; i < option.Size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd * range.Length;
                result.Append(range[idx]);
            }
        }

        return result.ToString();
    }
}