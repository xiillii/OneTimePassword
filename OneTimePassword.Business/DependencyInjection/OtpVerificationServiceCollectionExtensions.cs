using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OneTimePassword.Contracts;
using OneTimePassword.Shared.Options;

namespace OneTimePassword.Business.DependencyInjection;

public static class OtpVerificationServiceCollectionExtensions
{
    public static IServiceCollection AddOtpVerification(this IServiceCollection services,
        Action<OtpVerificationOptions> options = default)
    {
        services.AddDataProtection();
        services.AddHttpContextAccessor();
        services.Add(ServiceDescriptor.Singleton<IOtpVerification, OtpVerification>());

        services.Configure(options ??= (o => { }));
        OtpVerificationOptions opts = new();
        options(opts);

        if (opts.IsInMemoryCache)
        {
            return services.AddMemoryCache();
        }

        return services.AddStackExchangeRedisCache(op => op.Configuration = "localhost");
    }
}

public static class EndpointRouteBuilderExtensions
{
    public static void MapOtpVerification(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet($"/{nameof(OtpVerification)}/{{*key}}", (string key) =>
        {
            var otp = endpoints.ServiceProvider.GetRequiredService<IOtpVerification>();
            return otp.Scan(key) ? "Verify" : "Un-Verify";
        });
    }
}