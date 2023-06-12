using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EasMe;

/// <summary>
///     JWT Authentication helper, generating and reading tokens.
/// </summary>
public class EasJWT {
    private readonly JwtSecurityTokenHandler TokenHandler = new();

    public EasJWT(string secret, string? issuer = null, string? audience = null) {
        Issuer = issuer;
        Audience = audience;
        if (!string.IsNullOrEmpty(Issuer)) ValidateIssuer = true;
        if (!string.IsNullOrEmpty(Audience)) ValidateAudience = true;
        Secret = Encoding.ASCII.GetBytes(secret);
    }

    private bool ValidateIssuer { get; }
    private bool ValidateAudience { get; }
    private string? Issuer { get; }
    private string? Audience { get; }
    private byte[]? Secret { get; }

    /// <summary>
    ///     Generates a JWT token by ClaimsIdentity.
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <param name="expireMinutes"></param>
    /// <returns></returns>
    public string GenerateJwtToken(ClaimsIdentity claimsIdentity, int expireMinutes) {
        return GenerateJwtToken(claimsIdentity, DateTime.Now.AddMinutes(expireMinutes));
    }

    public string GenerateJwtToken(ClaimsIdentity claimsIdentity, DateTime expire) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = claimsIdentity,
            Expires = expire,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature),
            Issuer = Issuer,
            Audience = Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    ///     Generates a JWT token by claims as IDictionary.
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <param name="expireMinutes"></param>
    /// <returns></returns>
    public string GenerateJwtToken(Dictionary<string, object?> claims, int expireMinutes) {
        return GenerateJwtToken(claims, DateTime.Now.AddMinutes(expireMinutes));
    }

    public string GenerateJwtToken(Dictionary<string, object?> claims, DateTime expire) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor {
            Claims = claims,
            Expires = expire,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature),
            Issuer = Issuer,
            Audience = Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    ///     Validates JWT token and returns ClaimsPrincipal.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="validateIssuer"></param>
    /// <param name="validateAudience"></param>
    /// <returns></returns>
    public ClaimsPrincipal? ValidateJwtToken(string token) {
        var tokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Secret),
            ValidateIssuer = ValidateIssuer,
            ValidateAudience = ValidateAudience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero
        };
        var claims = TokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        return claims;
    }
}