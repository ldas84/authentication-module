⭐ ADR Index — Architecture Decision Records

# Architecture Decision Records (ADR) — Index

This directory contains all **Architecture Decision Records** for the Modular Backend Project.

ADRs document significant architectural decisions, their context, and their consequences.  
They serve as a historical and technical reference for maintainers and contributors.

---

## 📌 ADR List

| ID      | Title                                      | Status     | File                                      |
|---------|--------------------------------------------|------------|-------------------------------------------|
| ADR-001 | Use of Clean Architecture                  | Accepted   | [ADR-001-clean-architecture.md](ADR-001-clean-architecture.md) |
| ADR-002 | Modular Monolith Architecture              | Accepted   | [ADR-002-modular-monolith.md](ADR-002-modular-monolith.md)     |
| ADR-003 | Use of CQRS                                | Accepted   | [ADR-003-cqrs.md](ADR-003-cqrs.md)                             |
| ADR-004 | Separation of Auth and AuthZ               | Accepted   | [ADR-004-separation-auth-authz.md](ADR-004-separation-auth-authz.md) |
| ADR-005 | Use of Entity Framework Core               | Accepted   | [ADR-005-ef-core.md](ADR-005-ef-core.md)                       |

---

## 📁 Directory Structure

docs/
└── adr/
├── ADR-001-clean-architecture.md
├── ADR-002-modular-monolith.md
├── ADR-003-cqrs.md
├── ADR-004-separation-auth-authz.md
├── ADR-005-ef-core.md
└── README.md   ← (this file)


---

## 🧭 How to Add a New ADR

1. Create a new file:

ADR-00X-title.md


2. Use the standard template:

ADR-00X — Title
Status: Proposed / Accepted / Rejected / Superseded

Date: YYYY-MM-DD

Context
...

Decision
...

Consequences
...


3. Add it to the table above.

---

## 📝 Notes

ADRs are **immutable** once accepted.  
If a decision changes, create a new ADR and mark the old one as **Superseded**.

