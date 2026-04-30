# ADR-003 — Use of CQRS (Commands and Queries)

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The project needs clear separation between read and write operations, explicit use cases, and testable business logic.

## Decision

We adopt **CQRS** at the Application layer:

- **Commands** for write operations (e.g., CreateRole, AssignRole)
- **Queries** for read operations (e.g., GetRoles, CheckPermission)
- **Handlers** encapsulate each use case
- DTOs are used for input/output models

We do **not** implement event sourcing; CQRS is applied at the application/use-case level only.

## Consequences

### Positive
- Clear, explicit use cases
- Easier testing of business flows
- Better separation of read/write concerns
- Easier evolution of APIs

### Negative
- More classes (commands, queries, handlers)
- Slightly higher learning curve
