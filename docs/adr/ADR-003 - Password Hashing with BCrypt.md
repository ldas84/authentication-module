# ADR-003: Password Hashing with BCrypt

## Status
Accepted

## Context
User passwords must be stored securely. The hashing algorithm must be resistant to brute-force attacks and support configurable work factors.

## Decision
The system will use BCrypt for password hashing.

## Rationale
- BCrypt is widely used and battle-tested.
- It includes built-in salting.
- It supports adjustable cost factors to increase security over time.

## Alternatives Considered
- Argon2 (more modern but not required for MVP)
- PBKDF2 (secure but less resistant to GPU attacks)
- SHA-256 or SHA-512 (not suitable for password hashing)

## Consequences
### Positive
- Strong protection against brute-force attacks.
- Easy to implement and maintain.

### Negative
- Slightly slower than PBKDF2 (acceptable for authentication workloads).