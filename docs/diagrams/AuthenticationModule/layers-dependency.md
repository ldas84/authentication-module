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