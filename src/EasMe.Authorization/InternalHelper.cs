namespace EasMe.Authorization;

internal static class InternalHelper
{
    internal static HttpMethod? GetHttpMethod(string permission) {
        var parse = Enum.TryParse(typeof(HttpMethod), permission, out var obj);
        if (!parse) return null;
        return (HttpMethod?)obj;
    }

    internal static string[] SplitPermissions(string permissionString) {
        return permissionString.Split(",").ToArray();
    }
}