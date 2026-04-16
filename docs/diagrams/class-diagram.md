classDiagram

    class User {
        +Guid Id
        +string Email
        +string PasswordHash
        +bool IsActive
        +List~Role~ Roles
        +List~RefreshToken~ RefreshTokens
        +AddRole(Role role)
        +AddRefreshToken(RefreshToken token)
    }

    class Role {
        +Guid Id
        +string Name
    }

    class RefreshToken {
        +Guid Id
        +string Token
        +DateTime ExpiresAt
        +bool IsRevoked
        +DateTime CreatedAt
        +string ReplacedByToken
    }

    class IUserRepository {
        +GetByEmailAsync(string email) User
        +AddAsync(User user) Task
    }

    class IRoleRepository {
        +GetByNameAsync(string name) Role
        +AddAsync(Role role) Task
    }

    class IRefreshTokenRepository {
        +GetByTokenAsync(string token) RefreshToken
        +AddAsync(RefreshToken token) Task
        +RevokeAsync(RefreshToken token) Task
    }

    class ITokenService {
        +GenerateAccessToken(User user) string
        +GenerateRefreshToken() RefreshToken
    }

    class IPasswordHasher {
        +Hash(string password) string
        +Verify(string password, string hash) bool
    }

    %% Relationships
    User "1" --> "*" RefreshToken
    User "*" --> "*" Role
    IUserRepository --> User
    IRoleRepository --> Role
    IRefreshTokenRepository --> RefreshToken
    ITokenService --> User
    IPasswordHasher --> User