# 🎓 Student Management System API

A production-ready ASP.NET Core (.NET 8) Web API implementing JWT Authentication, Layered Architecture, Global Exception Handling, Logging, and Swagger Documentation.

---

## 🚀 Overview

This project is a Student Management System API that allows CRUD operations on students with:

- Secure access using JWT tokens
- Clean layered architecture
- Centralized error handling
- Easy testing and debugging

---

## ✨ Features

- JWT Authentication (secure APIs)
- Layered Architecture (Controller → Service → Repository)
- Global Exception Handling (Middleware)
- Logging (Console / Serilog ready)
- Swagger API Documentation
- Entity Framework Core integration
- Unit Testing using xUnit + Moq

---

## 🏗️ Architecture

API Layer (Controllers, Middleware)  
↓  
Application Layer (Business Logic)  
↓  
Domain Layer (Entities, Interfaces)  
↓  
Infrastructure Layer (Database, Repositories)

---

## 🔹 Layer Details

### API Layer
- Controllers: StudentController, AuthController
- Middleware: ExceptionMiddleware
- JWT Authentication setup
- Swagger configuration

### Application Layer
- Business logic
- IStudentService and implementation

### Domain Layer
- Student entity
- Repository interfaces

### Infrastructure Layer
- AppDbContext (EF Core)
- Repository implementation

---

## 🗄️ Database

### Current Database
- SQLite (for development)
- Entity Framework Core

### Why SQLite?
- Lightweight
- No installation required
- Easy local testing

---

## 🔄 SQL Server Support

### Install package

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### Update connection string (appsettings.json)

```
"ConnectionStrings": {
  "Default": "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Update DbContext

```csharp
options.UseSqlServer(connectionString);
```

---

## 🧾 Student Table Schema

| Column       | Type     |
|-------------|----------|
| Id          | int      |
| Name        | string   |
| Email       | string   |
| Age         | int      |
| Course      | string   |
| CreatedDate | datetime |

---

## 🔐 JWT Authentication

### Flow

1. User logs in using email
2. Server validates user
3. JWT token is generated
4. Token is returned
5. Token is used in Authorization header

---

## 📥 Login API

POST /api/auth/login

### Request

```json
{
  "email": "xyz@gmail.com"
}
```

### Response

```json
{
  "token": "your_jwt_token"
}
```

---

## 🔒 Access Protected APIs

Add header:

Authorization: Bearer <your_token>

---

## ⚠️ Global Exception Handling

Handled via ExceptionMiddleware.

### Example Response

```json
{
  "status": 404,
  "code": 404,
  "message": "Student not found"
}
```

---

## 📊 Logging

- Built-in ASP.NET Core logging
- Console logs for debugging
- Can integrate Serilog

---

## 📄 Swagger

Access:

http://localhost:5000/swagger/index.html

---

## 🛠️ Setup Instructions

### 1. Install .NET 8

Check:

```bash
dotnet --version
```

---

### 2. Clone Project

```bash
git clone <your-repo-url>
cd StudentManagementSystemFinal
```

---

### 3. Restore Packages

```bash
dotnet restore
```

---

### 4. Apply Database

```bash
cd Infrastructure
dotnet ef database update
```

---

### 5. Run API

```bash
cd ../API
dotnet run
```

---

### 6. Access API

http://localhost:5000

---

## 🧪 Testing APIs (cURL)

### Get Token

```bash
TOKEN=$(curl -s -X POST "http://localhost:5000/api/auth/login" \
-H "Content-Type: application/json" \
-d '{"email":"xyz@gmail.com"}' | jq -r '.token')
```

---

### Call Protected API

```bash
curl -X GET "http://localhost:5000/api/student" \
-H "Authorization: Bearer $TOKEN"
```

---

## 🧪 Unit Testing

### Run tests

```bash
dotnet test
```

---

## 📌 API Endpoints

### Auth

- POST /api/auth/login

### Students (Protected)

- GET /api/student
- GET /api/student/{id}
- POST /api/student
- PUT /api/student/{id}
- DELETE /api/student/{id}

---

## ✅ Technical Requirements Covered

- JWT Authentication
- Global Exception Middleware
- Logging
- Swagger Documentation
- Layered Architecture
- SQL Server Support
- Secure APIs

---

## 🎯 Final Output

- Working APIs
- Clean architecture
- Secure endpoints
- Structured error handling

---

## 🚀 Future Enhancements

- Role-based authorization
- FluentValidation
- Docker support
- CI/CD pipeline

---

## 👨‍💻 Author

Shivali Javia