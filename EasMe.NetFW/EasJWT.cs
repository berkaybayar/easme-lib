using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasMe
{
    /// <summary>
    /// JWT Authentication helper, generating and reading tokens.
    /// </summary>
    public class EasJWT
    {
        private static string _secretString;
        private static byte[] _secretBytes;
        private static string _issuer;
        private static string _audience;
        public EasJWT(string secret, string issuer = "", string audience = "")
        {
            _secretString = secret;
            _secretBytes = Encoding.ASCII.GetBytes(_secretString);
            _issuer = issuer;
            _audience = audience;
        }
        /// <summary>
        /// Generates a JWT token by ClaimsIdentity.
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        public string GenerateJWTToken(ClaimsIdentity claimsIdentity, int expireMinutes)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.Now.AddMinutes(expireMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretBytes), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _issuer,
                    Audience = _audience
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Validates JWT token and returns ClaimsPrincipal.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validateIssuer"></param>
        /// <param name="validateAudience"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateJWTToken(string token,bool validateIssuer = false, bool validateAudience = false)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secretBytes),
                    ValidateIssuer = validateIssuer,
                    ValidateAudience = validateAudience
                };
                var claims = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                if (claims != null)
                {
                    return claims;
                }
                return new ClaimsPrincipal();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
