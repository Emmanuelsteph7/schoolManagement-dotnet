# School Management System API

> 🚧 **In active development.** This project is being built as a production-style school management API using C# and .NET. The README reflects the current implementation and intended scope — update the checklist as features ship.

A backend API for managing students, teachers, classes, subjects, academic sessions, and enrollments.

The project is deliberately being built using **Clean Architecture**, domain-driven business rules, Entity Framework Core, PostgreSQL, and production-oriented API patterns. It serves as a practical project for deepening my backend engineering skills while applying the architectural principles I have developed through years of frontend engineering.

Built by [Stephen Osemene Emmanuel](https://emmanuel-stephen.onrender.com/), Senior Frontend Engineer.

[**Portfolio →**](https://emmanuel-stephen.onrender.com/) · [**GitHub →**](https://github.com/Emmanuelsteph7)

---

## Why this project

My professional experience has primarily been focused on frontend engineering — particularly React, TypeScript, Next.js, frontend architecture, and design systems.

This project is intentionally different.

Rather than building a simple CRUD API, the goal is to explore how a **real-world backend system** can be structured and evolved:

- Domain-driven business rules
- Clean Architecture
- Separation of concerns
- Application use cases
- Repository abstractions
- Entity Framework Core
- PostgreSQL
- Request validation
- Pagination, filtering, searching, and sorting
- Authentication and authorization
- Error handling
- Testing
- API documentation
- Maintainable domain models

The project is also a practical learning environment for transitioning from primarily frontend-focused engineering into **full-stack/backend engineering with C# and .NET**.

---

## Tech Stack

| Layer             | Technology                        |
| ----------------- | --------------------------------- |
| Language          | C#                                |
| Framework         | ASP.NET Core / .NET               |
| API Style         | Minimal APIs                      |
| Architecture      | Clean Architecture                |
| ORM               | Entity Framework Core             |
| Database          | PostgreSQL                        |
| Database Provider | Npgsql                            |
| Validation        | FluentValidation                  |
| API Documentation | Scalar / OpenAPI                  |
| Testing           | TBD                               |
| Authentication    | JWT / ASP.NET Core Authentication |
| Authorization     | Role-based authorization          |
| Logging           | TBD                               |
| Caching           | TBD                               |
| Deployment        | TBD                               |

---

## Features

### Teachers

- [x] Create teacher
- [x] Validate teacher input
- [x] Retrieve teacher by ID
- [x] Retrieve paginated teachers
- [x] Search teachers
- [x] Sort teachers
- [x] Filter teachers by employment status
- [x] Track creation and update timestamps
- [x] Teacher employment lifecycle
  - [x] Pending → Active
  - [x] Active → Inactive
  - [x] Active → On Leave
  - [x] Inactive → Active
  - [x] On Leave → Active

- [x] Email account verification
- [ ] Update teacher
- [ ] Delete teacher
- [ ] Filter by email account status

### Students

- [x] Create student
- [x] Validate student input
- [x] Track creation and update timestamps
- [x] Update student
- [x] Student enrollment domain logic
- [ ] Retrieve student by ID
- [ ] Retrieve paginated students
- [ ] Search students
- [ ] Sort students
- [ ] Filter students
- [ ] Delete student

### Classes

- [ ] Create class
- [ ] Retrieve class
- [ ] Retrieve classes
- [ ] Update class
- [ ] Delete class

### Subjects

- [ ] Create subject
- [ ] Retrieve subject
- [ ] Retrieve subjects
- [ ] Update subject
- [ ] Delete subject

### Academic Sessions

- [x] Academic session domain model
- [ ] Create academic session
- [ ] Retrieve academic session
- [ ] Retrieve academic sessions
- [ ] Update academic session
- [ ] Delete academic session

### Enrollment

- [x] Enrollment domain model
- [x] Prevent duplicate student enrollment within an academic session
- [ ] Enroll student
- [ ] Retrieve student enrollments
- [ ] Retrieve class enrollments
- [ ] Remove enrollment

### Platform

- [ ] Global exception handling
- [ ] Consistent API error responses
- [ ] Authentication
- [ ] Authorization
- [ ] Role management
- [ ] Logging
- [ ] Automated tests
- [ ] Integration tests
- [ ] API versioning
- [ ] Production deployment

---

## Architecture

The application follows **Clean Architecture**, separating business rules from infrastructure and API concerns.

```text
┌──────────────────────────────────────────────┐
│                    API                       │
│                                              │
│  Minimal APIs · HTTP · Validation · Scalar  │
└──────────────────────┬───────────────────────┘
                       │
                       ▼
┌──────────────────────────────────────────────┐
│                APPLICATION                   │
│                                              │
│  Use Cases · Handlers · DTOs · Abstractions │
└──────────────────────┬───────────────────────┘
                       │
                       ▼
┌──────────────────────────────────────────────┐
│                  DOMAIN                      │
│                                              │
│  Entities · Enums · Business Rules          │
│  Domain Behaviour                            │
└──────────────────────────────────────────────┘
                       ▲
                       │
┌──────────────────────┴───────────────────────┐
│               INFRASTRUCTURE                 │
│                                              │
│  EF Core · PostgreSQL · Repositories         │
│  Database Configurations · Migrations        │
└──────────────────────────────────────────────┘
```

### Dependency direction

The important architectural principle is that **business logic does not depend on infrastructure**.

For example:

```text
API
 ↓
Application
 ↓
Domain

Infrastructure
 └── implements Application abstractions
```

The Application layer can depend on Domain, but the Domain layer should not know that Entity Framework Core, PostgreSQL, or ASP.NET Core exists.

---

## Domain-Driven Behaviour

Entities are responsible for enforcing business rules that belong to them.

For example, a teacher isn't activated by simply changing:

```csharp
teacher.EmploymentStatus = EmploymentStatus.Active;
```

Instead, the domain exposes behaviour:

```csharp
teacher.ActivateTeacher();
```

The entity determines whether activation is allowed.

For example:

```text
Email Pending
      │
      ▼
 Cannot Activate
      │
      ▼
Verify Email
      │
      ▼
Email Verified
      │
      ▼
Activate Teacher
      │
      ▼
EmploymentStatus = Active
```

This keeps important business rules inside the domain rather than scattering them across API endpoints or database code.

---

## Application Layer

Application features are organized around use cases.

For example:

```text
Features/
└── Teachers/
    ├── CreateTeacher/
    │   ├── CreateTeacherHandler.cs
    │   ├── CreateTeacherRequest.cs
    │   └── CreateTeacherValidator.cs
    │
    ├── GetTeacher/
    │   ├── GetTeacherHandler.cs
    │   ├── GetTeacherRequest.cs
    │   └── TeacherResponse.cs
    │
    └── GetTeachers/
        ├── GetTeachersHandler.cs
        ├── GetTeachersRequest.cs
        └── GetTeachersValidator.cs
```

This makes each use case explicit and keeps application behaviour organized as the system grows.

---

## Query Capabilities

List endpoints are designed around common production API requirements.

For example:

```http
GET /api/teachers
```

Supports:

### Pagination

```http
GET /api/teachers?page=2&pageSize=20
```

### Searching

```http
GET /api/teachers?search=john
```

### Sorting

```http
GET /api/teachers?sortBy=FirstName&sortDirection=Asc
```

### Filtering

```http
GET /api/teachers?employmentStatus=Active
```

These capabilities can also be combined:

```http
GET /api/teachers?search=john&employmentStatus=Active&sortBy=CreatedAt&sortDirection=Desc&page=1&pageSize=20
```

The query is composed using Entity Framework Core's `IQueryable`, allowing filtering, sorting, and pagination to be translated into database queries rather than loading the entire dataset into memory.

---

## Database

The project uses:

- PostgreSQL
- Entity Framework Core
- Npgsql
- EF Core migrations

Entity configuration is separated from the entities themselves.

For example:

```text
Infrastructure/
└── Persistence/
    ├── Configurations/
    │   ├── TeacherConfiguration.cs
    │   ├── StudentConfiguration.cs
    │   └── ...
    │
    ├── Migrations/
    │
    └── SchoolManagementDbContext.cs
```

EF Core configurations are automatically discovered using:

```csharp
modelBuilder.ApplyConfigurationsFromAssembly(
    typeof(SchoolManagementDbContext).Assembly
);
```

This keeps `DbContext` focused on database access while entity-specific persistence configuration remains in dedicated configuration classes.

---

## Audit Timestamps

Entities inherit from a common `BaseEntity`:

```csharp
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public DateTimeOffset CreatedAt { get; protected set; }

    public DateTimeOffset? UpdatedAt { get; protected set; }
}
```

This provides consistent auditing information across entities.

For example:

```text
CreatedAt
    ↓
When the entity was created

UpdatedAt
    ↓
When the entity was last modified
```

`CreatedAt` is also used as a default sorting field for collection endpoints.

---

## Project Structure

```text
SchoolManagement/
│
├── SchoolManagement.Api/
│   ├── Endpoints/
│   ├── Extensions/
│   └── Program.cs
│
├── SchoolManagement.Application/
│   ├── Abstractions/
│   │   └── Persistence/
│   │
│   ├── Common/
│   │   └── Pagination/
│   │
│   └── Features/
│       ├── Teachers/
│       ├── Students/
│       ├── Classes/
│       ├── Subjects/
│       ├── AcademicSessions/
│       └── Enrollments/
│
├── SchoolManagement.Domain/
│   ├── Common/
│   ├── Entities/
│   └── Enums/
│
└── SchoolManagement.Infrastructure/
    ├── Persistence/
    │   ├── Configurations/
    │   └── Migrations/
    │
    └── Repositories/
```

---

## Getting Started

### Prerequisites

- .NET SDK
- PostgreSQL
- pgAdmin 4 or another PostgreSQL client
- Git

### Clone the repository

```bash
git clone https://github.com/Emmanuelsteph7/[repository-name].git

cd [repository-name]
```

### Configure the database

Configure the PostgreSQL connection string in the application's configuration/environment.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SchoolManagement;Username=postgres;Password=your-password"
  }
}
```

> Do not commit real database credentials to source control.

### Apply migrations

From the solution directory:

```bash
dotnet ef database update \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.Api
```

### Run the API

```bash
dotnet run --project SchoolManagement.Api
```

The API documentation is available through Scalar when running in the configured development environment.

---

## Database Migrations

When the domain model or EF Core configuration changes, create a migration:

```bash
dotnet ef migrations add MigrationName \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.Api \
  --output-dir Persistence/Migrations
