sequenceDiagram
    participant C as Client
    participant API as Auth.Api (Controller)
    participant APP as Auth.Application (LoginHandler)
    participant INFRA as Auth.Infrastructure
    participant DB as Database

    C->>API: POST /auth/login (email, password)
    API->>APP: LoginAsync(email, password)

    APP->>INFRA: UserRepository.GetByEmailAsync(email)
    INFRA->>DB: Query user by email
    DB-->>INFRA: User entity
    INFRA-->>APP: User entity

    APP->>INFRA: PasswordHasher.Verify(password, user.PasswordHash)
    INFRA-->>APP: Valid / Invalid

    alt Invalid password
        APP-->>API: Throw AuthenticationError
        API-->>C: 401 Unauthorized
    end

    APP->>INFRA: TokenService.GenerateAccessToken(user)
    INFRA-->>APP: accessToken

    APP->>INFRA: TokenService.GenerateRefreshToken()
    INFRA-->>APP: refreshToken

    APP->>INFRA: Save refreshToken to DB
    INFRA->>DB: Insert refresh token
    DB-->>INFRA: OK

    APP-->>API: accessToken, refreshToken
    API-->>C: 200 OK (tokens)