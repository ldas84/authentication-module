sequenceDiagram
    autonumber

    participant C as Client
    participant API as AuthZ.Api<br/>RolesController
    participant APP as AuthZ.Application<br/>AssignRoleHandler
    participant RR as IRoleRepository
    participant PR as IPermissionRepository
    participant AR as IAuthorizationService
    participant INFRA as AuthZ.Infrastructure<br/>RoleRepository

    C->>API: POST /roles/{roleId}/assign
    API->>APP: Send AssignRoleCommand(roleId, userId)

    APP->>RR: GetByIdAsync(roleId)
    RR-->>APP: Role

    APP->>AR: Validate role assignment rules
    AR-->>APP: Validation OK

    APP->>INFRA: AddRoleToUserAsync(roleId, userId)
    INFRA-->>APP: Success

    APP-->>API: Result (200 OK)
    API-->>C: Role assigned successfully
