Authentication and Authorization Module
Modular, reusable, and framework‑independent authentication core for .NET backends.

📌 Overview
This module provides a complete authentication and authorization foundation designed to be decoupled, reusable, and easy to integrate into any .NET backend project.
It follows Clean Architecture, includes JWT authentication, refresh token rotation, and role-based authorization.

🚀 Features
User registration

User login

JWT access token generation

Refresh token generation and rotation

Logout and token revocation

Password recovery

Basic role management

Clean Architecture (Domain, Application, Infrastructure, API)

Fully documented (SRS + ADR + diagrams)

📂 Repository Structure
Código
docs/         → SRS, ADRs, diagrams  
src/          → Domain, Application, Infrastructure, API  
tests/        → Unit and integration tests  
📘 Documentation
SRS: /docs/srs/SRS-Authentication-Module.md

ADR: /docs/adr/

Diagrams: /docs/diagrams/

🛠️ Technologies
.NET 8

JWT

BCrypt

Entity Framework Core

Clean Architecture

📅 Roadmap
v0.1.0 — Documentation + project structure

v0.2.0 — Domain layer implementation

v0.3.0 — Application layer (use cases)

v0.4.0 — Infrastructure (EF Core + JWT)

v0.5.0 — API endpoints

v1.0.0 — Stable release


graph TD

    A[Auth.Domain<br/>Entities, Value Objects] 

    B[Auth.Application<br/>Use Cases, Interfaces]
    C[Auth.Infrastructure<br/>EF Core, JWT, Repositories]
    D[Auth.Api<br/>Controllers]

    B --> A
    C --> A
    C --> B
    D --> B
    D --> C
    
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

    User ||--o{ RefreshToken : "has many"
    User ||--o{ UserRole : "assigned roles"
    Role ||--o{ UserRole : "belongs to"

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

sequenceDiagram
    participant C as Client
    participant API as Auth.Api (Controller)
    participant APP as Auth.Application (RegisterHandler)
    participant INFRA as Auth.Infrastructure
    participant DB as Database

    C->>API: POST /auth/register (email, password)
    API->>APP: RegisterAsync(email, password)

    APP->>INFRA: UserRepository.GetByEmailAsync(email)
    INFRA->>DB: Query user by email
    DB-->>INFRA: User exists? (null or entity)
    INFRA-->>APP: Result

    alt User already exists
        APP-->>API: Throw EmailAlreadyExistsError
        API-->>C: 400 Bad Request
    end

    APP->>INFRA: PasswordHasher.Hash(password)
    INFRA-->>APP: hashedPassword

    APP->>APP: Create User entity

    APP->>INFRA: UserRepository.AddAsync(user)
    INFRA->>DB: Insert new user
    DB-->>INFRA: OK

    APP-->>API: Registration successful
    API-->>C: 201 Created

flowchart TD

    %% API Layer
    API[Auth.Api<br/>Controllers, DTOs]

    %% Application Layer
    APP[Auth.Application<br/>Use Cases, Interfaces]

    %% Domain Layer
    DOMAIN[Auth.Domain<br/>Entities, Value Objects, Rules]

    %% Infrastructure Layer
    INFRA[Auth.Infrastructure<br/>Repositories, TokenService, PasswordHasher, DbContext]

    %% Relationships
    API --> APP
    APP --> DOMAIN
    APP --> INFRA
    INFRA --> DOMAIN

flowchart TB

    %% Client
    CLIENT[Client<br/>Browser / Mobile App]

    %% Gateway
    GATEWAY[API Gateway / Reverse Proxy<br/>Nginx / Traefik / APIM]

    %% API
    API[Auth.Api<br/>ASP.NET Core Controllers]

    %% Application
    APP[Auth.Application<br/>Use Cases / Handlers]

    %% Domain
    DOMAIN[Auth.Domain<br/>Entities / Value Objects]

    %% Infrastructure
    INFRA[Auth.Infrastructure<br/>Repositories / TokenService / DbContext]

    %% Database
    DB[(Database<br/>Users / Roles / Tokens)]

    %% Connections
    CLIENT -->|HTTPS| GATEWAY
    GATEWAY -->|HTTP (internal)| API
    API --> APP
    APP --> DOMAIN
    APP --> INFRA
    INFRA --> DB

flowchart TB

    %% Domain Layer
    subgraph DOMAIN[Auth.Domain]
        D1[Entities]
        D2[ValueObjects]
        D3[Interfaces]
        D4[Exceptions]
        D5[Enums]
    end

    %% Application Layer
    subgraph APP[Auth.Application]
        A1[UseCases.Auth]
        A2[UseCases.Users]
        A3[DTOs]
        A4[Interfaces]
        A5[Exceptions]
    end

    %% Infrastructure Layer
    subgraph INFRA[Auth.Infrastructure]
        I1[Persistence.Context]
        I2[Persistence.Repositories]
        I3[Persistence.Configurations]
        I4[Security]
        I5[Migrations]
    end

    %% API Layer
    subgraph API[Auth.Api]
        P1[Controllers]
        P2[Models]
        P3[Filters]
        P4[Extensions]
    end

    %% Dependencies
    API --> APP
    APP --> DOMAIN
    APP --> INFRA
    INFRA --> DOMAIN

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

stateDiagram-v2

    [*] --> Created

    Created --> Active: Token stored in DB

    Active --> Used: Token used for refresh
    Active --> Expired: Expiration reached
    Active --> Revoked: Manual revocation

    Used --> Replaced: Rotation generates new token

    Expired --> [*]
    Revoked --> [*]
    Replaced --> [*]

flowchart TD

    A([Start]) --> B[Get email and password]
    B --> C[Validate input data]

    C -->|Invalid Data| Z[Error 400 - Bad Request]
    C -->|Valid Data| D[Search user by email]

    D -->|Not exist| E[Error 401 - Unauthorized]
    D -->|Exist| F[Verify Password]

    F -->|Incorrect| E2[Error 401 - Unauthorized]
    F -->|Correct| G[Generate Access Token]

    G --> H[Generate Refresh Token]
    H --> I[Store Refresh Token en DB]

    I --> J[Return tokens (200 OK)]
    J --> K([End])

flowchart TD

    A([Start]) --> B[Get refresh token]
    B --> C[Validate token is present]

    C -->|Token not found| Z[Error 400 - Bad Request]
    C -->|Token found| D[Search for the token into database]

    D -->|Not exist| E[Error 401 - Unauthorized]
    D -->|Exist| F[Validate token status]

    F -->|Expired o revocated| E2[Error 401 - Unauthorized]
    F -->|Active| G[Generate Access Token]

    G --> H[Generate new Refresh Token (rotation)]
    H --> I[Store the new Refresh Token in DB]
    I --> J[Mark previous token as revocated/replaced]

    J --> K[Return tokens (200 OK)]
    K --> L([End])

flowchart TD

    A([Start]) --> B[Get email and password]
    B --> C[Validate input data]

    C -->|Invalid Data| Z[Error 400 - Bad Request]
    C -->|Valid Data| D[Search user by email]

    D -->|Exist| E[Error 409 - Conflict]
    D -->|Not exist| F[Hash password]

    F --> G[Create user entity]
    G --> H[Store user into DB]

    H --> I[Return 201 Created]
    I --> J([End])