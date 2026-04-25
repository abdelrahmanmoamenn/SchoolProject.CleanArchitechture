[README.md](https://github.com/user-attachments/files/27070837/README.md)
# 🏫 School Management System — Clean Architecture REST API

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-9.0-purple?style=for-the-badge" />
  <img src="https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/Docker-Containerized-2496ED?style=for-the-badge&logo=docker&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
</p>

A production-grade **School Management REST API** built with **ASP.NET Core 9** following **Clean Architecture** principles. The system implements **CQRS via MediatR**, **Role/Claims-based Authorization**, **JWT with Refresh Tokens**, **multilingual localization**, **column-level encryption**, **structured logging**, and is fully **containerized with Docker**.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture--design-patterns)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Reference](#-api-reference)
- [Security Model](#-security-model)
- [Database Design](#-database-design)
- [Configuration](#-configuration)
- [Docker Deployment](#-docker-deployment)
- [Technical Highlights](#-technical-highlights)
- [Future Improvements](#-future-improvements)
- [Why This Project Stands Out](#-why-this-project-stands-out)
- [Author](#-author)

---

## 🔍 Overview

This project is a fully-featured backend API for managing a school's core entities — **students, departments, instructors, subjects, users, and roles** — built to demonstrate enterprise-level backend engineering practices.

The architecture enforces a strict separation of concerns across five dedicated layers, ensuring the codebase is testable, maintainable, and scalable. Every design decision reflects real-world production patterns: structured error handling, database migrations, data seeding, email workflows, image uploads, and multi-language support.

---

## ✨ Features

### Student Management
- Full CRUD (Create, Read, Update, Delete) for students
- Server-side **pagination** with configurable page size
- **Search** by name or address (bilingual)
- **Sorting** by ID, name, address, or department
- Department-level student filtering via queryable projections

### Department Management
- Retrieve department details including subjects, instructors, and paginated student list
- **Database View** integration for department-level student count reporting

### Instructor Management
- Add instructors with **image upload** (stored in `wwwroot`, served via static files)
- Self-referencing supervisor hierarchy (instructors supervise other instructors)

### Authentication & Authorization
- **JWT Bearer** token authentication
- **Refresh token** rotation with expiry tracking
- **Email confirmation** required before login
- **Password reset** via 6-digit code sent to email
- **Role-based authorization** (Admin / User roles)
- **Claims-based authorization** policies (`CreateStudent`, `EditStudent`, `DeleteStudent`)
- Token validation endpoint

### User Management
- Register, edit, delete users
- Change password with old-password verification
- Paginated user listing
- Role and claims management per user

### Role & Claims Management
- Create, edit, and delete roles
- Assign/revoke roles per user
- Assign/revoke claims per user

### Localization
- Full **multilingual support**: Arabic (`ar-EG`), English (`en-US`), German (`de-DE`), French (`fr-FR`)
- All validation messages and API responses are localized
- Culture resolved from the `Accept-Language` request header

### Email Service
- SMTP email delivery via **MailKit**
- Email confirmation on registration
- Password reset code delivery

### Logging
- **Serilog** structured logging to **Console** and **SQL Server** (`SystemLogs` table)
- Log level filtering per namespace

### Developer Experience
- **Scalar UI** (modern OpenAPI explorer) available in Development
- **JWT Bearer** scheme pre-configured in OpenAPI spec
- **Auto-migration** on startup — zero manual database setup
- Database seeding: departments, subjects, students, instructors, roles, and admin user

---

## 🛠 Tech Stack

| Category | Technology |
|---|---|
| Runtime | .NET 9 |
| Web Framework | ASP.NET Core 9 Web API |
| ORM | Entity Framework Core 9 |
| Database | Microsoft SQL Server 2022 |
| Messaging | MediatR 14 (CQRS) |
| Validation | FluentValidation 12 |
| Mapping | AutoMapper 12 |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Email | MailKit 4 |
| Logging | Serilog (Console + MSSQL sink) |
| API Docs | Scalar / OpenAPI |
| Encryption | EntityFrameworkCore.EncryptColumn |
| Containerization | Docker + Docker Compose |
| Architecture | Clean Architecture |
| Patterns | CQRS, Repository, Generic Repository, Mediator, Pipeline Behavior |

---

## 🏗 Architecture / Design Patterns

This project applies **Clean Architecture** with a strict dependency rule: outer layers depend on inner layers, never the reverse.

```
┌─────────────────────────────────────────────────────────────────┐
│                        SchoolProject.Api                        │
│             Controllers · Middleware · Filters · DI             │
├─────────────────────────────────────────────────────────────────┤
│                       SchoolProject.Core                        │
│     CQRS Handlers · Validators · Mappings · Response Handler    │
├───────────────────────────┬─────────────────────────────────────┤
│    SchoolProject.Service  │   SchoolProject.Infrastructure      │
│    Business Logic ·       │   Repositories · DbContext ·        │
│    Email · File · Auth    │   Migrations · Configurations       │
├───────────────────────────┴─────────────────────────────────────┤
│                       SchoolProject.Data                        │
│             Entities · DTOs · Enums · Helpers · Results         │
└─────────────────────────────────────────────────────────────────┘
```

### Key Design Decisions

**CQRS with MediatR** — Every API operation is a `Command` (write) or `Query` (read), handled by a dedicated `IRequestHandler`. This eliminates fat controllers and makes each use case independently testable.

**FluentValidation Pipeline Behavior** — A MediatR `IPipelineBehavior<TRequest, TResponse>` intercepts every command/query before it reaches the handler. Validation failures throw a `ValidationException` that the global error middleware catches and returns as `422 Unprocessable Entity`.

**Generic Repository** — `IGenericRepositoryAsync<T>` provides consistent CRUD and transaction support across all entities. Specialized repositories extend it where needed (e.g., `IStudentRepository` adds `GetStudentListAsync`).

**Response Wrapper** — All API responses use a uniform `Response<T>` envelope containing `StatusCode`, `Succeeded`, `Message`, `Data`, `Errors`, and `Meta` — ensuring predictable client-side handling.

**AutoMapper Partial Classes** — Mapping profiles are split into partial classes per operation type (e.g., `AddStudentMapping`, `EditStudentMapping`, `GetStudentByIdMapping`), keeping each file small and focused.

---

## 📁 Project Structure

```
SchoolProject.CleanArchitechture/
├── SchoolProject.Api/               # Entry point — controllers, middleware, Program.cs
│   ├── Controllers/                 # StudentController, AuthenticationController, etc.
│   ├── Base/                        # AppControllerBase (shared MediatR + response helpers)
│   └── appsettings.json             # JWT, SMTP, Serilog, connection string config
│
├── SchoolProject.Core/              # Application layer
│   ├── Features/                    # Organized by domain → Commands + Queries + Validators
│   │   ├── Students/
│   │   ├── Departments/
│   │   ├── Instructors/
│   │   ├── Users/
│   │   ├── Authentication/
│   │   └── Authorization/
│   ├── Mapping/                     # AutoMapper profiles (partial classes per operation)
│   ├── Behaviors/                   # ValidationBehavior MediatR pipeline
│   ├── Bases/                       # Response<T>, ResponseHandler
│   ├── Filters/                     # AuthFilter (action-level role check)
│   ├── Middleware/                  # ErrorHandlerMiddleware
│   ├── Resources/                   # .resx files (en-US, ar-EG, de-DE, fr-FR)
│   └── Wrappers/                    # PaginatedResult<T>, QueryableExtensions
│
├── SchoolProject.Data/              # Domain layer — pure C# with no framework dependencies
│   ├── Entities/                    # Student, Department, Instructor, Subjects, etc.
│   │   ├── Identity/                # User, Role, UserRefreshToken
│   │   └── Views/                   # ViewDepartment (keyless entity)
│   ├── Commons/                     # GeneralLocalizableEntity (Localize helper)
│   ├── Enums/                       # StudentOrderingEnum
│   ├── Helpers/                     # JwtSettings, EmailSettings, ClaimsStore
│   ├── Requests/                    # Update role/claims request models
│   ├── Results/                     # JwtAuthResult, ManageUserRolesResult
│   └── AppMetaData/                 # Router (all route constants)
│
├── SchoolProject.Infrastructure/    # Data access layer
│   ├── Context/                     # ApplicationDbContext (IdentityDbContext)
│   ├── Configurations/              # IEntityTypeConfiguration per entity + seeding
│   ├── InfrastructureBases/         # GenericRepositoryAsync<T>
│   ├── IRepositories/               # Repository interfaces
│   ├── Repositories/                # Repository implementations + Views
│   ├── Migrations/                  # EF Core migrations
│   ├── Seeder/                      # RoleSeeder, UserSeeder
│   └── ServiceRegisteration.cs      # Identity, JWT, Authorization policies DI
│
├── SchoolProject.Service/           # Business logic layer
│   ├── Abstracts/                   # Service interfaces
│   ├── Implementations/             # StudentService, DepartmentService, AuthService, etc.
│   └── AuthServices/                # ICurrentUserService + implementation
│
├── Dockerfile                       # Multi-stage Docker build
└── docker-compose.yml               # SQL Server + API services
```

---

## 🚀 Getting Started

### Prerequisites

| Tool | Version |
|---|---|
| .NET SDK | 9.0+ |
| SQL Server | 2019+ (or Docker) |
| Docker & Docker Compose | Latest (for containerized setup) |

### Option 1 — Docker (Recommended, Zero Configuration)

```bash
# Clone the repository
git clone https://github.com/<your-username>/SchoolProject.CleanArchitechture.git
cd SchoolProject.CleanArchitechture

# Start SQL Server + API
docker compose up --build
```

The API will be available at **http://localhost:8888**  
Scalar API explorer: **http://localhost:8888/scalar/v1**

> The API runs `db.Database.Migrate()` on startup — the database and all seed data are created automatically.

---

### Option 2 — Local Development

**1. Clone the repository**
```bash
git clone https://github.com/<your-username>/SchoolProject.CleanArchitechture.git
cd SchoolProject.CleanArchitechture
```

**2. Configure the connection string**

Edit `SchoolProject.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "SchoolDb": "Server=YOUR_SERVER;Database=SchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

**3. Configure SMTP (optional — for email features)**
```json
"emailSettings": {
  "host": "smtp.gmail.com",
  "port": 465,
  "FromEmail": "your-email@gmail.com",
  "password": "your-app-password"
}
```

**4. Run the API**
```bash
cd SchoolProject.Api
dotnet run
```

The API auto-applies migrations and seeds the database on first launch.

**5. Open the API explorer**

Navigate to: `http://localhost:5247/scalar/v1`

---

### Default Admin Credentials (Seeded)

| Field | Value |
|---|---|
| Username | `budi` |
| Password | `Budi_123` |
| Email | `admin@project.com` |
| Role | `Admin` |

---

## 📡 API Reference

All routes follow the pattern: `/Api/V1/<Resource>/`

### Authentication

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/Api/V1/Authentication/SignIn/` | Obtain access + refresh token | Public |
| `POST` | `/Api/V1/Authentication/Refresh-Token/` | Rotate refresh token | Public |
| `GET` | `/Api/V1/Authentication/Validate-Token/` | Validate access token | Public |
| `GET` | `/Api/Authentication/ConfirmEmail` | Confirm email from link | Public |
| `POST` | `/Api/V1/Authentication/Send-Reset-Password-Code/` | Send password reset code | Public |
| `GET` | `/Api/V1/Authentication/Confirm-Reset-Password-Code/` | Verify reset code | Public |
| `POST` | `/Api/V1/Authentication/Reset-Password/` | Set new password | Public |

### Students

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `GET` | `/Api/V1/Student/List/` | Get all students | `User` role + `AuthFilter` |
| `GET` | `/Api/V1/Student/Paginated/` | Paginated + filtered + sorted students | `Admin` role |
| `GET` | `/Api/V1/Student/{id}` | Get student by ID | `Admin` role |
| `POST` | `/Api/V1/Student/Create/` | Create student | `CreateStudent` claim |
| `PUT` | `/Api/V1/Student/Edit/` | Update student | `EditStudent` claim |
| `DELETE` | `/Api/V1/Student/Delete/{id}` | Delete student | `DeleteStudent` claim |

### Departments

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `GET` | `/Api/V1/Department/Id/` | Get department with subjects, instructors, students | JWT |
| `GET` | `/Api/V1/Department/Department-Students-Count/` | Student count per department (via DB View) | JWT |

### Instructors

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/Api/V1/Instructor/Create/` | Add instructor with image (`multipart/form-data`) | Public |

### Users

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/Api/V1/ApplicationUser/Create/` | Register new user | `Admin` role |
| `GET` | `/Api/V1/ApplicationUser/Paginated/` | Paginated user list | Public |
| `GET` | `/Api/V1/ApplicationUser/{id}` | Get user by ID | `Admin` role |
| `PUT` | `/Api/V1/ApplicationUser/Edit/` | Update user | `Admin` role |
| `PUT` | `/Api/V1/ApplicationUser/Change-Password/` | Change password | `Admin` role |
| `DELETE` | `/Api/V1/ApplicationUser/Delete/{id}` | Delete user | `Admin` role |

### Roles & Claims

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/Api/V1/Authorization/Roles/Create/` | Create role | `Admin` role |
| `POST` | `/Api/V1/Authorization/Roles/Edit/` | Edit role | `Admin` role |
| `DELETE` | `/Api/V1/Authorization/Roles/Delete/{id}` | Delete role | `Admin` role |
| `GET` | `/Api/V1/Authorization/Roles/List/` | List all roles | `Admin` role |
| `GET` | `/Api/V1/Authorization/Roles/Manage-User-Roles/{id}` | Get user role assignments | `Admin` role |
| `POST` | `/Api/V1/Authorization/Roles/Update-User-Roles/` | Update user roles | `Admin` role |
| `GET` | `/Api/V1/Authorization/Claims/Manage-User-Claims/{id}` | Get user claim assignments | `Admin` role |
| `POST` | `/Api/V1/Authorization/Claims/Update-User-Claims/` | Update user claims | `Admin` role |

### Email

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| `POST` | `/Api/V1/Emails/SendEmail/` | Send a direct email | Public |

---

## 🔐 Security Model

### Authentication Flow

```
Client → POST /SignIn (credentials)
       ← 200 { AccessToken, RefreshToken }

Client → Any protected endpoint (Bearer <AccessToken>)
       ← 200 / 401

Client → POST /Refresh-Token (AccessToken + RefreshToken)
       ← 200 { New AccessToken, Same RefreshToken }
```

### Authorization Layers

| Layer | Mechanism | Example |
|---|---|---|
| Role-based | `[Authorize(Roles = "Admin")]` | User management endpoints |
| Claims-based | `[Authorize(Policy = "CreateStudent")]` | Create student endpoint |
| Action filter | `AuthFilter` (checks role at runtime) | Student list endpoint |

### JWT Claims Payload

Each token includes: `Name`, `NameIdentifier`, `Email`, `PhoneNumber`, `Id`, all user roles, and all user claims — enabling fine-grained authorization decisions at every layer.

### Column Encryption

The `User.Code` field (password reset code) is encrypted at rest using `EntityFrameworkCore.EncryptColumn` with AES encryption. The encryption key is managed in the `ApplicationDbContext`.

---

## 🗄 Database Design

### Core Entities

```
Department ──< DepartmentSubject >── Subjects
     │                                   │
     │                               Ins_Subject
     │                                   │
     ├──< Students >── StudentSubject    │
     │                                   │
     └──< Instructors >─────────────────┘
           │
           └── (self-referencing: SupervisorId)
           │
           └── InsManager → Department (1:1)

User ──< UserRefreshToken
```

### Database View

`ViewDepartment` is a keyless EF entity mapped to a SQL Server view that aggregates student counts per department, used by the `GetDepartmentStudentsCount` endpoint.

### Seeded Data

| Entity | Count |
|---|---|
| Departments | 3 (CS, Engineering, Science) |
| Subjects | 5 |
| Instructors | 4 |
| Students | 5 |
| Roles | 2 (Admin, User) |
| Admin User | 1 |

---

## ⚙️ Configuration

All configuration lives in `appsettings.json`. Key sections:

```json
{
  "ConnectionStrings": {
    "SchoolDb": "<connection-string>"
  },
  "jwtSettings": {
    "secret": "<min-32-char-secret>",
    "issuer": "SchoolProject",
    "audience": "WebSite",
    "AccessTokenExpireDate": 1,
    "RefreshTokenExpireDate": 20
  },
  "emailSettings": {
    "host": "smtp.gmail.com",
    "port": 465,
    "FromEmail": "<email>",
    "password": "<app-password>"
  },
  "Serilog": {
    "WriteTo": ["Console", "MSSqlServer"]
  }
}
```

In Docker, all secrets are injected via environment variables defined in `docker-compose.yml`.

---

## 🐳 Docker Deployment

The project ships with a production-ready multi-stage `Dockerfile` and a `docker-compose.yml` orchestrating both services.

```yaml
# docker-compose.yml highlights
services:
  sqlserver:         # SQL Server 2022 Developer Edition
    healthcheck: ... # API waits until DB is ready

  api:
    depends_on:
      sqlserver:
        condition: service_healthy  # Prevents startup race condition
    environment:
      ConnectionStrings__SchoolDb: "Server=sqlserver,1433;..."
```

**Multi-stage Dockerfile:**
- **Build stage**: `mcr.microsoft.com/dotnet/sdk:9.0` — restores, builds, publishes
- **Runtime stage**: `mcr.microsoft.com/dotnet/aspnet:9.0` — minimal image, copies published output and `.resx` localization files

---

## 💡 Technical Highlights

**1. MediatR Pipeline Validation** — FluentValidation runs automatically for every command/query via a registered `IPipelineBehavior`. Zero validation code in controllers or handlers.

**2. Bilingual Search with EF Core** — Student search works correctly in both Arabic and English without raw SQL, using the `Localize(ar, en)` helper method on `GeneralLocalizableEntity` that resolves based on the current thread's `CultureInfo`.

**3. Projection-based Pagination** — `ToPaginatedListAsync` extension executes `COUNT(*)` and paginated `SELECT` in two optimized queries, projecting directly to DTOs using `Expression<Func<TEntity, TDTO>>` — no over-fetching.

**4. Database View for Aggregation** — Instead of an expensive in-memory group-by, department student counts are fetched from a SQL Server view registered as a keyless EF entity.

**5. Refresh Token Rotation** — Every refresh token is stored in the database with `IsUsed`, `IsRevoked`, `ExpiryDate`, and linked JWT ID. Expired tokens are explicitly marked revoked, preventing replay.

**6. Self-Referencing Hierarchy** — The `Instructor` entity has a `SupervisorId` FK pointing to itself, correctly configured with `OnDelete(DeleteBehavior.Restrict)` to prevent cascading supervisor deletions.

**7. Transactional Operations** — Multi-step operations (user registration with email send, password reset, role updates) are wrapped in explicit `BeginTransaction` / `CommitAsync` / `RollbackAsync` blocks.

**8. Startup Health Dependency** — Docker Compose uses a SQL Server healthcheck before starting the API container, eliminating the race condition common in containerized .NET + SQL Server setups.

---

## 🔭 Future Improvements

- [ ] Add xUnit integration tests with an in-memory or Testcontainers SQL Server database
- [ ] Implement Redis caching for frequently-read department/subject data
- [ ] Add refresh token invalidation on password change (revoke all tokens)
- [ ] Introduce `IOptions<T>` pattern for typed configuration instead of singleton injection
- [ ] Add OpenTelemetry tracing for distributed observability
- [ ] Migrate image storage from local `wwwroot` to Azure Blob Storage or AWS S3
- [ ] Add a GitHub Actions CI/CD pipeline (build → test → Docker push)
- [ ] Add rate limiting middleware to authentication endpoints
- [ ] Implement soft-delete pattern for students and users

---

## 🌟 Why This Project Stands Out

Most tutorial-level APIs stop at CRUD with a database. This project goes further:

- **Real Clean Architecture** — not just folders named after layers, but enforced dependency direction with five distinct projects and proper DI wiring
- **Production authentication** — refresh token rotation, email confirmation, encrypted reset codes, and claims-based policies in one system
- **Zero-configuration startup** — auto-migration, seeding, and Docker healthchecks mean the project runs with a single `docker compose up`
- **Bilingual-first design** — localization is not an afterthought; it is built into the entity base class, response handler, and all four `.resx` files
- **Observability** — Serilog writes structured logs to both console and a SQL Server table, matching how production systems are monitored
- **Consistent API contract** — every endpoint returns the same `Response<T>` envelope, making frontend integration predictable

---

## 📸 Screenshots / Demo

> Add screenshots of the Scalar API explorer (`/scalar/v1`) demonstrating authentication flow, paginated student list, and department detail responses.

---

## 👤 Author

**Abdelrahman Mohamed** *(update with your name)*  
Backend Developer — ASP.NET Core / Clean Architecture  

- GitHub: [abdelrahmanmoamenn](https://github.com/abdelrahmanmoamenn)
- LinkedIn: [linkedin.com/in/abdelrahman-moamen-594666289](https://linkedin.com/in/abdelrahman-moamen-594666289)
- Email: abdelrahmanmoamen18@gmail.com

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

---

<p align="center">
  Built with .NET 9 · Clean Architecture · CQRS · JWT · Docker
</p>
