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