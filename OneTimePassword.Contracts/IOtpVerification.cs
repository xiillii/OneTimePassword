using OneTimePassword.Shared;
using OneTimePassword.Shared.Options;

namespace OneTimePassword.Contracts;

public interface IOtpVerification
{
    OtpVia Generate(string id);
    OtpVia Generate(string id, out DateTime expire);
    OtpVia Generate(string id, OtpVerificationOptions options);
    OtpVia Generate(string id, OtpVerificationOptions options, out DateTime expire);
    bool Scan(string id, string plain);
    bool Scan(string id, string plain, OtpVerificationOptions options);
    bool Scan(string id, string plain, int expire);
    bool Scan(string url);
}