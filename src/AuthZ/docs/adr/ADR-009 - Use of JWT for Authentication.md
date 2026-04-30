# ADR-009 — Use of JWT for Authentication

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The system requires a stateless, scalable authentication mechanism suitable for:

- APIs  
- Modular architectures  
- Future distributed systems  
- Integration with AuthZ  

JWT is widely supported and integrates well with .NET.

## Decision

We adopt **JWT (JSON Web Tokens)** as the primary authentication mechanism.

Key points:

- Access tokens contain user identity  
- Refresh tokens stored securely in Auth module  
- Tokens signed with strong symmetric keys  
- Expiration enforced  
- No session state stored on server  

## Consequences

### Positive
- Stateless authentication  
- Easy integration with Authorization module  
- Works well with microservices if needed  
- Standard in modern APIs  

### Negative
- Requires careful key management  
- Token invalidation is non-trivial  
- Must avoid storing sensitive data inside tokens  
