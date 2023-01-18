using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneTimePassword.Contracts;
using OneTimePassword.Shared;
using OneTimePassword.Shared.Options;

namespace OneTimePassword.Business
{
    internal class OtpVerification : IOtpVerification
    {
        public OtpVia Generate(string id)
        {
            throw new NotImplementedException();
        }

        public OtpVia Generate(string id, out DateTimeOffset expire)
        {
            throw new NotImplementedException();
        }

        public OtpVia Generate(string id, OtpVerificationOptions options)
        {
            throw new NotImplementedException();
        }

        public OtpVia Generate(string id, OtpVerificationOptions options, out DateTimeOffset expire)
        {
            throw new NotImplementedException();
        }

        public bool Scan(string id, string plain)
        {
            throw new NotImplementedException();
        }

        public bool Scan(string id, string plain, OtpVerificationOptions options)
        {
            throw new NotImplementedException();
        }

        public bool Scan(string id, string plain, int expire)
        {
            throw new NotImplementedException();
        }

        public bool Scan(string url)
        {
            throw new NotImplementedException();
        }
    }
}
