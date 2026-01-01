# TrueCounsel

A robust Legal Management System built on .NET 9, following Clean Architecture principles and modern development patterns.

## 🚀 Overview

TrueCounsel is designed to streamline legal workflows, case tracking, and client management. It prioritizes maintainability, scalability, and performance through a decoupled architecture.

## 🛠 Tech Stack

- **Framework:** .NET 9 Web API
- **Layering:** Clean Architecture (Onion)
- **Patterns:** CQRS with MediatR
- **Data Access:** Entity Framework Core (SQL Server)
- **Validation:** FluentValidation with Pipeline Behaviors
- **Mapping:** AutoMapper
- **Security:** JWT (Bearer) Authentication
- **Documentation:** OpenAPI with Scalar UI

## 🏗 Architecture

The solution is divided into four main layers:

- **TrueCounsel.Domain:** Core entities, interfaces, and domain logic. Zero external dependencies.
- **TrueCounsel.Application:** Interface-driven logic, CQRS commands/queries, and DTOs.
- **TrueCounsel.Infrastructure:** Data persistence (EF Core), repository implementations, and external services.
- **TrueCounsel.API:** Entry point, Middleware, and Controllers leveraging IMediator.

## 🚦 Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server

### Setup
1. **Clone the repository:**
   ```bash
   git clone https://github.com/Sudip777/true-counsel.git
   ```
2. **Update Connection String:**
   Modify `appsettings.json` in `TrueCounsel.API` with your SQL Server details.
3. **Database Migration:**
   ```bash
   dotnet ef database update --project src/TrueCounsel.Infrastructure --startup-project src/TrueCounsel.API
   ```
4. **Run the API:**
   ```bash
   dotnet run --project src/TrueCounsel.API
   ```

## 📖 API Documentation

Once the application is running, the interactive API documentation (Scalar) is available at:
`https://localhost:PORT/` (Redirects to `/scalar/v1`)

## 🛡 Features

- **Granular Auth:** Secure authentication and identity management.
- **Case Management:** Full lifecycle tracking of legal cases.
- **Client & Lawyer Portals:** Specialized workflows for legal professionals.
- **Validation Pipeline:** Automated request validation via MediatR behaviors.
- **Consistency:** Standardized API responses and error handling.
