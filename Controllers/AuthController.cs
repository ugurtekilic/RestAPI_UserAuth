using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthApi.Data;
using UserAuthApi.Models;

namespace UserAuthApi.Controllers
{
    // Bu API'ye erişmek için kullanılacak adres: api/auth
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        // "Constructor Injection": Yukarıda hazırladığımız veritabanı köprüsünü içeri alıyoruz
        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));

            // EKSİK OLAN SATIR BURASI:
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds // Artık buradaki hata düzelecek
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // --- KAYIT OLMA İŞLEMİ (POST api/auth/register) ---
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            
            var newUser = new User()
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kayıt başarılı!" });
        }

        // --- GİRİŞ YAPMA İŞLEMİ (POST api/auth/login) ---
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) { return Unauthorized(new { message = "Kullanıcı bulunamadı!" });};

            if(user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.Now) 
            {
                var kalanSure = (user.LockoutEnd.Value - DateTime.Now).Minutes;
                return BadRequest(new { message = $"Çok fazla hatalı deneme! Hesabınız {kalanSure + 1} dakika daha kilitli." });
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

            if (!isPasswordValid) 
            { 
                 user.AccessFailedCount++;
                if (user.AccessFailedCount > 5)
                {
                    user.LockoutEnd = DateTime.Now.AddMinutes(1);
                    await _context.SaveChangesAsync();
                    return BadRequest(new { message = "Çok Fazla Hatalı Giriş Denendi! Hesabınız 1 dakika askıya alındı." });
                }
                await _context.SaveChangesAsync();
                int kalanHak = 5 - user.AccessFailedCount;
                return Unauthorized(new { message = $"Hatalı şifre! {kalanHak} deneme hakkınız kaldı." });
            }

            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            await _context.SaveChangesAsync();

            string token = CreateToken(user);
            return Ok(new { message = "Giriş Başarılı!", token = token });

        }

        [HttpGet("gizli-bilgi"), Authorize] // Sadece giriş yapanlar görebilir
        public IActionResult GetSecretData()
        {
            return Ok("Bu bilgiyi sadece geçerli bir Token'ı olanlar görebilir!");
        }
    }
}