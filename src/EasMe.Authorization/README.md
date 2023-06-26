# EasMe.Authorization .NET 6

Permission Authorization helper for AsNetCore.Mvc.
Uses base HttpContext.User authentication and verifies permissions initialized by server

### Table of Contents

- HasPermissionAttribute
- HttpMethodAuthorizationMiddleware
- Enums

### HasPermissionAttribute

1. Add "EasMeClaimType.EndPointPermissions" to User claims
2. Set value as list of permissions separated by ","
3. Add HasPermission("PermissionName") to Controller or Endpoint Action

### HttpMethodAuthorizationMiddleware
 
1. Add "EasMeClaimType.HttpMethodPermissions" to User claims
2. Set value as list of permissions separated by ","
3. Get permission values from "EasMe.Authorization.HttpMethod" enum


### Enums

HttpMethod enum type is for HttpMethodAuthorizationMiddleware
```csharp
public enum HttpMethod
{
    GET,
    POST,
    PUT,
    DELETE,
    PATCH,
    HEAD,
    OPTIONS,
    TRACE,
    CONNECT
}

```
Pre-defined claim type strings for adding user permissions to claims. 

It is required to add these claims to user claims in order to use HasPermissionAttribute and HttpMethodAuthorizationMiddleware

```csharp
//Claim type for HttpMethodAuthorizationMiddleware
EasMeClaimType.HttpMethodPermissions

//Claim type for HasPermissionAttribute
EasMeClaimType.EndPointPermissions 
```
