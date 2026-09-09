# Student Management App

A student management CRUD API built with ASP.NET Core Web API, Entity Framework Core, and PostgreSQL.

## Features

- Create, read, update, and delete students
- DTO validation using DataAnnotations
- Unique email validation
- PostgreSQL database integration
- Swagger API documentation in Development
- Entity Framework Core migrations

## Tech Stack

- .NET 8+
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Npgsql

## Requirements

- .NET SDK 8 or later
- PostgreSQL 14 or later

## Configuration

Create `StudentCrudApp/appsettings.Development.json` locally:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=StudentManagementDb;Username=postgres;Password=YOUR_PASSWORD;"
  }
}
```

Never commit real database passwords or production connection strings.

## Run the API

```bash
dotnet restore
dotnet build
dotnet run --project StudentCrudApp
```

When running in Development, the application applies pending EF Core migrations automatically.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/student` | Get all students |
| GET | `/api/student/{id}` | Get one student |
| POST | `/api/student` | Create a student |
| PUT | `/api/student/{id}` | Update a student |
| DELETE | `/api/student/{id}` | Delete a student |

## Example Request

```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "+94771234567",
  "course": "Software Engineering",
  "dateOfBirth": "2000-01-15"
}
```

## Database Migrations

Create a migration:

```bash
dotnet ef migrations add InitialCreate --project StudentCrudApp
```

Apply migrations manually for production deployments:

```bash
dotnet ef database update --project StudentCrudApp
```
