⭐ README — Authorization Module (AuthZ)
Role-Based Access Control (RBAC) & Permission-Based Authorization

📌 Overview
The Authorization Module (AuthZ) provides a complete, modular, and extensible authorization system for the backend.
It implements:

-Roles

-Permissions

-Many-to-many Role–Permission relationships

-Permission validation

-Role assignment

-Integration with the Authentication module

The module follows Clean Architecture, DDD, and a fully decoupled design that allows it to evolve independently.


📌 Goals
-Provide a robust RBAC (Role-Based Access Control) system.

-Support granular permissions (e.g., users.read, roles.create).

-Allow assigning permissions to roles.

-Allow assigning roles to users (via integration with Auth).

-Validate permissions at runtime.

-Maintain a modular, scalable, and testable architecture.


🧱 Architecture
AuthZ is structured into four layers:

-AuthZ.Api → HTTP endpoints

-AuthZ.Application → Use cases (CQRS)

-AuthZ.Domain → Entities + domain interfaces

-AuthZ.Infrastructure → EF Core + repositories

📌 Component + Architecture Diagram

flowchart TB

    classDef api fill:#4b5563,stroke:#fff,stroke-width:1px,color:#fff;
    classDef app fill:#1f2937,stroke:#fff,stroke-width:1px,color:#fff;
    classDef domain fill:#111827,stroke:#fff,stroke-width:1px,color:#fff;
    classDef infra fill:#374151,stroke:#fff,stroke-width:1px,color:#fff;

    subgraph API["AuthZ.Api (Presentation Layer)"]
        Controllers["Controllers\n- RolesController\n- PermissionsController"]
    end
    class API api

    subgraph APPLICATION["AuthZ.Application (Application Layer)"]
        Commands["Commands\n- CreateRoleCommand\n- CreatePermissionCommand\n- AssignRoleCommand"]
        Handlers["Handlers\n- CreateRoleHandler\n- CreatePermissionHandler\n- AssignRoleHandler"]
        DTOs["DTOs\n- RoleDto\n- PermissionDto"]
    end
    class APPLICATION app

    subgraph DOMAIN["AuthZ.Domain (Domain Layer)"]
        Role["Entities\n- Role"]
        Permission["Entities\n- Permission"]
        RolePermission["Entities\n- RolePermission"]
        Interfaces["Interfaces\n- IRoleRepository\n- IPermissionRepository\n- IAuthorizationService"]
    end
    class DOMAIN domain

    subgraph INFRA["AuthZ.Infrastructure (Infrastructure Layer)"]
        DbContext["AuthorizationDbContext"]
        RoleRepo["RoleRepository"]
        PermissionRepo["PermissionRepository"]
        AuthService["AuthorizationService"]
    end
    class INFRA infra

    API --> APPLICATION
    APPLICATION --> DOMAIN
    APPLICATION --> INFRA
    INFRA --> DOMAIN


🗄 Data Model (ERD)

erDiagram

    ROLE {
        GUID Id PK
        string Name
    }

    PERMISSION {
        GUID Id PK
        string Code
    }

    ROLE_PERMISSION {
        GUID RoleId FK
        GUID PermissionId FK
    }

    ROLE ||--o{ ROLE_PERMISSION : "has"
    PERMISSION ||--o{ ROLE_PERMISSION : "grants"


🧩 Domain Model

✔ Entities
Role
Represents a group of permissions.
-Id
-Name

Permission
Represents an atomic action that can be authorized.
-Id
-Code

RolePermission
Many-to-many join entity.
-RoleId
-PermissionId

✔ Domain Interfaces
-IRoleRepository
-IPermissionRepository
-IAuthorizationService
These interfaces ensure the Application layer depends only on abstractions.

⚙️ Use Cases

✔ Create a Role
POST /roles

✔ Create a Permission
POST /permissions

✔ Assign a Role to a User
POST /roles/{roleId}/assign

✔ Check if a Role Has a Permission
GET /authorization/check?roleId=&permission=

🔄 Sequence Diagrams

📌 AssignRole

sequenceDiagram
    autonumber

    participant C as Client
    participant API as AuthZ.Api<br/>RolesController
    participant APP as AuthZ.Application<br/>AssignRoleHandler
    participant RR as IRoleRepository
    participant AR as IAuthorizationService
    participant INFRA as AuthZ.Infrastructure<br/>RoleRepository

    C->>API: POST /roles/{roleId}/assign
    API->>APP: Send AssignRoleCommand(roleId, userId)

    APP->>RR: GetByIdAsync(roleId)
    RR-->>APP: Role

    APP->>AR: Validate assignment rules
    AR-->>APP: OK

    APP->>INFRA: AddRoleToUserAsync(roleId, userId)
    INFRA-->>APP: Success

    APP-->>API: 200 OK
    API-->>C: Role assigned

📌 CheckPermission

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
    API->>APP: Send CheckPermissionQuery

    APP->>AR: RoleHasPermissionAsync

    AR->>RR: GetByIdAsync(roleId)
    RR-->>AR: Role

    AR->>PR: ExistsByCodeAsync(permissionCode)
    PR-->>AR: OK

    AR->>INFRA: CheckRolePermissionAsync
    INFRA-->>AR: true/false

    AR-->>APP: result
    APP-->>API: 200 OK
    API-->>C: { hasPermission: true/false }

🧪 Technologies

-.NET 10
-Entity Framework Core 10
-SQL Server
-Clean Architecture
-DDD
-CQRS

📁 Project Structure

src/AuthZ/
│
├── AuthZ.Api/
├── AuthZ.Application/
├── AuthZ.Domain/
└── AuthZ.Infrastructure/

🚀 Roadmap

✔ v0.2.0 (current)
-Roles
-Permissions
-Role–Permission relationship
-Permission validation
-Sequence diagrams
-Architecture diagrams
-Documentation
-Integration with Auth

🔜 v0.3.0 (planned)
-Dynamic policies
-Hierarchical permissions
-Audit logging
-Admin UI for roles/permissions
-Permission caching

🔗 Integration with Authentication Module
AuthZ does not manage users.
Auth does.

Integration occurs through:
-AssignRoleCommand
-AddRoleToUserAsync
-Permission checks in protected endpoints

📝 License
This module is part of the modular backend developed by Luis Diego Arias Sánchez.