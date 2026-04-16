sequenceDiagram
    participant C as Client
    participant API as Auth.Api (Controller)
    participant APP as Auth.Application (RefreshTokenHandler)
    participant INFRA as Auth.Infrastructure
    participant DB as Database

    C->>API: POST /auth/refresh (refreshToken)
    API->>APP: RefreshAsync(refreshToken)

    APP->>INFRA: RefreshTokenRepository.GetByToken(refreshToken)
    INFRA->>DB: Query refresh token
    DB-->>INFRA: RefreshToken entity
    INFRA-->>APP: RefreshToken entity

    APP->>APP: Validate token (not expired, not revoked)

    alt Invalid token
        APP-->>API: Throw AuthenticationError
        API-->>C: 401 Unauthorized
    end

    APP->>INFRA: TokenService.GenerateAccessToken(user)
    INFRA-->>APP: accessToken

    APP->>INFRA: TokenService.GenerateRefreshToken()
    INFRA-->>APP: newRefreshToken

    APP->>INFRA: Save new refresh token
    INFRA->>DB: Insert new refresh token
    DB-->>INFRA: OK

    APP->>INFRA: Revoke old refresh token
    INFRA->>DB: Update old token (IsRevoked = true)
    DB-->>INFRA: OK

    APP-->>API: accessToken, newRefreshToken
    API-->>C: 200 OK (tokens)