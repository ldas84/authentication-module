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