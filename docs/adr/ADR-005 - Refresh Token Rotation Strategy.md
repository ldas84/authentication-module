# ADR-005: Refresh Token Rotation Strategy

## Status
Accepted

## Context
Refresh tokens can be stolen if stored insecurely on the client. A rotation strategy reduces the impact of token theft by invalidating old tokens.

## Decision
The system will implement refresh token rotation: each time a refresh token is used, a new one is issued and the previous one is invalidated.

## Rationale
- Prevents reuse of stolen tokens.
- Recommended by OAuth2 security best practices.
- Enables detection of suspicious activity.

## Alternatives Considered
- Non-rotating refresh tokens (less secure)
- Short-lived refresh tokens (worse UX)

## Consequences
### Positive
- Stronger protection against token theft.
- Enables anomaly detection.

### Negative
- Requires additional database operations.