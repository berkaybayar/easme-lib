using EasMe.Exceptions;
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

        private readonly static JwtSecurityTokenHandler TokenHandler = new();
        private static bool ValidateIssuer { get; set; } = false;
        private static bool ValidateAudience { get; set; } = false;
        private static string? Issuer { get; set; }
        private static string? Audience { get; set; }
        private static byte[]? Secret { get; set; }
        public static bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Loads your secret key, issuer, audience. Call this method in your application startup.
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        public static void Init(string secret, string? issuer = null, string? audience = null)
        {
            Issuer = issuer;
            Audience = audience;
            if (!string.IsNullOrEmpty(Issuer)) ValidateIssuer = true;
            if (!string.IsNullOrEmpty(Audience)) ValidateAudience = true;
            Secret = Encoding.ASCII.GetBytes(secret);
            IsInitialized = true;
        }

        private static void CheckSecret()
        {
            if (!IsInitialized) throw new NotInitializedException("EasJWT configuration error, not initialized. Call EasJWT.Init() in your application startup.");
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
                throw new FailedToCreateException("Could not create JWT token.", ex);
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
                throw new FailedToValidateException("Could not validate JWT token.", ex);
            }
        }


    }
}
