# 🏫 School Management System — Clean Architecture REST API

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-9.0-purple?style=for-the-badge" />
  <img src="https://img.shields.io/badge/SQL_Server-2022-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/AWS-Elastic_Beanstalk-FF9900?style=for-the-badge&logo=amazonaws&logoColor=white" />
  <img src="https://img.shields.io/badge/Docker-Containerized-2496ED?style=for-the-badge&logo=docker&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/CI%2FCD-AWS_CodeBuild-FF9900?style=for-the-badge&logo=amazonaws&logoColor=white" />
</p>

A **production-deployed** school management REST API built with **ASP.NET Core 9** following strict **Clean Architecture** principles. The system delivers CQRS via MediatR, role and claims-based authorization, JWT authentication with refresh token rotation, bilingual localization, column-level AES encryption, structured logging, containerization, and a fully automated AWS CI/CD pipeline.

**Live API Explorer →** [http://school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar](http://school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar)

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Live Deployment](#-live-deployment)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture & Design Patterns](#-architecture--design-patterns)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Reference](#-api-reference)
- [Security Model](#-security-model)
- [Database Design](#-database-design)
- [Configuration](#-configuration)
- [Docker Deployment](#-docker-deployment)
- [AWS Infrastructure & CI/CD](#-aws-infrastructure--cicd)
- [Technical Highlights](#-technical-highlights)
- [Challenges Solved](#-challenges-solved)
- [Future Improvements](#-future-improvements)
- [Why This Project Stands Out](#-why-this-project-stands-out)
- [Author](#-author)

---

## 🔍 Overview

This project is a fully-featured, **production-deployed** backend API for managing a school's core entities — students, departments, instructors, subjects, users, and roles — engineered to demonstrate enterprise-level backend development practices.

The architecture enforces strict separation of concerns across five dedicated layers. Every design decision reflects real-world production patterns: structured error handling, transactional operations, database migrations with seeding, email confirmation workflows, image uploads, multi-language support, and a live AWS deployment with an automated CI/CD pipeline that pushes code changes directly to production.

---

## 🌐 Live Deployment

| Resource | URL |
|---|---|
| API Explorer (Scalar) | [school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar](http://school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar) |
| Environment | AWS Elastic Beanstalk (eu-north-1) |
| Compute | Amazon EC2 |
| Database | Amazon RDS (SQL Server) |
| CI/CD | AWS CodeBuild — auto-deploys on every push to main |

> The API auto-migrates and seeds the database on startup. No manual database setup is required.

**Default Admin Credentials (seeded)**

| Field | Value |
|---|---|
| Username | `budi` |
| Password | `Budi_123` |
| Email | `admin@project.com` |
| Role | `Admin` |

---

## ✨ Features

### Student Management
- Full CRUD with server-side **pagination**, **search** (bilingual), and **sorting** by ID, name, address, or department
- Department-level filtering via composable IQueryable projections

### Department Management
- Department detail endpoint including subjects, instructors, and a paginated student list
- **Database View** integration for aggregated student count reporting per department

### Instructor Management
- Add instructors with **image upload** (`multipart/form-data`) stored in `wwwroot` and served via static files
- Self-referencing supervisor hierarchy with correct cascade-delete protection

### Authentication
- **JWT Bearer** token issuance with configurable expiry
- **Refresh token** rotation: tokens stored with `IsUsed`, `IsRevoked`, `JwtId`, and `ExpiryDate` — preventing replay attacks
- **Email confirmation** enforced before first login
- **Password reset** via 6-digit code delivered to email (code stored AES-encrypted at rest)
- Token validation endpoint

### Authorization
- **Role-based authorization** (`Admin` / `User` roles)
- **Claims-based policies** (`CreateStudent`, `EditStudent`, `DeleteStudent`)
- **Action-level filter** (`AuthFilter`) for runtime role verification independent of controller-level attributes

### User & Role Management
- Register, edit, delete, and paginate users
- Change password with old-password verification
- Create, edit, and delete roles
- Assign/revoke roles per user
- Assign/revoke claims per user

### Localization
- Full **multilingual support**: Arabic (`ar-EG`), English (`en-US`), German (`de-DE`), French (`fr-FR`)
- All validation messages and API responses are localized via `.resx` resource files
- Culture resolved from the `Accept-Language` request header

### Email Service
- SMTP email delivery via **MailKit**
- Email confirmation on registration with a tokenized confirmation link
- Password reset code delivery

### Logging & Observability
- **Serilog** structured logging to both **Console** and a **SQL Server** `SystemLogs` table (auto-created)
- Log-level filtering per namespace

### Developer Experience
- **Scalar UI** (modern OpenAPI explorer) available at `/scalar/v1`
- **JWT Bearer** scheme pre-configured in the OpenAPI document
- **Auto-migration and seeding** on startup — zero manual database setup

---

## 🛠 Tech Stack

| Category | Technology |
|---|---|
| Runtime | .NET 9 |
| Web Framework | ASP.NET Core 9 Web API |
| ORM | Entity Framework Core 9 |
| Database | Microsoft SQL Server 2022 |
| Cloud Database | Amazon RDS (SQL Server) |
| Hosting | AWS Elastic Beanstalk + EC2 |
| CI/CD | AWS CodeBuild |
| CQRS / Mediator | MediatR 14 |
| Validation | FluentValidation 12 |
| Mapping | AutoMapper 12 |
| Authentication | ASP.NET Core Identity + JWT Bearer |
| Encryption | EntityFrameworkCore.EncryptColumn (AES) |
| Email | MailKit 4 |
| Logging | Serilog (Console + MSSQL sink) |
| API Documentation | Scalar / OpenAPI |
| Containerization | Docker + Docker Compose |
| Architecture | Clean Architecture |
| Patterns | CQRS, Repository, Generic Repository, Mediator, Pipeline Behavior, Factory |

---

## 🏗 Architecture & Design Patterns

This project applies **Clean Architecture** with a strict inward dependency rule: outer layers depend on inner layers, never the reverse.

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

### Key Engineering Decisions

**CQRS with MediatR** — Every API operation is a `Command` (write) or `Query` (read), handled by a dedicated `IRequestHandler`. This eliminates fat controllers and makes each use case independently testable and maintainable.

**FluentValidation Pipeline Behavior** — A MediatR `IPipelineBehavior<TRequest, TResponse>` intercepts every command and query before it reaches the handler. Validation failures are caught by the global error middleware and returned as `422 Unprocessable Entity` — zero validation code in controllers.

**Generic Repository** — `IGenericRepositoryAsync<T>` provides consistent CRUD, range operations, and transaction support across all entities. Specialized repositories extend it only where domain-specific queries are required.

**Uniform Response Envelope** — All endpoints return a `Response<T>` object containing `StatusCode`, `Succeeded`, `Message`, `Data`, `Errors`, and `Meta`. This makes client-side error handling predictable and testable.

**AutoMapper Partial Classes** — Mapping profiles are split into partial classes per operation type (e.g., `AddStudentMapping`, `EditStudentMapping`, `GetStudentByIdMapping`), keeping each file focused and under 30 lines.

**Keyless Entity for DB View** — The `ViewDepartment` entity is mapped to a SQL Server view using EF Core's `[Keyless]` attribute, allowing complex aggregations to stay in the database layer without loading raw data into application memory.

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

---

### Option 1 — Docker (Recommended, Zero Configuration)

```bash
# Clone the repository
git clone https://github.com/abdelrahmanmoamenn/SchoolProject.CleanArchitechture.git
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
git clone https://github.com/abdelrahmanmoamenn/SchoolProject.CleanArchitechture.git
cd SchoolProject.CleanArchitechture
```

**2. Configure the connection string**

Edit `SchoolProject.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "SchoolDb": "Server=YOUR_SERVER;Database=SchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

**3. Configure JWT**
```json
"jwtSettings": {
  "secret": "your-minimum-32-character-secret-key",
  "issuer": "SchoolProject",
  "audience": "WebSite",
  "AccessTokenExpireDate": 1,
  "RefreshTokenExpireDate": 20
}
```

**4. Configure SMTP (optional — for email features)**
```json
"emailSettings": {
  "host": "smtp.gmail.com",
  "port": 465,
  "FromEmail": "your-email@gmail.com",
  "password": "your-app-password"
}
```

**5. Run the API**
```bash
cd SchoolProject.Api
dotnet run
```

The API auto-applies all migrations and seeds the database on first launch.

**6. Open the API explorer**

Navigate to: `http://localhost:5247/scalar/v1`

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
| Action filter | `AuthFilter` — checks role at runtime | Student list endpoint |

### JWT Claims Payload

Each token includes: `Name`, `NameIdentifier`, `Email`, `PhoneNumber`, `Id`, all user roles, and all user claims — enabling fine-grained authorization decisions at every layer.

### Column Encryption

The `User.Code` field (password reset code) is encrypted at rest using `EntityFrameworkCore.EncryptColumn` with AES encryption. The encryption key is managed within the `ApplicationDbContext`.

---

## 🗄 Database Design

### Entity Relationships

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

`ViewDepartment` is a keyless EF entity mapped to a SQL Server view that aggregates student counts per department — used by the `GetDepartmentStudentsCount` endpoint. This avoids expensive in-memory group-by operations.

### Seeded Data

| Entity | Count |
|---|---|
| Departments | 3 (CS, Engineering, Science) |
| Subjects | 5 |
| Instructors | 4 |
| Students | 5 |
| Student-Subject Enrollments | 10 |
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

In Docker and on AWS, all secrets are injected via environment variables — no credentials are hardcoded anywhere in the codebase.

---

## 🐳 Docker Deployment

The project ships with a production-ready multi-stage `Dockerfile` and a `docker-compose.yml` orchestrating both services.

```yaml
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

```bash
# Run everything with a single command
docker compose up --build
```

---

## ☁️ AWS Infrastructure & CI/CD

The application is fully deployed on AWS with a production-grade setup:

```
Developer Push → GitHub Repository
                       ↓
                 AWS CodeBuild
          (build · test · publish · package)
                       ↓
           AWS Elastic Beanstalk Deploy
                       ↓
              EC2 Instance (ASP.NET Core)
                       ↓
              Amazon RDS (SQL Server)
```

| AWS Service | Role |
|---|---|
| **Elastic Beanstalk** | Managed application hosting, auto-scaling, health monitoring |
| **EC2** | Underlying compute for the .NET runtime |
| **Amazon RDS** | Managed SQL Server database with automated backups |
| **CodeBuild** | CI/CD pipeline — builds and deploys on every push to main |

Every `git push` to the main branch triggers the CodeBuild pipeline, which compiles, packages, and deploys the application to Elastic Beanstalk automatically — achieving zero-downtime deployments without manual intervention.

---

## 💡 Technical Highlights

**1. MediatR Pipeline Validation** — FluentValidation runs automatically for every command and query via a registered `IPipelineBehavior`. Zero validation code exists in controllers or handlers; the pipeline intercepts before the handler is ever invoked.

**2. Bilingual Search with EF Core** — Student search operates correctly in both Arabic and English without raw SQL. The `Localize(ar, en)` helper method on `GeneralLocalizableEntity` resolves the correct language based on the current thread's `CultureInfo`.

**3. Projection-based Pagination** — `ToPaginatedListAsync` extension executes `COUNT(*)` and the paginated `SELECT` in two separate optimized queries, projecting directly to DTOs via `Expression<Func<TEntity, TDTO>>`. No full entities are loaded into memory.

**4. Database View for Aggregation** — Department student counts are fetched from a SQL Server view registered as a keyless EF entity, keeping expensive aggregations in the database engine rather than application memory.

**5. Refresh Token Rotation with Replay Protection** — Every refresh token is stored with `IsUsed`, `IsRevoked`, `ExpiryDate`, and the linked JWT ID. Expired tokens are explicitly marked revoked; reuse of a token after rotation is detected and blocked.

**6. AES Column Encryption** — The password reset code is encrypted at the column level via `[EncryptColumn]` — the encrypted value is persisted to the database, and the ORM transparently handles encryption and decryption.

**7. Self-Referencing Supervisor Hierarchy** — The `Instructor` entity has a `SupervisorId` FK pointing to itself, correctly configured with `OnDelete(DeleteBehavior.Restrict)` to prevent accidental cascading deletion of supervisor records.

**8. Transactional Multi-Step Operations** — Operations like user registration (create user → assign role → send email), password reset (generate code → update user → send email), and role updates are wrapped in explicit `BeginTransaction` / `CommitAsync` / `RollbackAsync` blocks to maintain data consistency.

**9. Docker Startup Race Condition Fix** — Docker Compose uses a SQL Server healthcheck (`sqlcmd SELECT 1`) before starting the API container, eliminating the race condition that commonly breaks .NET + SQL Server setups in CI/CD pipelines.

**10. AWS Auto-Migration** — `db.Database.Migrate()` runs at startup inside a scoped service, ensuring the database schema and all seed data are always in sync with the deployed application version — including on fresh RDS instances.

---

## 🧗 Challenges Solved

**Cross-origin Localization in EF Queries** — Making `Localize()` work inside `IQueryable` LINQ expressions (which are translated to SQL) required careful design. The helper is called in client-evaluated projections (`Select` after the database query) rather than inside server-side `Where` clauses to avoid translation failures.

**Circular FK Between Department and Instructor** — The `Department.InsManager` → `Instructor` and `Instructor.DID` → `Department` relationship creates a circular dependency. Resolving it required `OnDelete(DeleteBehavior.Restrict)` on both ends and careful migration ordering.

**Seeding Data with Self-Referencing FK** — Seeding instructors with supervisor relationships required inserting records in the correct dependency order (supervisors before subordinates) within EF Core's `HasData()` seeding, which does not guarantee insertion order automatically.

**Docker + SQL Server Startup Race** — Coordinating the API container to wait until SQL Server is fully initialized (not just started) required a proper `CMD-SHELL` healthcheck with `sqlcmd`, which took several iterations to configure correctly for SQL Server 2022 on Linux.

---

## 🔭 Future Improvements

- [ ] xUnit integration tests with Testcontainers (isolated SQL Server per test run)
- [ ] Redis distributed caching for department and subject read endpoints
- [ ] Refresh token revocation on password change (invalidate all active tokens)
- [ ] Rate limiting middleware on authentication endpoints (prevent brute force)
- [ ] Soft-delete pattern for students and users
- [ ] OpenTelemetry distributed tracing
- [ ] Migrate image storage to AWS S3 or Azure Blob Storage
- [ ] `IOptions<T>` pattern for strongly-typed configuration
- [ ] GitHub Actions mirroring the AWS CodeBuild pipeline

---

## 🌟 Why This Project Stands Out

Most portfolio APIs stop at CRUD with a database connection. This project goes significantly further:

**It is live on AWS.** A real production deployment with EC2, RDS, and a CI/CD pipeline that deploys on every commit — not just a `docker run` on localhost.

**Real Clean Architecture.** Not just folders named after layers, but enforced dependency direction across five dedicated C# projects with proper DI wiring and zero cross-layer leakage.

**Production-grade authentication.** Refresh token rotation with replay protection, AES-encrypted reset codes, email confirmation, and claims-based authorization policies — all in one cohesive system.

**Zero-configuration startup.** Auto-migration, seeding, Docker healthchecks, and AWS auto-deploy mean the project goes from `git clone` to running API in under five minutes.

**Bilingual by design.** Localization is built into the entity base class, the response handler, and all four `.resx` files — not added as an afterthought.

**Observability included.** Serilog writes structured logs to both the console and a SQL Server table, matching how production systems are actually monitored.

**Consistent API contract.** Every endpoint returns the same `Response<T>` envelope, making frontend integration and testing predictable and reliable.

---

## 📸 Screenshots / Demo

> **Try it live:** [http://school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar](http://school-project-env.eba-zepuqeap.eu-north-1.elasticbeanstalk.com/scalar)

The Scalar API explorer provides a fully interactive interface to authenticate, explore all endpoints, and test request/response flows without any client setup.

---

## 👤 Author

**Abdelrahman Mohamed**
Backend Developer — ASP.NET Core / Clean Architecture

- GitHub: [github.com/abdelrahmanmoamenn](https://github.com/abdelrahmanmoamenn)
- LinkedIn: [linkedin.com/in/abdelrahman-moamen-594666289](https://linkedin.com/in/abdelrahman-moamen-594666289)
- Email: abdelrahmanmoamen18@gmail.com

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

---

<p align="center">
  Built with .NET 9 · Clean Architecture · CQRS · JWT · Docker · AWS
</p>
