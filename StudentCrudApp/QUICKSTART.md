# Quick Start Guide

## Prerequisites
- .NET 8 SDK
- PostgreSQL 12+
- Visual Studio or VS Code with C# extension

## Setup Steps

### 1. Database Configuration
Edit `appsettings.json` with your PostgreSQL connection:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=StudentManagementDb;Username=postgres;Password=YOUR_PASSWORD;"
  }
}
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Apply Migrations
The app automatically runs migrations on startup. If needed, run manually:
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

The API will be available at: `https://localhost:7000` (or `http://localhost:5000`)

Swagger UI: `https://localhost:7000/swagger`

---

## API Endpoints

### Create Student
```bash
curl -X POST https://localhost:7000/api/student \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "phone": "1234567890",
    "course": "Computer Science",
    "dateOfBirth": "2000-01-15"
  }'
```

### Get All Students
```bash
curl https://localhost:7000/api/student
```

### Get Student by ID
```bash
curl https://localhost:7000/api/student/1
```

### Update Student
```bash
curl -X PUT https://localhost:7000/api/student/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "name": "John Smith",
    "email": "john.smith@example.com",
    "phone": "9876543210",
    "course": "Data Science",
    "dateOfBirth": "2000-01-15"
  }'
```

### Delete Student
```bash
curl -X DELETE https://localhost:7000/api/student/1
```

---

## Project Structure at a Glance

| Folder | Purpose |
|--------|---------|
| `Domain/` | Business entities & interfaces |
| `Application/` | DTOs & business logic services |
| `Infrastructure/` | Database context & repositories |
| `Controllers/` | API endpoints |
| `Migrations/` | Database schema versions |

---

## Key Files

| File | Purpose |
|------|---------|
| `Program.cs` | Dependency injection & startup config |
| `appsettings.json` | App settings & database connection |
| `StudentController.cs` | API endpoints |
| `StudentService.cs` | Business logic |
| `StudentRepository.cs` | Data access |

---

## Testing the API

### Using Swagger UI (Recommended)
1. Run the app: `dotnet run`
2. Open: `https://localhost:7000/swagger`
3. Try each endpoint interactively

### Using Postman
1. Import the base URL: `https://localhost:7000`
2. Create requests for each endpoint

### Using cURL
See examples above

---

## Common Issues

### Database Connection Error
- Check PostgreSQL is running
- Verify connection string in `appsettings.json`
- Ensure database credentials are correct

### Port Already in Use
Edit `Properties/launchSettings.json` to change ports

### Migration Error
```bash
# Reset database
dotnet ef database drop
dotnet ef database update
```

---

## Development Workflow

1. **Create new migration** when you change entities:
   ```bash
   dotnet ef migrations add YourMigrationName
   dotnet ef database update
   ```

2. **Add business logic** in `Application/Services/`

3. **Add API endpoints** in `Controllers/`

4. **Test via Swagger UI** or Postman

---

## Next Steps

1. Add input validation (FluentValidation)
2. Add pagination to GetAll endpoint
3. Add unit tests
4. Add logging (Serilog)
5. Add caching layer
