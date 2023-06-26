namespace EasMe.Authorization;

public enum HttpMethod
{
    GET,
    POST,
    PUT,
    DELETE,
    CONNECT,
    HEAD,
    OPTIONS,
    PATCH,
    TRACE
}

public enum AuthorizationType
{
    HttpMethodAuthorization,
    EndpointAuthorization
}

public static class EasMeClaimType
{
    /// <summary>
    ///     Claim Type for initializing User authorization in order to use <see cref="HttpMethodAuthorizationMiddleware" />.
    ///     <br />
    ///     Value must contain <see cref="HttpMethod" /> strings merged with ","
    /// </summary>
    public static string HttpMethodPermissions => "EasMe.Authorization::Permissions";

    /// <summary>
    ///     Claim Type for initializing User authorization in order to use <see cref="HasActionPermissionAttribute" />.
    ///     <br />
    ///     Value must contain <see cref="HttpMethod" /> strings merged with ","
    /// </summary>
    public static string EndPointPermissions => "EasMe.Authorization::EndPointPermissions";
}