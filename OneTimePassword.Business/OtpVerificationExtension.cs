using OneTimePassword.Shared.Options;
using OneTimePassword.Shared.Utils;

namespace OneTimePassword.Business;

public class OtpVerificationExtension : SecureHasher
{
    public static string Generate(OtpVerificationOptions options, out DateTime expire, out string hash)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (options.Size <= 0)
        {
            throw new ArgumentException("Can't be 0 or low", nameof(options.Size));
        }

        if (options.Length <= 0)
        {
            throw new ArgumentException("Can't be 0 or low", nameof(options.Length));
        }

        if (options.Expire <= 0)
        {
            throw new ArgumentException("Can't be 0 or low", nameof(options.Expire));
        }

        if (options.Iterations <= 0)
        {
            throw new ArgumentException("Can't be 0 or low", nameof(options.Iterations));
        }

        var dateNow = DateTime.Now;
        var plain = RandomString.Generate(options.Size, StringsOfLetters.Number);

        expire = dateNow.AddSeconds(59 - dateNow.Second).AddMinutes(options.Expire - 1);

        hash = Hash(plain + dateNow.ToString("yyyyMMddHHmm"), options.Length, options.Iterations);

        return plain;
    }

    public static string Generate(out DateTime expire, out string hash) =>
        Generate(new OtpVerificationOptions(), out expire, out hash);

    public static string Generate(OtpVerificationOptions options, out string hash) =>
        Generate(options, out _, out hash);

    public static string Generate(out string hash) => Generate(new OtpVerificationOptions(), out hash);

    public static string Generate(OtpVerificationOptions options, out DateTime expire) =>
        Generate(options, out expire, out _);

    public static string Generate(out DateTime expire) =>
        Generate(new OtpVerificationOptions(), out expire, out _);

    public static string Generate(OtpVerificationOptions options) => Generate(options, out _, out _);

    public static string Generate() => Generate(new OtpVerificationOptions());

    public static bool Scan(string plain, string? hash, OtpVerificationOptions options)
    {
        if (string.IsNullOrEmpty(plain))
        {
            throw new ArgumentNullException(nameof(plain));
        }

        if (string.IsNullOrEmpty(hash))
        {
            throw new ArgumentNullException(nameof(hash));
        }

        bool verify;
        var begin = 0;

        do
        {
            verify = Verify(plain + DateTime.Now.AddMinutes(-begin).ToString("yyyyMMddHHmm"), hash);
            begin++;
        } while (verify == false && begin <= options.Expire);

        return verify;
    }

    public static bool Scan(string plain, string? hash, int expire) =>
        Scan(plain, hash, new OtpVerificationOptions { Expire = expire });

    public static bool Scan(string plain, string? hash) => Scan(plain, hash, new OtpVerificationOptions());
}