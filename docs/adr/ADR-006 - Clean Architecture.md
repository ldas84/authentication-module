# ADR-006 — Use of Clean Architecture

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The project aims to be a modular backend with high testability, clear separation of concerns, and the ability to evolve independently per module (Auth, AuthZ, future modules).

## Decision

We adopt **Clean Architecture** as the primary architectural style for all modules.

Each module is structured into:

- Api (Presentation)
- Application (Use Cases)
- Domain (Entities + Interfaces)
- Infrastructure (Persistence, external integrations)

Dependencies flow inwards:

- Api → Application → Domain  
- Infrastructure → Domain  

No outer layer depends on inner implementation details.

## Consequences

### Positive
- High testability (Domain & Application are pure)
- Clear separation of concerns
- Easy to reason about module boundaries
- Infrastructure can be replaced with minimal impact

### Negative
- More boilerplate (layers, projects, mappings)
- Higher initial complexity for newcomers