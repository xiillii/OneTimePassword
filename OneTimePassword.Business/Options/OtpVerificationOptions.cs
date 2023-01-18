namespace OneTimePassword.Business.Options;

public class OtpVerificationOptions
{
    /// <summary>
    /// Enable or disable in-memory cache
    /// </summary>
    /// <para>Default value = false. Redis will handle caching</para>
    public bool IsInMemoryCache { get; set; } = false;

    /// <summary>
    /// Active to generate URL to verify code with Id OTP
    /// </summary>
    public bool EnableUrl { get; set; } = true;

    /// <summary>
    /// Number of complexity of rounds hashing
    /// <para>Default value = 1</para>
    /// </summary>
    public int Iteration { get; set; } = 1;

    /// <summary>
    /// Number of char code generator to hash
    /// </summary>
    /// <para>Default value = 6</para>
    public int Size { get; set; } = 6;

    /// <summary>
    /// Length hash result
    /// </summary>
    /// <para>Default value = 6</para>
    public int Length { get; set; } = 20;

    /// <summary>
    /// Measure after minutes
    /// <para>Default value = 2</para>
    /// </summary>
    public int Expire { get; set; } = 2;
}