# StudentCrudApp - Student Management REST API

A modern, scalable ASP.NET Core 8 REST API for managing student records built with clean architecture principles. This project demonstrates best practices in API design, dependency injection, entity framework, and database management.

## 🎯 Project Overview

StudentCrudApp is a complete CRUD (Create, Read, Update, Delete) application for managing student information. It provides a RESTful API for performing all standard operations on student records with proper error handling, validation, and clean code architecture.

### Key Features

- ✅ **Full CRUD Operations**: Create, read, update, and delete student records
- ✅ **Clean Architecture**: Domain, Application, Infrastructure, and Presentation layers
- ✅ **RESTful API**: Standard HTTP methods with appropriate status codes
- ✅ **Entity Framework Core**: ORM for database operations
- ✅ **PostgreSQL Database**: Reliable relational database
- ✅ **Dependency Injection**: Built-in .NET Core DI container
- ✅ **Swagger/OpenAPI**: Auto-generated API documentation
- ✅ **Error Handling**: Comprehensive exception handling and logging
- ✅ **Data Validation**: Input validation and business rule enforcement
- ✅ **Async/Await**: Fully asynchronous API operations
- ✅ **Database Migrations**: EF Core migrations for schema versioning

---

## 🛠️ Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | ASP.NET Core | 8.0 |
| **Language** | C# | Latest |
| **Database** | PostgreSQL | 12+ |
| **ORM** | Entity Framework Core | 8.0 |
| **API Documentation** | Swagger/OpenAPI | 6.6.2 |
| **Database Driver** | Npgsql | 8.0 |

---

## 📋 Prerequisites

Before running the application, ensure you have:

- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **PostgreSQL 12 or higher** - [Download here](https://www.postgresql.org/download/)
- **Git** - For version control
- **IDE** - Visual Studio 2022, Visual Studio Code, or JetBrains Rider

---

## 🚀 Quick Start

### 1. Clone or Open the Project

```bash
# If cloned from repository
git clone <repository-url>
cd StudentCrudApp
```

### 2. Configure Database Connection

Edit `appsettings.json` with your PostgreSQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=StudentManagementDb;Username=postgres;Password=YOUR_PASSWORD;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Apply Database Migrations

Migrations are automatically applied on application startup, but you can run them manually:

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The API will start on:
- **HTTPS**: `https://localhost:7000`
- **HTTP**: `http://localhost:5000`

### 6. Access Swagger UI

Open your browser and navigate to:
```
https://localhost:7000/swagger
```

---

## 📚 API Endpoints

All endpoints return JSON responses with appropriate HTTP status codes.

### Base URL
```
https://localhost:7000/api/student
```

### Endpoints

#### 1. Create Student
**Request**
```http
POST /api/student
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "1234567890",
  "course": "Computer Science",
  "dateOfBirth": "2000-01-15"
}
```

**Response** (201 Created)
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "1234567890",
  "course": "Computer Science",
  "dateOfBirth": "2000-01-15",
  "createdAt": "2026-08-21T10:30:00Z",
  "updatedAt": null
}
```

#### 2. Get All Students
**Request**
```http
GET /api/student
```

**Response** (200 OK)
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "phone": "1234567890",
    "course": "Computer Science",
    "dateOfBirth": "2000-01-15",
    "createdAt": "2026-08-21T10:30:00Z",
    "updatedAt": null
  },
  {
    "id": 2,
    "name": "Jane Smith",
    "email": "jane.smith@example.com",
    "phone": "9876543210",
    "course": "Data Science",
    "dateOfBirth": "2001-05-20",
    "createdAt": "2026-08-21T10:35:00Z",
    "updatedAt": null
  }
]
```

#### 3. Get Student by ID
**Request**
```http
GET /api/student/1
```

**Response** (200 OK)
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "1234567890",
  "course": "Computer Science",
  "dateOfBirth": "2000-01-15",
  "createdAt": "2026-08-21T10:30:00Z",
  "updatedAt": null
}
```

**Response** (404 Not Found)
```json
{
  "message": "Student not found"
}
```

#### 4. Update Student
**Request**
```http
PUT /api/student/1
Content-Type: application/json

