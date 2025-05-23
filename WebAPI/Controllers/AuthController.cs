using WebAPI.Helpers;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string jwtKey;  // = "your-very-strong-secret-key-12345"; // Replace with your secret from appsettings


        public AuthController()
        {
            var builder = WebApplication.CreateBuilder();

            // Access configuration
            var config = builder.Configuration;
            jwtKey = config["JwtSettings:Secret"]!;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Dummy user validation
            if (model.Email == "aa@aa.com" && model.Password == "bb")
            {
                var token = JwtHelper.GenerateToken(model.Email, jwtKey);

                // Set JWT as HTTP-only cookie
                Response.Cookies.Append("jwtToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.Now.AddHours(1)
                });

                var token1 = Request.Cookies["jwtToken"];

                return Ok(new { message = "Login successful" });
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Delete the JWT cookie
            //Response.Cookies.Delete("jwtToken");
            Response.Cookies.Append("jwtToken", "", new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(-1), // Force expiry
                SameSite = SameSiteMode.None,
                Secure = true
            });


            return Ok(new { message = "Logged out successfully" });
        }

    }
}
