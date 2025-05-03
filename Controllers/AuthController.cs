using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EduApplication.Models;
using EduApplication.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Net.Mail;
using BCrypt.Net; 

namespace EduApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly EduApplicationContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly SmtpSettings _smtpSettings;

        public AuthController(EduApplicationContext context, IOptions<JwtSettings> jwtSettings, IOptions<SmtpSettings> smtpSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _context.Parents.AnyAsync(p => p.email == model.Email))
                return BadRequest("Email already exists.");

            var parent = new Parents
            {
                name = model.Name,
                email = model.Email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.email == model.Email);
            if (parent == null || !BCrypt.Net.BCrypt.Verify(model.Password, parent.password_hash))
                return Unauthorized("Invalid email or password.");

            var token = GenerateJwtToken(parent);

            return Ok(new { Token = token });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.email == model.Email);
            if (parent == null)
                return BadRequest("Email not found.");

            // Generate OTP
            var otp = GenerateOtp();
            parent.otp = otp;
            parent.otp_expiration_time = DateTime.UtcNow.AddMinutes(10);
            await _context.SaveChangesAsync();

            try
            {
                SendEmail(parent.email, "Password Reset OTP", $"Your OTP code is: {otp}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send OTP email: {ex.Message}");
            }

            return Ok("OTP sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.email == model.Email && p.otp == model.OtpCode);
            if (parent == null || parent.otp_expiration_time < DateTime.UtcNow)
                return BadRequest("Invalid or expired OTP.");

            parent.password_hash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            parent.otp = null;
            parent.otp_expiration_time = null;

            await _context.SaveChangesAsync();

            return Ok("Password reset successfully.");
        }

        private string GenerateJwtToken(Parents parent)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, parent.parent_id.ToString()),
                    new Claim(ClaimTypes.Email, parent.email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpSettings.Server)
            {
                Port = 587,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
