# ADR-001: Use of JWT for Authentication

## Status
Accepted

## Context
The module requires a stateless authentication mechanism that can be easily consumed by frontend applications and internal services. The solution must support scalability, horizontal deployment, and integration with modern web architectures.

## Decision
The system will use JSON Web Tokens (JWT) as the primary mechanism for access tokens.

## Rationale
- JWT is widely adopted and compatible with modern frontend frameworks.
- It supports stateless authentication, enabling horizontal scaling.
- It allows embedding claims such as user ID, email, and roles.
- It avoids server-side session storage.

## Alternatives Considered
- Server-side sessions (cookies)
- Opaque tokens with database lookup
- OAuth2 authorization code flow (not required for this module)

## Consequences
### Positive
- Easy integration with SPAs and mobile apps.
- No server-side session storage required.
- Good performance and scalability.

### Negative
- JWTs cannot be revoked unless paired with refresh tokens.
- Requires secure signing key management.
