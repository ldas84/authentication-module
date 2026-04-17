# ADR-002: Use of Refresh Tokens for Session Continuity

## Status
Accepted

## Context
Access tokens must be short-lived for security reasons. Users should not be required to log in repeatedly, so a mechanism is needed to renew access tokens without re-entering credentials.

## Decision
The system will use long-lived refresh tokens stored in the database to renew access tokens.

## Rationale
- Enables long-term sessions without storing server-side state.
- Allows revocation of compromised tokens.
- Supports token rotation for enhanced security.

## Alternatives Considered
- Long-lived access tokens (less secure)
- Re-authentication on every expiration (poor UX)
- Server-side sessions (not aligned with stateless architecture)

## Consequences
### Positive
- Secure and modern session management.
- Enables logout and token revocation.
- Supports rotation to mitigate token theft.

### Negative
- Requires database storage and lookup.
- Slightly more complex implementation.