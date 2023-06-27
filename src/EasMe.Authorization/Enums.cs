namespace EasMe.Authorization;

public enum HttpMethod
{
    GET,
    POST,
    PUT,
    DELETE,
    HEAD,
    OPTIONS,
    PATCH,
}

// public enum AuthorizationType
// {
//     HttpMethodAuthorization,
//     EndpointAuthorization
// }

public static class EasMeClaimType
{
    /// <summary>
    ///     Claim Type for initializing User authorization in order to use <see cref="HttpMethodAuthorizationMiddleware" />.
    ///     <br />
    ///     <br />
    ///     Value must contain <see cref="HttpMethod" /> strings separated with ","
    /// </summary>
    public static string HttpMethodPermissions => "EasMe.Authorization::HttpMethodPermissions";

    /// <summary>
    ///     Claim Type for initializing User authorization in order to use <see cref="RequirePermissionAttribute" />.
    ///     <br />
    ///     <br />
    ///     Value must contain <see cref="HttpMethod" /> strings separated with ","
    /// </summary>
    public static string EndPointPermissions => "EasMe.Authorization::EndPointPermissions";
}