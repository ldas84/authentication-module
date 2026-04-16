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