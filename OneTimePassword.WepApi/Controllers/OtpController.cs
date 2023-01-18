using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneTimePassword.Contracts;
using OneTimePassword.Shared.Options;
using OneTimePassword.Shared.Utils;


namespace OneTimePassword.WepApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OtpController : ControllerBase
{
    private readonly IOtpVerification _otp;
    private const int MinutesToExpire = 3;

    public record User(string Fullname)
    {
        public int Id { get; set; }
        public bool IsVerify { get; set; }

        public override string ToString() => $"Id: {Id}, Fullname: {Fullname}, IsVerify: {IsVerify}";
    }

    private static List<User> Users = new List<User>();

    public OtpController(IOtpVerification otp) => _otp = otp;

    [HttpGet]
    public IActionResult GetUsers() => Ok(Users);

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        user.IsVerify = default;
        user.Id = int.Parse(RandomString.Generate(2, StringsOfLetters.Number));
        Users.Add(user);

        var code = _otp.Generate(user.Id.ToString(), new OtpVerificationOptions { Expire = MinutesToExpire, IsInMemoryCache = true},
            expire: out var expireDate);

        return Ok(new { user, via = code, expire = expireDate });
    }

    [HttpPost("{id}")]
    public IActionResult RefreshUser([FromRoute] int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return NotFound(new { message = "User not found" });
        }

        user.IsVerify = false;
        var code = _otp.Generate(user.Id.ToString(), new OtpVerificationOptions { Expire = MinutesToExpire,IsInMemoryCache = true},
            expire: out var expireDate);
        return Ok(new { user, via = code, expire = expireDate });
    }

    [HttpPost]
    public IActionResult VerifyUser([Range(0, 99)] int userId, [Required] string code)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        if (user is null)
        {
            return NotFound(new { message = "User not found" });
        }

        if (user.IsVerify)
        {
            return BadRequest(new { message = "User is already verified" });
        }

        if (_otp.Scan(userId.ToString(), code, new OtpVerificationOptions { Expire = MinutesToExpire, IsInMemoryCache = true }))
        {
            user.IsVerify = true;
            return Ok(new { user, message = $"User successful confirmed his OTP code {code}" });
        }

        return BadRequest(new { user, message = "User enter wrong code or expired" });
    }
}