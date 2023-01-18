namespace OneTimePassword.Shared;

public class OtpVia
{
    public string Code { get; set; }
    public string? Url { get; set; }

    public OtpVia(string code, string? url = default)
    {
        Code = code;
        Url = url;
    }

    public override string ToString() => $"Code: {Code}, Url: {Url}";
}