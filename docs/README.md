# Authentication Module — v0.1.0

### Clean Architecture • Modular • Extensible • C# • .NET 10

This project provides the foundational structure for a modular authentication system, designed with Clean Architecture principles to ensure separation of concerns, testability, and long‑term maintainability.

Version **v0.1.0** establishes the architectural skeleton of the module, including the Domain, Application, Infrastructure, and API layers.

---

## 🚀 Key Features (v0.1.0)

- Full Clean Architecture structure:
  - **Domain** — Entities, interfaces, value objects, domain rules
  - **Application** — DTOs, use cases, application services
  - **Infrastructure** — EF Core context, repositories, technical services
  - **API** — Controllers, routing, dependency injection
- Initial repository implementation (`UserRepository`)
- Initial authentication service (`AuthService`)
- Use case handler for user registration
- Basic API endpoint: `POST /api/auth/register`
- EF Core DbContext scaffolded
- Successfully compiles on .NET 10

This version focuses on architecture, not business logic — serving as a clean, professional foundation for future development.

---

## 🧱 Architecture Overview

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

## 📦 Requirements

- .NET 10 SDK  
- SQL Server or SQLite (for future versions)  
- Git  

---

## ▶️ How to Run

```bash
git clone https://github.com/<your-username>/<your-repo>.git
cd authentication-module
dotnet build
dotnet run --project src/Auth.Api
API will be available at:


https://localhost:5001
http://localhost:5000
📌 Available Endpoint (v0.1.0)
POST /api/auth/register
Request body:

json
{
  "email": "user@example.com",
  "password": "123456"
}
This endpoint currently stores the user using the base architecture.
Password hashing and validation will be added in v0.2.0.

🗺️ Roadmap
v0.2.0 — Core Authentication Logic
Secure password hashing (PBKDF2 / BCrypt / Argon2)

Domain and application validations

Error handling and domain exceptions

Preparation for JWT

v0.3.0 — Full Authentication
JWT Access Token

Refresh Token flow

TokenService implementation

Endpoints: login, refresh, me

v0.4.0 — Advanced Security
Roles and permissions

Claims-based authorization

Basic auditing

v1.0.0 — Stable Release
Full documentation

Unit and integration tests

Integration examples

📘 Full Architecture (future design)
The complete module design, including JWT, refresh tokens, role management, and advanced flows, is documented here:


👤 Author
Luis Diego Arias Sánchez — Backend Developer
Focused on clean architecture, modular design, and professional engineering practices.

docs/architecture/Full-Module-Design.md


```md
# Full Authentication & Authorization Module — Complete Architecture

This document describes the **full design** of the Authentication and Authorization Module, including all planned features for the stable release (v1.0.0).

It includes diagrams, flows, sequence diagrams, ERD, class diagrams, and architecture layers.

This is the **future complete design**, not the current implementation.

---

# 📌 Overview

A modular, reusable, and framework‑independent authentication core for .NET backends.

This module provides a complete authentication and authorization foundation designed to be decoupled, reusable, and easy to integrate into any .NET backend project.

It follows Clean Architecture, includes JWT authentication, refresh token rotation, and role-based authorization.

---

# 🚀 Features (Full Module)

- User registration  
- User login  
- JWT access token generation  
- Refresh token generation and rotation  
- Logout and token revocation  
- Password recovery  
- Basic role management  
- Clean Architecture (Domain, Application, Infrastructure, API)  
- Fully documented (SRS + ADR + diagrams)  

---

# 📂 Repository Structure

docs/         → SRS, ADRs, diagrams
src/          → Domain, Application, Infrastructure, API
tests/        → Unit and integration tests

Código

---

# 🛠️ Technologies

- .NET 8 / 10  
- JWT  
- BCrypt  
- Entity Framework Core  
- Clean Architecture  

---

# 📅 Roadmap (Full Module)

- **v0.1.0** — Documentation + project structure  
- **v0.2.0** — Domain layer implementation  
- **v0.3.0** — Application layer (use cases)  
- **v0.4.0** — Infrastructure (EF Core + JWT)  
- **v0.5.0** — API endpoints  
- **v1.0.0** — Stable release  

---

# 🧱 Architecture Diagram

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



# 🗄️ ER Diagram

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


# 🔄 Login Flow — Sequence Diagram

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



# 🔄 Refresh Token Flow — Sequence Diagram

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

# 🧩 Class Diagram

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

# 🔁 Token Lifecycle — State Diagram

    [*] --> Created

    Created --> Active: Token stored in DB

    Active --> Used: Token used for refresh
    Active --> Expired: Expiration reached
    Active --> Revoked: Manual revocation

    Used --> Replaced: Rotation generates new token

    Expired --> [*]
    Revoked --> [*]
    Replaced --> [*]

# 🔐 Login Flow — Flowchart

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

# 🔄 Refresh Flow — Flowchart

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

# 🧱 Registration Flow — Flowchart

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

# 📘 End of Full Module Design