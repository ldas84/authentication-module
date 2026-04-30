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