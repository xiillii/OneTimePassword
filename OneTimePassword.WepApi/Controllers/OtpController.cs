using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTimePassword.Shared.Contracts;

namespace OneTimePassword.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpVerification _otp;

        public OtpController(IOtpVerification otp) => _otp = otp;


    }
}
