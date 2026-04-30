sequenceDiagram
    autonumber

    participant C as Client
    participant API as AuthZ.Api<br/>AuthorizationController
    participant APP as AuthZ.Application<br/>CheckPermissionHandler
    participant AR as IAuthorizationService
    participant RR as IRoleRepository
    participant PR as IPermissionRepository
    participant INFRA as AuthZ.Infrastructure<br/>AuthorizationService

    C->>API: GET /authorization/check?roleId=&permission=
    API->>APP: Send CheckPermissionQuery(roleId, permissionCode)

    APP->>AR: RoleHasPermissionAsync(roleId, permissionCode)

    AR->>RR: GetByIdAsync(roleId)
    RR-->>AR: Role

    AR->>PR: ExistsByCodeAsync(permissionCode)
    PR-->>AR: Permission exists

    AR->>INFRA: CheckRolePermissionAsync(roleId, permissionCode)
    INFRA-->>AR: true/false

    AR-->>APP: Permission result

    APP-->>API: Result (200 OK)
    API-->>C: { "hasPermission": true/false }