# Authentication Module — Clean Architecture (.NET 8)

![Version](https://img.shields.io/badge/version-v0.1.1-blue)
![Architecture](https://img.shields.io/badge/architecture-Clean%20Architecture-brightgreen)
![DotNet](https://img.shields.io/badge/.NET-8.0-purple)
![Status](https://img.shields.io/badge/docs-available-success)

A modular, extensible, and production‑ready Authentication Module built using .NET, following Clean Architecture, Domain‑Driven Design (DDD) principles, and industry‑standard security practices.
This module is designed to serve as a standalone authentication core for larger systems or as a reusable component across multiple backend services.

📌 Features
User Registration with domain validation

Login with secure credential handling

JWT Access Tokens

Refresh Token Rotation

BCrypt Password Hashing

Clean Architecture Layers (Domain, Application, Infrastructure, API)

Repository Pattern

Entity & Value Object Modeling

Extensible Use Case Handlers

Fully documented ADRs and diagrams

📂 Project Structure
Código
authentication-module/
│
├── docs/
│   ├── adr/                     # Architectural Decision Records
│   ├── architecture/            # Full module design documentation
│   ├── diagrams/                # ERD, sequence, activity, state, package, deployment
│   └── srs/                     # Software Requirements Specification
│
├── src/
│   ├── Auth.Api/                # Presentation layer (Web API)
│   ├── Auth.Application/        # Application layer (Use cases, DTOs, interfaces)
│   ├── Auth.Domain/             # Domain layer (Entities, Value Objects, Interfaces)
│   └── Auth.Infrastructure/     # Infrastructure layer (EF Core, services, security)
│
├── .gitignore
├── Directory.Build.props
└── Auth.slnx
🏛 Clean Architecture Overview
The module follows a strict separation of concerns:

Domain Layer
Enterprise business rules

Entities, Value Objects, Domain Exceptions

Interfaces for repositories and services

Application Layer
Use cases (Login, Register, Refresh Token)

DTOs

Application exceptions

Interfaces for dependency inversion

Infrastructure Layer
EF Core DbContext

Repository implementations

Token generation

Password hashing

API Layer
Controllers

Dependency injection setup

Request/response models

🔐 Security Practices
BCrypt for password hashing

JWT for stateless authentication

Refresh Token Rotation to prevent replay attacks

Short‑lived access tokens

Domain‑validated email and token value objects

📘 ADRs (Architectural Decision Records)
The project includes detailed ADRs documenting key decisions:

ADR‑001 — Use of JWT for Authentication

ADR‑002 — Refresh Tokens for Session Continuity

ADR‑003 — Password Hashing with BCrypt

ADR‑004 — Clean Architecture Layered Design

ADR‑005 — Refresh Token Rotation Strategy

These documents help maintain architectural clarity and justify design choices.

📊 Diagrams Included
All diagrams are available under docs/diagrams/:

Entity‑Relationship Diagram (ERD)

Class Diagram

Sequence Diagrams (Login, Register, Refresh)

Activity Diagrams

State Diagram

Deployment Diagram

Package Diagram

Layer Dependency Diagram

🚀 Getting Started
Prerequisites
.NET 8 SDK

SQL Server or any EF Core‑compatible database

Run the API
bash
cd src/Auth.Api
dotnet run
The API will start on the configured port (see launchSettings.json).

🧪 Testing the Endpoints
You can use the included .http file:

Código
src/Auth.Api/Auth.Api.http
Or test manually using Postman / Thunder Client.

📦 Roadmap
Role‑based authorization

Email verification

Password reset flow

Multi‑factor authentication (MFA)

Integration tests

Docker support

📄 License
This module is part of a personal backend architecture portfolio and is free to use for learning and experimentation.