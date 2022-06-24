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
        
        private readonly static JwtSecurityTokenHandler TokenHandler = new ();
        private static bool ValidateIssuer { get; set; } = false;
        private static bool ValidateAudience { get; set; } = false;
        private static string? Issuer { get; set; }
        private static string? Audience { get; set; }
        private static byte[]? Secret { get; set; }
        
        /// <summary>
        /// Loads your secret key, issuer, audience. Call this method in your application startup.
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        public static void LoadConfiguration(string secret, string? issuer = null, string? audience = null)
        {
            Issuer = issuer;
            Audience = audience;
            if (!string.IsNullOrEmpty(Issuer))
            {
                ValidateIssuer = true;
            }
            if (!string.IsNullOrEmpty(Audience))
            {
                ValidateAudience = true;
            }
            Secret = Encoding.ASCII.GetBytes(secret);
        }

        private static void CheckSecret()
        {
            if (Secret.IsNullOrEmpty()) throw new EasException(Error.NULL_REFERENCE, "EasJWT configuartion error, secret not loaded.");
        }
        /// <summary>
        /// Generates a JWT token by ClaimsIdentity.
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        public static string GenerateJWTToken(ClaimsIdentity claimsIdentity, int expireMinutes)
        {
            CheckSecret();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Expires = DateTime.Now.AddMinutes(expireMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = Issuer,
                    Audience = Audience
                    
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_CREATE, "Could not create JWT token.", ex);
            }

        }

        /// <summary>
        /// Validates JWT token and returns ClaimsPrincipal.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validateIssuer"></param>
        /// <param name="validateAudience"></param>
        /// <returns></returns>
        public static ClaimsPrincipal? ValidateJWTToken(string token)
        {
            CheckSecret();
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Secret),
                    ValidateIssuer = ValidateIssuer,
                    ValidateAudience = ValidateAudience

                };
                var claims = TokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
                return claims;

            }
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_VALIDATE, "Could not validate JWT token.", ex);
            }
        }


    }
}
