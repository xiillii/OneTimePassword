using System.Security.Cryptography;

namespace OneTimePassword.Shared.Utils;

public abstract class SecureHasher
{
    private const int SaltSize = 16;

    protected static string Hash(string plain, int hashSize, int iterations)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt;
        rng.GetBytes(salt = new byte[SaltSize]);

        using var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations);
        var hash = pbkdf2.GetBytes(hashSize);
        var hashBytes = new byte[SaltSize + hashSize];

        // Combine salt and hash
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, hashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);

        // Format hash with extra information
        return $"{iterations}${base64Hash}";
    }

    protected static bool Verify(string plain, string? hashedPlain, int hashSize = 20)
    {
        var splittedHashString = hashedPlain.Split("$");
        _ = int.TryParse(splittedHashString[0], out var iterations);
        var base64Hash = splittedHashString[1];

        var hashBytes = Convert.FromBase64String(base64Hash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations);
        var hash = pbkdf2.GetBytes(hashSize);

        for (var i = 0; i < hashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}