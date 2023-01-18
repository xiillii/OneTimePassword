using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using OneTimePassword.Contracts;
using OneTimePassword.Shared;
using OneTimePassword.Shared.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Options;

namespace OneTimePassword.Business
{
    public class OtpVerification : IOtpVerification
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IDistributedCache? _distributedCache;
        private readonly IMemoryCache? _memoryCache;
        private readonly IDataProtector _dataProtection;
        private readonly OtpVerificationOptions _options;

        private record IdPlain(string Id, string Plain);

        private string BaseOtpUrl =>
            $"{_httpContext.HttpContext?.Request.Scheme}://{_httpContext.HttpContext?.Request.Host}/{nameof(OtpVerification)}/";

        

        public OtpVerification(IDataProtectionProvider dataProtection, IHttpContextAccessor accessor,
            IDistributedCache? distributedCache = null, IMemoryCache? memoryCache = null,
            IOptions<OtpVerificationOptions>? options = null)
        {
            _dataProtection = dataProtection.CreateProtector("ZdNR4D-NpB9Fx-djDh2I");
            _httpContext = accessor;
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
            _options = options?.Value ?? new OtpVerificationOptions();
        }

        public OtpVia Generate(string id, OtpVerificationOptions options, out DateTime expire)
        {
            var plain = OtpVerificationExtension.Generate(options, out expire, out string hash);
            var url = string.Empty;

            if (options.IsInMemoryCache)
            {
                _memoryCache?.Set(Key(id), hash, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expire,
                    Priority = CacheItemPriority.High
                });
            }
            else
            {
                _distributedCache?.SetString(Key(id), hash, new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expire
                });
            }

            if (options.EnableUrl)
            {
                url = BaseOtpUrl + _dataProtection.Protect(JsonSerializer.Serialize(new IdPlain(id, plain)));
            }

            return new OtpVia(plain, url);
        }

        public OtpVia Generate(string id) => Generate(id, out _);

        public OtpVia Generate(string id, out DateTime expire) => Generate(id, _options, out expire);

        public OtpVia Generate(string id, OtpVerificationOptions options) => Generate(id, options, out _);

        

        public bool Scan(string id, string plain, OtpVerificationOptions options)
        {
            var hash = options.IsInMemoryCache ? _memoryCache?.Get<string>(Key(id)) : _distributedCache?.GetString(Key(id));

            if (hash is null)
            {
                return false;
            }

            if (OtpVerificationExtension.Scan(plain, hash, options))
            {
                if (options.IsInMemoryCache)
                {
                    _memoryCache?.Remove(Key(id));
                }
                else
                {
                    _distributedCache?.Remove(Key(id));
                }
                return true;
            }

            return false;
        }

        public bool Scan(string id, string plain) => Scan(id, plain, _options);


        public bool Scan(string id, string plain, int expire) => Scan(id, plain, new OtpVerificationOptions { Expire = expire });

        public bool Scan(string url) => TryUnprotectUrl(url, out var id, out var code) && Scan(id, code);


        private string Key(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Cannot be null or empty");
            }

            return $"{nameof(OtpVerification)}:{id}";
        }

        private bool TryUnprotectUrl(string key, out string id, out string plain)
        {
            id = plain = string.Empty;

            try
            {
                var data = _dataProtection.Unprotect(key);

                var obj = JsonSerializer.Deserialize<IdPlain>(data);
                if (obj != null)
                {
                    id = obj.Id;
                    plain = obj.Plain;
                }

                return true;
            }
            catch (Exception e) when (e is CryptographicException or RuntimeBinderException)
            {
                return false;
            }
        }
    }
}