{
  "id": 1,
  "name": "John Smith",
  "email": "john.smith@example.com",
  "phone": "9876543210",
  "course": "Data Science",
  "dateOfBirth": "2000-01-15"
}
```

**Response** (200 OK)
```json
{
  "id": 1,
  "name": "John Smith",
  "email": "john.smith@example.com",
  "phone": "9876543210",
  "course": "Data Science",
  "dateOfBirth": "2000-01-15",
  "createdAt": "2026-08-21T10:30:00Z",
  "updatedAt": "2026-08-21T11:00:00Z"
}
```

#### 5. Delete Student
**Request**
```http
DELETE /api/student/1
```

**Response** (204 No Content)
```
[Empty body]
```

**Response** (404 Not Found)
```json
{
  "message": "Student not found"
}
```

---

## 🏗️ Architecture Overview

This project follows **Clean Architecture** principles with clear separation of concerns:

```
StudentCrudApp/
├── Domain/                      # Core business entities & interfaces
│   ├── Entities/
│   │   └── Student.cs          # Student entity
│   └── Interfaces/
│       └── IStudentRepository.cs # Repository contract
│
├── Application/                 # Business logic & use cases
│   ├── DTOs/                    # Data Transfer Objects
│   │   ├── CreateStudentDto.cs
│   │   ├── UpdateStudentDto.cs
│   │   └── StudentResponseDto.cs
│   └── Services/
│       └── StudentService.cs    # Business logic
│
├── Infrastructure/              # Data access & external services
│   ├── Data/
│   │   └── StudentDbContext.cs  # EF Core DbContext
│   └── Repositories/
│       └── StudentRepository.cs # Data access implementation
│
├── Controllers/
│   └── StudentController.cs     # API endpoints
│
├── Migrations/                  # Database schema versions
├── Program.cs                   # Startup & DI configuration
├── appsettings.json            # Configuration
└── StudentCrudApp.csproj       # Project file
```

### Architecture Layers

**Domain Layer** (No external dependencies)
- Contains core business entities (`Student`)
- Defines repository interfaces
- Represents pure business logic

**Application Layer** (Business logic)
- DTOs for input/output transformation
- `StudentService` for business operations
- Validation and business rule enforcement
- Orchestrates data access

**Infrastructure Layer** (Data access)
- Entity Framework Core configuration
- Repository implementations
- Database context and migrations
- Persistence logic

**Presentation Layer** (API)
- HTTP controllers
- Request/response handling
- Error handling and logging
- Status code management

---

## 🗄️ Data Model

### Student Entity

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| `Id` | int | Primary Key, Auto-increment | Unique student identifier |
| `Name` | string | Required, Max 100 chars | Student's full name |
| `Email` | string | Required, Max 100 chars, Unique | Student's email address |
| `Phone` | string | Required, Max 15 chars | Student's phone number |
| `Course` | string | Required, Max 100 chars | Course enrolled in |
| `DateOfBirth` | DateTime | Required | Student's date of birth |
| `CreatedAt` | DateTime | Auto-generated | Record creation timestamp |
| `UpdatedAt` | DateTime? | Nullable | Record last update timestamp |

---

## 🔧 Development

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test StudentCrudApp.Tests
```

### Creating Database Migrations

When you modify the `Student` entity:

```bash
# Create a new migration
dotnet ef migrations add MigrationName

# Apply migration to database
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove

# Revert to previous migration
dotnet ef database update PreviousMigrationName
```

### Adding New Features

1. **Add Entity** in `Domain/Entities/`
2. **Create DTOs** in `Application/DTOs/`
3. **Implement Repository** in `Infrastructure/Repositories/`
4. **Create Service** in `Application/Services/`
5. **Add Controller** in `Controllers/`
6. **Create Migration**: `dotnet ef migrations add FeatureName`

---

## 🐛 Error Handling

The API returns consistent error responses:

```json
{
  "message": "Human-readable error description"
}
```

### HTTP Status Codes

| Code | Meaning | Example |
|------|---------|---------|
| **200** | OK | Successful GET/PUT |
| **201** | Created | Successful POST |
| **204** | No Content | Successful DELETE |
| **400** | Bad Request | Invalid input data |
| **404** | Not Found | Student doesn't exist |
| **500** | Server Error | Unexpected error (check logs) |

### Common Error Scenarios

**Invalid Email Format**
```json
{
  "message": "Email format is invalid"
}
```

**Duplicate Email**
```json
{
  "message": "A student with this email already exists"
}
```

**Student Not Found**
```json
{
  "message": "Student not found"
}
```

**ID Mismatch**
```json
{
  "message": "ID mismatch"
}
```

---

## 🔒 Dependency Injection

The application uses ASP.NET Core's built-in dependency injection container configured in `Program.cs`:

```csharp
// Repository registration
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Service registration
builder.Services.AddScoped<StudentService>();

// DbContext registration
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

**Scope Types:**
- **Scoped**: New instance per HTTP request (use for repositories & services)
- **Transient**: New instance every time (use for stateless objects)
- **Singleton**: Single instance for app lifetime (use for configuration)

---

## 🚨 Troubleshooting

### Database Connection Error

**Problem:** `Exception: unable to connect to database`

**Solutions:**
1. Verify PostgreSQL is running: `psql --version`
2. Check connection string in `appsettings.json`
3. Verify username and password are correct
4. Ensure database `StudentManagementDb` exists

```bash
# Connect to PostgreSQL and create database if needed
psql -U postgres
CREATE DATABASE "StudentManagementDb";
```

### Port Already in Use

**Problem:** `System.IO.IOException: Address already in use`

**Solution:** Edit `Properties/launchSettings.json` and change the ports:
```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5001"
    },
    "https": {
      "applicationUrl": "https://localhost:7001"
    }
  }
}
```

### Migration Errors

**Problem:** Migration fails or database schema is out of sync

**Solution:** Reset database and reapply migrations:
```bash
# Drop database completely
dotnet ef database drop --force

