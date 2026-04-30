⭐ CONTRIBUTING.md — Modular Backend Project

# Contributing Guide — Modular Backend Project

Thank you for your interest in contributing to this modular backend.  
This project follows **Clean Architecture**, **DDD**, and **CQRS**, and is organized into independent modules such as:

- Authentication (Auth)
- Authorization (AuthZ)
- Future modules (Users, Notifications, Audit, etc.)

This guide explains how to contribute effectively and consistently.

---

# 📁 Project Structure

Each module follows the same structure:

ModuleName/
│
├── ModuleName.Api/
├── ModuleName.Application/
├── ModuleName.Domain/
└── ModuleName.Infrastructure/


Shared documentation lives under:

docs/


Global project documentation lives under:

.github/


---

# 🧱 Architectural Principles

All contributions must respect the following principles:

### ✔ Clean Architecture
- Api → Application → Domain  
- Infrastructure depends only on Domain  
- No circular dependencies  

### ✔ Domain-Driven Design
- Domain contains entities and interfaces  
- Application contains use cases  
- Infrastructure contains implementations  
- Api contains controllers and request/response models  

### ✔ Modularity
- Each module evolves independently  
- No cross-module coupling except through well-defined interfaces  
- Shared logic goes into `Shared/` (future)

---

# 🧪 Coding Standards

### ✔ Language & Framework
- C# 13 / .NET 10
- EF Core 10
- Async/await everywhere
- Nullable reference types enabled

### ✔ Style
- Follow .NET conventions (PascalCase, camelCase)
- No business logic in controllers
- No direct DbContext usage in Application layer
- Use records for DTOs when possible
- Use dependency injection everywhere

### ✔ Error Handling
- Use domain exceptions when appropriate
- Return meaningful HTTP responses in Api layer
- Avoid throwing generic exceptions

---

# 🧩 Adding a New Module

To add a new module:

1. Create folder structure:

NewModule/
├── NewModule.Api
├── NewModule.Application
├── NewModule.Domain
└── NewModule.Infrastructure


2. Add the module to the solution:


dotnet sln add ...


3. Implement:
- Domain entities + interfaces
- Application commands/queries + handlers
- Infrastructure repositories + DbContext
- Api controllers

4. Add diagrams under:

docs/diagrams/NewModule/


5. Add README + CHANGELOG + ROADMAP for the module.

---

# 📝 Commit Guidelines

This project uses **Conventional Commits**:

### Types
- `feat:` new feature
- `fix:` bug fix
- `docs:` documentation changes
- `refactor:` code refactoring
- `test:` adding or updating tests
- `chore:` tooling, configs, CI/CD

### Examples

feat(authz): add AssignRoleCommand handler
fix(auth): resolve null reference in RefreshToken flow
docs: add architecture overview diagram


---

# 🔀 Pull Request Guidelines

### ✔ PR Requirements
- Clear title using Conventional Commits
- Description of the change
- Reference to related issues (if any)
- Include tests when applicable
- Include diagrams if architecture changes
- Update module README/CHANGELOG if needed

### ✔ PR Size
- Prefer small, focused PRs
- Large PRs must be split into logical chunks

---

# 📐 Diagrams

All diagrams must be stored under:

docs/diagrams/<ModuleName>/


Supported diagram types:

- ERD
- Class diagrams
- Component diagrams
- Architecture diagrams
- Sequence diagrams
- Activity diagrams

All diagrams must be written in **Mermaid**.

---

# 🧪 Testing

### ✔ Unit Tests
- Required for Application and Domain layers
- Use xUnit or NUnit

### ✔ Integration Tests
- Required for Infrastructure and Api layers
- Use Testcontainers (future)

---

# 🚀 Branching Strategy

- `main` → stable releases
- `develop` → active development (optional)
- `feature/*` → new features
- `fix/*` → bug fixes
- `docs/*` → documentation updates

---

# 🧭 Release Process

1. Update module CHANGELOG
2. Update project ROADMAP if needed
3. Tag release using semantic versioning
4. Merge into `main`

---

# 🤝 Code of Conduct

Be respectful, constructive, and collaborative.  
This project values clarity, quality, and professionalism.

---

# 🙌 Thank You

Your contributions help make this modular backend cleaner, stronger, and more scalable.
