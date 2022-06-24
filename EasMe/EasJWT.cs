using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasMe
{
    /// <summary>
    /// JWT Authentication helper, generating and reading tokens.
    /// </summary>
    public static class EasJWT
    {
        private static string _secretString;
        private static byte[] _secretBytes;
        private static string _issuer;
        private static string _audience;

        /// <summary>
        /// Loads your secret key, issuer, audience. Call this method in your application startup.
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        public static void LoadConfiguration(string secret, string issuer = "", string audience = "")
        {
            _secretString = secret;
            _secretBytes = Encoding.ASCII.GetBytes(_secretString);
            _issuer = issuer;
            _audience = audience;
        }

        private static void CheckConfig()
        {
            if (_secretBytes.IsNullOrEmpty()) throw new EasException(Error.NULL_REFERENCE, "EasJWT configuartion error, secret not loaded.");
            if (_secretString.IsNullOrEmpty()) throw new EasException(Error.NULL_REFERENCE, "EasJWT configuartion error, secret not loaded.");
            if (_issuer.IsNullOrEmpty()) throw new EasException(Error.NULL_REFERENCE, "EasJWT configuartion error, issuer not loaded.");
            if (_audience.IsNullOrEmpty()) throw new EasException(Error.NULL_REFERENCE, "EasJWT configuartion error, audience not loaded.");
        }
        /// <summary>
        /// Generates a JWT token by ClaimsIdentity.
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        public static string GenerateJWTToken(ClaimsIdentity claimsIdentity, int expireMinutes)
        {
            CheckConfig();
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
        public static ClaimsPrincipal ValidateJWTToken(string token, bool validateIssuer = false, bool validateAudience = false)
        {
            CheckConfig();
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
