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