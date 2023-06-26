# EasMe.Authorization

Permission Authorization helper for AsNetCore.Mvc.
Uses base HttpContext.User authentication and verifies permissions initialized by server

### Table of Contents

- Filters > HasPermissionAttribute
- Middlewares > HttpMethodAuthorizationMiddleware
- AuthorizationHelper
- Enums

### HasPermissionAttribute

1. Add "EasMeClaimType.EndPointPermissions" to User claims
2. Set value as list of permissions separeted by ","
3. Add HasPermission("PermissionName") to Controller or Endpoint Action

### HttpMethodAuthorizationMiddleware

1. Add "EasMeClaimType.HttpMethodPermissions" to User claims
2. Set value as list of permissions separeted by ","
3. Get permission values from "EasMe.Authorization.HttpMethod" enum