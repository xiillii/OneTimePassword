namespace OneTimePassword.Shared.Utils;

public struct GeneratorCharacters
{
    public StringsOfLetters Value;
    private readonly string? _sequence;

    public GeneratorCharacters(StringsOfLetters value)
    {
        Value = value;
        _sequence = default;
    }

    public GeneratorCharacters(string? sequence)
    {
        Value = default;
        _sequence = sequence;
    }

    public string? Sequence() => _sequence;

    public static implicit operator GeneratorCharacters(StringsOfLetters value) => new(value);

    public static implicit operator GeneratorCharacters(string? value) => new(value);
}