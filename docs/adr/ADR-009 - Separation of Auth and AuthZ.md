# ADR-004 — Separation of Authentication (Auth) and Authorization (AuthZ)

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

Authentication (identity, login, tokens) and authorization (roles, permissions, access rules) are related but distinct concerns. Mixing them in a single module reduces clarity and flexibility.

## Decision

We separate:

- **Auth Module** → Authentication:
  - User identity
  - Login, register, refresh tokens
  - JWT issuance

- **AuthZ Module** → Authorization:
  - Roles
  - Permissions
  - Role–Permission relationships
  - Permission checks

AuthZ does not manage users directly; it integrates with Auth via IDs and contracts.

## Consequences

### Positive
- Clear separation of responsibilities
- Each module can evolve independently
- Easier to replace or extend Auth or AuthZ
- Better alignment with real-world architectures

### Negative
- Requires integration logic between modules
- Slightly more complexity in configuration and wiring
