# ADR-012 — One DbContext per Module

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

Each module (Auth, AuthZ, Users, etc.) has its own domain, entities, and persistence rules.  
Sharing a single DbContext across modules would:

- Create coupling  
- Mix unrelated entities  
- Make migrations harder  
- Reduce modularity  

## Decision

Each module will have **its own DbContext**, for example:

- `AuthDbContext`
- `AuthorizationDbContext`
- `UsersDbContext` (future)
- `NotificationsDbContext` (future)

Each DbContext manages only its module’s entities.

## Consequences

### Positive
- Strong module boundaries  
- Independent migrations  
- Clear ownership of tables  
- Easier refactoring and evolution  

### Negative
- Requires multiple DbContexts in DI  
- Cross-module queries require coordination  