# Reapply all migrations from scratch
dotnet ef database update
```

### Swagger Not Loading

**Problem:** Swagger UI shows no endpoints

**Solutions:**
1. Verify app is running in Development environment
2. Check that `app.UseSwagger()` and `app.UseSwaggerUI()` are called in `Program.cs`
3. Clear browser cache and refresh

---

## 📖 Additional Documentation

For deeper understanding of the project:

- **[QUICKSTART.md](StudentCrudApp/QUICKSTART.md)** - Get up and running quickly with step-by-step instructions
- **[ARCHITECTURE.md](StudentCrudApp/ARCHITECTURE.md)** - Deep dive into clean architecture principles and project design

---

## 📝 Example Usage

### Using cURL

```bash
# Create a student
curl -X POST https://localhost:7000/api/student \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Alice Johnson",
    "email": "alice@example.com",
    "phone": "5551234567",
    "course": "Software Engineering",
    "dateOfBirth": "2000-03-10"
  }'

# Get all students
curl https://localhost:7000/api/student

# Get specific student
curl https://localhost:7000/api/student/1

# Update student
curl -X PUT https://localhost:7000/api/student/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "name": "Alice Brown",
    "email": "alice.brown@example.com",
    "phone": "5559876543",
    "course": "Artificial Intelligence",
    "dateOfBirth": "2000-03-10"
  }'

# Delete student
curl -X DELETE https://localhost:7000/api/student/1
```

### Using Swagger UI

1. Start the application: `dotnet run`
2. Open: `https://localhost:7000/swagger`
3. Click on an endpoint to expand it
4. Click "Try it out" button
5. Enter parameters and click "Execute"

### Using Postman

1. Import base URL: `https://localhost:7000`
2. Create a new request for each endpoint
3. Set method (GET, POST, PUT, DELETE)
4. Add JSON body for POST/PUT requests
5. Click "Send"

---

## 🔄 Development Workflow

1. **Understand the Requirements**: What feature/fix needs to be implemented?
2. **Update Entity Model**: Modify `Student.cs` if schema changes needed
3. **Create Migration**: `dotnet ef migrations add MigrationName`
4. **Implement Business Logic**: Add methods to `StudentService.cs`
5. **Add Repository Methods**: Implement in `StudentRepository.cs`
6. **Create/Update Controller**: Add endpoints in `StudentController.cs`
7. **Add DTOs**: Create new DTOs in `Application/DTOs/` if needed
8. **Test**: Use Swagger UI or Postman to test endpoints
9. **Review & Commit**: Make sure code follows conventions and is well-documented

---

## 📋 Common Tasks

### Add a New Field to Student

1. Add property to `Student.cs`:
   ```csharp
   public string Address { get; set; } = string.Empty;
   ```

2. Create migration:
   ```bash
   dotnet ef migrations add AddAddressToStudent
   ```

3. Update `StudentDbContext.cs` if needed for constraints

4. Apply migration:
   ```bash
   dotnet ef database update
   ```

### Add Validation to Student

1. Add validation in `StudentService.cs`:
   ```csharp
   if (string.IsNullOrWhiteSpace(dto.Email))
       throw new InvalidOperationException("Email is required");
   ```

2. Return appropriate HTTP status code from controller

### Add New API Endpoint

1. Add method to `StudentService.cs`
2. Add method to controller with `[Http*]` attribute
3. Test via Swagger UI

---

## 🤝 Contributing

To contribute to this project:

1. Create a feature branch: `git checkout -b feature/YourFeature`
2. Make your changes following the existing code style
3. Ensure migrations are created for schema changes
4. Test thoroughly using Swagger UI
5. Commit with clear messages: `git commit -m "Add YourFeature"`
6. Push to branch: `git push origin feature/YourFeature`
7. Create a Pull Request with description

---

## 📄 Project Structure Summary

| Path | Purpose |
|------|---------|
| `Domain/` | Business entities & repository interfaces |
| `Application/` | DTOs & business services |
| `Infrastructure/` | Database context & repository implementations |
| `Controllers/` | API endpoints |
| `Migrations/` | Database schema versions |
| `Program.cs` | Application startup & configuration |
| `appsettings.json` | Configuration settings |
| `StudentCrudApp.csproj` | Project dependencies |

---

## 🎓 Learning Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Guide](https://docs.microsoft.com/en-us/ef/core/)
- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)

---

## 📄 License

This project is provided as-is for educational purposes.

---

## ✉️ Contact & Support

For issues, questions, or suggestions:
- Open an issue in the repository
- Check existing documentation
- Review error logs for troubleshooting

---

## 🎯 Future Enhancements

Potential features for future development:

- [ ] Add FluentValidation for robust input validation
- [ ] Implement pagination for GetAll endpoint
- [ ] Add unit & integration tests
- [ ] Add structured logging with Serilog
- [ ] Implement Redis caching
- [ ] Add authentication & authorization
- [ ] Add role-based access control (RBAC)
- [ ] Add API rate limiting
- [ ] Add search and filtering
- [ ] Add bulk operations

---

**Happy Coding! 🚀**

Last Updated: 2026-08-21
