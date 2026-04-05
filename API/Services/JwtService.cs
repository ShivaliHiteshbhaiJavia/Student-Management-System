using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;

        public JwtService()
        {
            // Hardcoded for simplicity; ideally get these from IConfiguration
            _secretKey = "ThisIsAVeryStrongSecretKeyWith32Chars!"; // Must be at least 16 chars
            _issuer = "StudentAPI";
            _audience = "StudentAPI";
            _expiryMinutes = 60;
        }

        public string Generate(string email)
        {
            try
            {
                Console.WriteLine("JwtService: Generating token for email: " + email);

                // Create security key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

                // Create signing credentials
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create claims
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                };

                // Create token
                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                    signingCredentials: credentials
                );

                // Write token as string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                Console.WriteLine("JwtService: Token generated successfully: " + tokenString);
                return tokenString;
            }
            catch (Exception ex)
            {
                Console.WriteLine("JwtService: Exception while generating token: " + ex.Message);
                throw; // Re-throw so controller can handle 500
            }
        }
    }
}