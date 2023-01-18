namespace OneTimePassword.Shared.Utils;

public struct GeneratorSize
{
    public int Value;
    private readonly int _max;

    public GeneratorSize(int value)
    {
        Value = value;
        _max = value;
    }

    public GeneratorSize(int min, int max)
    {
        Value = min;
        _max = max;
    }

    public int Max() => _max;

    public static implicit operator GeneratorSize(int value) => new(value);

    public static implicit operator int(GeneratorSize value) => value.Value;
}