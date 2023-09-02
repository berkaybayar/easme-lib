using System.Security.Claims;

namespace EasMe.Extensions;

public static class ClaimsExtensions
{
    /// <summary>
    ///   Converts given model to claims identity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Model"></param>
    /// <returns></returns>
    /// <exception cref="FailedToConvertException"></exception>
    public static Dictionary<string, object> ToDictionary(this IEnumerable<Claim> claims) {
    var dictionary = new Dictionary<string, object>();
    foreach (var claim in claims) dictionary.TryAdd(claim.Type, claim.Value);
    return dictionary;
  }


    /// <summary>
    ///   Converts given model to claims identity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ConversionNotSupportedException"></exception>
    public static ClaimsIdentity ToClaimsIdentity<T>(this T model) {
    var claimsIdentity = new ClaimsIdentity();
    var props = model?.GetType().GetProperties();
    if (props is null || props.Length == 0)
      throw new Exception("Failed to convert model to claims identity. Model has no properties");
    foreach (var property in props) {
      var value = property.GetValue(model);
      if (value is null) continue;
      claimsIdentity.AddClaim(new Claim(property.Name, value.ToString() ?? string.Empty));
    }

    return claimsIdentity;
  }

    /// <summary>
    ///   Converts given model to claims identity. Outs exceptions if there is an error occurs while converting one of the
    ///   properties. This adds one claim that
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <param name="exceptionMessages"></param>
    /// <returns></returns>
    public static ClaimsIdentity ToClaimsIdentity<T>(this T model, out List<Exception> exceptionMessages) {
    exceptionMessages = new List<Exception>();
    var claimsIdentity = new ClaimsIdentity();
    var props = model?.GetType().GetProperties();
    if (props is null || props.Length == 0)
      throw new Exception("Failed to convert model to claims identity. Model has no properties");
    foreach (var property in props)
      try {
        var value = property.GetValue(model);
        if (value == null) continue;
        claimsIdentity.AddClaim(new Claim(property.Name, value.ToString()));
      }
      catch (Exception ex) {
        exceptionMessages.Add(ex);
      }

    return claimsIdentity;
  }
}