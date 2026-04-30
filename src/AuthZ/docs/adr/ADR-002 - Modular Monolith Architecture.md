# ADR-002 — Modular Monolith Architecture

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The system must support multiple business domains (Auth, AuthZ, Users, Notifications, Audit) with strong boundaries, but full microservices are not required at this stage.

## Decision

We adopt a **Modular Monolith** architecture:

- A single deployable application
- Multiple **independent modules** inside the same codebase
- Each module has its own layers (Api, Application, Domain, Infrastructure)
- Modules communicate via well-defined interfaces and Application layer

## Consequences

### Positive
- Simpler deployment than microservices
- Strong domain boundaries
- Easier refactoring and evolution
- Possible future extraction to microservices if needed

### Negative
- Requires discipline to avoid cross-module coupling
- Build times may grow as the solution scales
