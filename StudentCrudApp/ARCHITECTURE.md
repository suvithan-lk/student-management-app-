# Student Management CRUD API - Clean Architecture

## Project Structure

```
StudentCrudApp/
├── Domain/                          # Core business logic layer
│   ├── Entities/
│   │   └── Student.cs              # Student entity
│   └── Interfaces/
│       └── IStudentRepository.cs    # Repository interface
│
├── Application/                     # Business logic & use cases
│   ├── DTOs/
│   │   ├── CreateStudentDto.cs     # Input DTO for creating students
│   │   ├── UpdateStudentDto.cs     # Input DTO for updating students
│   │   └── StudentResponseDto.cs   # Output DTO for API responses
│   └── Services/
│       └── StudentService.cs        # Business logic service
│
├── Infrastructure/                  # Data access & external services
│   ├── Data/
│   │   └── StudentDbContext.cs     # Entity Framework Core context
│   └── Repositories/
│       └── StudentRepository.cs     # Data access implementation
│
├── Controllers/
│   └── StudentController.cs         # API endpoints
│
├── Migrations/                      # Database migrations
├── Program.cs                       # Dependency injection & startup
└── appsettings.json               # Configuration
```

## Architecture Layers

### 1. Domain Layer
**Purpose**: Contains core business entities and interfaces (no external dependencies)

- **Entities**: `Student.cs` - Core domain model
- **Interfaces**: Repository contracts that data access layer must implement
- **Benefits**: Technology-agnostic, testable, independent

### 2. Application Layer
**Purpose**: Business logic, orchestration, and data transformation

- **DTOs**: Data Transfer Objects for API input/output
  - `CreateStudentDto`: Request payload for creating students
  - `UpdateStudentDto`: Request payload for updating students
  - `StudentResponseDto`: Response payload with all student details
- **Services**: `StudentService` handles business logic
  - Validation (e.g., duplicate email check)
  - Entity transformation between domain and DTOs
  - Orchestrates repository calls
  - **Benefits**: Decouples API from data access

### 3. Infrastructure Layer
**Purpose**: Technical implementation of data access and external services

- **DbContext**: Entity Framework Core configuration
  - Maps domain entities to database tables
  - Enforces constraints (unique email, max lengths)
  - Default timestamps
- **Repository**: Implements `IStudentRepository`
  - CRUD operations
  - Query methods
  - **Benefits**: Swappable data access (SQL, MongoDB, etc.)

### 4. Presentation/API Layer
**Purpose**: HTTP endpoints and API contracts

- **Controllers**: Handles HTTP requests/responses
  - Input validation
  - Error handling
  - Logging
  - Status codes
  - **Benefits**: Clean separation from business logic

---

## CRUD Operations

### Create Student
**Endpoint**: `POST /api/student`
**Request**:
```json
{
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "1234567890",
  "course": "Computer Science",
  "dateOfBirth": "2000-01-15"
}
```
**Response**: `201 Created`
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "phone": "1234567890",
  "course": "Computer Science",
  "dateOfBirth": "2000-01-15",
  "createdAt": "2026-08-21T10:30:00Z",
  "updatedAt": null
}
```

### Read Student
**Endpoint**: `GET /api/student/{id}`
**Response**: `200 OK` (or `404 Not Found`)

**Endpoint**: `GET /api/student`
**Response**: `200 OK` (list of all students)

### Update Student
**Endpoint**: `PUT /api/student/{id}`
**Request**: Same as Create
**Response**: `200 OK` with updated data

### Delete Student
**Endpoint**: `DELETE /api/student/{id}`
**Response**: `204 No Content`

---

## Data Model

### Student Entity
```csharp
public class Student
{
    public int Id { get; set; }                  // Primary Key
    public string Name { get; set; }             // Max 100 chars
    public string Email { get; set; }            // Max 100 chars, Unique
    public string Phone { get; set; }            // Max 15 chars
    public string Course { get; set; }           // Max 100 chars
    public DateTime DateOfBirth { get; set; }    // Stored as DateTime
    public DateTime CreatedAt { get; set; }      // Auto-timestamp
    public DateTime? UpdatedAt { get; set; }     // Optional update timestamp
}
```

---

## Dependency Injection (Program.cs)

```csharp
// Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Service
builder.Services.AddScoped<StudentService>();

// DbContext
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseNpgsql(connectionString));
```

**Scope Types**:
- `Scoped`: New instance per HTTP request (perfect for repositories & services)
- `Transient`: New instance every time (use sparingly)
- `Singleton`: Single instance for app lifetime (use for stateless services)

---

## Database Migrations

### Running Migrations
```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Current Schema
- **Students table**: Created with Initial Create migration
- **Latest**: UpdateStudentSchema migration (proper DateTime types, unique email, UpdatedAt)

---

## Error Handling

**Service Layer** throws:
- `InvalidOperationException`: Business rule violation (e.g., duplicate email)
- `KeyNotFoundException`: Resource not found

**Controller Layer** catches and returns:
- `400 Bad Request`: Invalid input
- `404 Not Found`: Resource doesn't exist
- `500 Internal Server Error`: Unexpected error (logs details)
- Errors include: `{ "message": "Human readable error" }`

---

## Benefits of This Architecture

1. **Separation of Concerns**: Each layer has single responsibility
2. **Testability**: Easy to unit test services with mocked repositories
3. **Maintainability**: Clear structure, easy to navigate
4. **Scalability**: Can add features in Application layer without touching other layers
5. **Flexibility**: Easy to swap implementations (e.g., change database)
6. **Dependency Inversion**: Depends on interfaces, not concrete classes

---

## Next Steps

1. **Add Validation**: Use FluentValidation for robust input validation
2. **Add Logging**: Structured logging with Serilog
3. **Add Tests**: Unit tests for services, integration tests for API
4. **Add Pagination**: For GetAll endpoint
5. **Add Caching**: Redis for frequently accessed data
6. **Add API Documentation**: Swagger/OpenAPI improvements
