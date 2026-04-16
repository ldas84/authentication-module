erDiagram

    User {
        GUID Id PK
        string Email
        string PasswordHash
        datetime CreatedAt
        bool IsActive
    }

    Role {
        GUID Id PK
        string Name
    }

    UserRole {
        GUID UserId FK
        GUID RoleId FK
    }

    RefreshToken {
        GUID Id PK
        GUID UserId FK
        string Token
        datetime ExpiresAt
        bool IsRevoked
        datetime CreatedAt
        string ReplacedByToken
    }