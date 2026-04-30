# Changelog — Authorization Module (AuthZ)

All notable changes to the **AuthZ** module will be documented in this file.

The format follows [Semantic Versioning](https://semver.org/) and the structure recommended by [Keep a Changelog](https://keepachangelog.com/).

---

## [0.2.0] - 2026-04-24
### Added
- Initial version of the **Authorization Module (AuthZ)**.
- Domain entities:
  - `Role`
  - `Permission`
  - `RolePermission` (many-to-many relationship)
- Domain interfaces:
  - `IRoleRepository`
  - `IPermissionRepository`
  - `IAuthorizationService`
- Application layer:
  - Commands: `CreateRoleCommand`, `CreatePermissionCommand`, `AssignRoleCommand`
  - Handlers for all commands
  - DTOs for roles and permissions
- Infrastructure layer:
  - `AuthorizationDbContext`
  - `RoleRepository`
  - `PermissionRepository`
  - `AuthorizationService` implementation
- API layer:
  - `RolesController`
  - `PermissionsController`
  - `AuthorizationController`
- ERD diagram for Roles & Permissions
- Class Diagram (Domain)
- Component Diagram
- Architecture Diagram
- Sequence Diagrams:
  - AssignRole
  - CheckPermission
- Full README for the AuthZ module (English)

### Changed
- Integrated AuthZ into the main solution (`.sln`) and build pipeline.
- Updated repository structure to support modular documentation under `docs/diagrams/AuthorizationModule`.

### Fixed
- Resolved CS8618 warnings in domain entities (`Role`, `Permission`).

### Removed
- N/A

---

## [0.3.0] - Unreleased
### Planned
- Dynamic policy-based authorization
- Hierarchical permissions
- Permission caching layer
- Audit logging for role/permission changes
- Admin UI for managing roles and permissions
- Integration tests for authorization flows

---

## [0.1.0] - Not Applicable
AuthZ did not exist before version 0.2.0.

---

