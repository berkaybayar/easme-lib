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
    public class EasJWT
    {

        private readonly static JwtSecurityTokenHandler TokenHandler = new();
        private static bool ValidateIssuer { get; set; } = false;
        private static bool ValidateAudience { get; set; } = false;
        private static string? Issuer { get; set; }
        private static string? Audience { get; set; }
        private static byte[]? Secret { get; set; }
        
        public EasJWT(string secret, string? issuer = null, string? audience = null)
        {
            Issuer = issuer;
            Audience = audience;
            if (!string.IsNullOrEmpty(Issuer)) ValidateIssuer = true;
            if (!string.IsNullOrEmpty(Audience)) ValidateAudience = true;
            Secret = Encoding.ASCII.GetBytes(secret);
        }
        public byte[] GetSecretByteArray() => Secret;
        
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
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = Issuer,
                    Audience = Audience

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new FailedToCreateException("EasJWT failed to create JWT token.", ex);
            }

        }

        /// <summary>
        /// Validates JWT token and returns ClaimsPrincipal.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validateIssuer"></param>
        /// <param name="validateAudience"></param>
        /// <returns></returns>
        public ClaimsPrincipal? ValidateJWTToken(string token)
        {
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
                throw new FailedToValidateException("EasJWT failed to validate JWT token.", ex);
            }
        }

        public static ClaimsIdentity ConvertModelToClaimsIdentity<T>(T Model)
        {
            var claimsIdentity = new ClaimsIdentity();
            foreach (var property in Model.GetType().GetProperties())
            {
                if (property == null) continue;
                var value = property.GetValue(Model);
                if (value == null) continue;
                claimsIdentity.AddClaim(new Claim(property.Name, value.ToString()));
            }
            return claimsIdentity;

        }
        public static T ConvertClaimsIdentityToModel<T>(ClaimsIdentity claimsIdentity)
        {
            var model = Activator.CreateInstance<T>();
            foreach (var property in model.GetType().GetProperties())
            {
                if (property == null) continue;
                var value = claimsIdentity.FindFirst(property.Name);
                if (value == null) continue;
                property.SetValue(model, value.Value);
            }
            return model;
        }

    }
}
