# School Management

## Dependencies

### Infrastructure

- Microsoft.EntityFrameworkCore.Design: This is the ORM for interacting with the DB
- Npgsql.EntityFrameworkCore.PostgreSQL:

## Migrations

### Start Migration

```
dotnet ef migrations add InitialCreate \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.Api \
  --output-dir Persistence/Migrations
```

What these options mean
--project SchoolManagement.Infrastructure

Tells EF Core:

The DbContext and migrations belong to Infrastructure.

--startup-project SchoolManagement.Api

Tells EF Core:

Use the API project to start the application and obtain configuration such as the connection string.

--output-dir Persistence/Migrations

### Update Database

```
dotnet ef database update \
  --project SchoolManagement.Infrastructure \
  --startup-project SchoolManagement.Api
```
