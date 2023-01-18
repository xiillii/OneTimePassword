using OneTimePassword.Business.Options;

namespace OneTimePassword.Shared.Contracts;

public interface IOtpVerification
{
    OtpVia Generate(string id);
    OtpVia Generate(string id, out DateTimeOffset expire);
    OtpVia Generate(string id, OtpVerificationOptions options);
    OtpVia Generate(string id, OtpVerificationOptions options, out DateTimeOffset expire);
    bool Scan(string id, string plain);
    bool Scan(string id, string plain, OtpVerificationOptions options);
    bool Scan(string id, string plain, int expire);
    bool Scan(string url);
}