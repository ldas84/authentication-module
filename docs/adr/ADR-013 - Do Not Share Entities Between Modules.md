# ADR-013 — Do Not Share Entities Between Modules

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

In a modular monolith, sharing domain entities between modules creates:

- Tight coupling  
- Hidden dependencies  
- Difficulty evolving modules independently  
- Risk of breaking changes propagating across modules  

## Decision

**No module may reference another module’s domain entities.**

Allowed communication patterns:

- Application-level interfaces  
- DTOs  
- IDs (e.g., UserId)  
- Events (future)  

Not allowed:

- Sharing domain models  
- Sharing DbContexts  
- Sharing repositories  
- Direct navigation properties across modules  

## Consequences

### Positive
- Strong encapsulation  
- Independent evolution of modules  
- Clear domain boundaries  
- Better alignment with DDD  

### Negative
- Requires mapping between modules  
- Some data must be duplicated or queried indirectly  
