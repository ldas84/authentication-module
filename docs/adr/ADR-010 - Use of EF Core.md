# ADR-005 — Use of Entity Framework Core

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The project needs a mature ORM for .NET, with strong support for SQL Server, migrations, and integration with Clean Architecture.

## Decision

We use **Entity Framework Core** as the ORM for all persistence needs:

- Each module has its own DbContext:
  - `AuthDbContext`
  - `AuthorizationDbContext`
  - Future: `UsersDbContext`, etc.
- Repositories are implemented on top of EF Core
- Migrations are managed per module

## Consequences

### Positive
- Strong integration with .NET
- Migrations and schema evolution are straightforward
- LINQ-based querying
- Good community and documentation

### Negative
- Requires careful handling of performance (tracking, includes)
- Potential overuse of lazy loading if not controlled
- Ties persistence to EF Core abstractions (though behind repositories)