```

Then apply it:

```bash
dotnet ef database update \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.Api
```

Migration files are committed to source control because they represent the database schema's evolution over time.

---

## API Documentation

The API uses **OpenAPI** with **Scalar** for interactive API documentation.

The documentation exposes:

- Available endpoints
- Request parameters
- Request bodies
- Response types
- HTTP status codes
- Validation responses
- Enum values

This makes it possible to develop and test the API without requiring a separate frontend application.

---

## Roadmap

### Phase 1 — Foundation

- [x] Solution and project structure
- [x] Clean Architecture
- [x] Domain entities
- [x] PostgreSQL integration
- [x] Entity Framework Core
- [x] Database migrations
- [x] Entity configurations
- [x] FluentValidation
- [x] Scalar / OpenAPI documentation

### Phase 2 — Core Resources

- [x] Teacher creation
- [x] Teacher retrieval
- [x] Teacher search
- [x] Teacher filtering
- [x] Teacher sorting
- [x] Teacher pagination
- [ ] Teacher updates
- [ ] Teacher deletion
- [ ] Student CRUD
- [ ] Class CRUD
- [ ] Subject CRUD
- [ ] Academic session CRUD

### Phase 3 — School Operations

- [ ] Student enrollment
- [ ] Enrollment management
- [ ] Class membership
- [ ] Teacher/class relationships
- [ ] Subject/class relationships
- [ ] Academic session management

### Phase 4 — Authentication & Authorization

- [ ] User accounts
- [ ] Password authentication
- [ ] JWT authentication
- [ ] Roles
- [ ] Permissions
- [ ] Protected endpoints
- [ ] Teacher/staff access control

### Phase 5 — Production Concerns

- [ ] Global exception handling
- [ ] Standardized error responses
- [ ] Structured logging
- [ ] Unit tests
- [ ] Integration tests
- [ ] API performance improvements
- [ ] Caching where appropriate
- [ ] API versioning
- [ ] Production deployment

### Phase 6 — Documentation & Portfolio

- [ ] Architecture documentation
- [ ] API documentation
- [ ] Database diagram
- [ ] Architecture decision records
- [ ] Technical blog post
- [ ] Production deployment

---

## Engineering Principles

The project is being developed around several principles:

### Separation of concerns

Each layer has a clear responsibility.

### Domain ownership

Business rules belong to the domain rather than being scattered across endpoints.

### Explicit use cases

Application handlers represent specific operations the system can perform.

### Persistence abstraction

Application code depends on repository abstractions rather than directly depending on EF Core.

### Thin API layer

Endpoints should primarily handle HTTP concerns and delegate application behaviour to handlers.

### Database efficiency

Queries should be executed by the database whenever possible rather than loading unnecessary data into application memory.

### Evolution over premature abstraction

Architecture is introduced when the application actually needs it rather than adding complexity without a purpose.

---

## Future Improvements

As the project grows, potential improvements include:

- Specification pattern for complex queries
- Generic pagination infrastructure
- Result/error pattern
- Domain events
- Outbox pattern
- Background jobs
- Caching
- Optimistic concurrency
- Soft deletion
- Audit logging
- Rate limiting
- Health checks
- Observability
- Containerized deployment

These will only be introduced where they solve an actual problem in the application.

---

## Author

**Stephen Osemene Emmanuel**
Senior Frontend Engineer · Full-Stack / Backend Engineering

[Portfolio](https://emmanuel-stephen.onrender.com/) · [LinkedIn](https://www.linkedin.com/in/osemenestephen/) · [GitHub](https://github.com/Emmanuelsteph7)
