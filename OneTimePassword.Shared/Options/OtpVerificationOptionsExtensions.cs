namespace OneTimePassword.Shared.Options;

public static class OtpVerificationOptionsExtensions
{
    public static OtpVerificationOptions UseInMemoryCache(this OtpVerificationOptions options)
    {
        options.IsInMemoryCache = true;
        return options;
    }
}