# Garage Portal - Car Service Management System

A comprehensive car service management system built with ASP.NET Core that allows administrators, mechanics, and customers to manage vehicle services efficiently.

## 🚗 Features

### Admin Features
- Manage customers and their vehicles
- Create service requests with work descriptions
- Assign mechanics to services
- View service history and status
- Add/remove vehicles from customer accounts

### Mechanic Features
- View assigned service requests
- Complete services with work descriptions and hours
- Update service status and costs
- Track work progress

### Customer Features
- View vehicle service history
- Track service status and completion
- View service costs and details

### API Features
- RESTful API for service history
- Customer management endpoints
- Service history retrieval by car ID
- Database integration with PostgreSQL

## 🛠️ Technologies Used

### Backend
- **ASP.NET Core 8.0** - Web framework
- **Entity Framework Core** - ORM for database operations
- **PostgreSQL** - Database server
- **Npgsql** - PostgreSQL provider for EF Core

### Frontend
- **Razor Pages** - Server-side rendering
- **Bootstrap 5.3.0** - CSS framework
- **Font Awesome 6.4.0** - Icons
- **JavaScript** - Client-side interactions

### Architecture
- **MVC Pattern** - Model-View-Controller architecture
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Service management

## 📋 Prerequisites

- **.NET 8.0 SDK**
- **PostgreSQL 12+**
- **Visual Studio 2022** or **VS Code**

## 🚀 Installation & Setup

### 1. Database Setup
```sql
-- Create PostgreSQL database
CREATE DATABASE GaragePortal;
```

### 2. Connection String Configuration
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=GaragePortal;Username=postgres;Password=postgres!"
  }
}
```

### 3. Database Migrations
```bash
# Navigate to Domain project
cd GaragePortal.Domain

# Apply migrations
dotnet ef database update
```

### 4. Run the Application

#### Web Application (Razor Pages)
```bash
cd GaragePortal.Razor
dotnet run
```
Access at: `http://localhost:5200`

#### API Application
```bash
cd GaragePortal.API
dotnet run
```
Access at: `http://localhost:5052`

## 👥 Default Users

The system creates default users automatically on first login:

### Admin
- **Email:** `admin@carservice.com`
- **Password:** `Dorset001^`
- **Role:** Administrator

### Mechanics
- **Email:** `mechanic1@carservice.com`
- **Password:** `Dorset001^`
- **Role:** Mechanic

- **Email:** `mechanic2@carservice.com`
- **Password:** `Dorset001^`
- **Role:** Mechanic

### Customers
- **Email:** `customer1@carservice.com`
- **Password:** `Dorset001^`
- **Role:** Customer

- **Email:** `customer2@carservice.com`
- **Password:** `Dorset001^`
- **Role:** Customer

## 📁 Project Structure

```
GaragePortal/
├── GaragePortal.Domain/          # Domain entities and DbContext
│   ├── Entities/                 # Database models
│   ├── Data/                     # DbContext and configurations
│   └── Migrations/               # Database migrations
├── GaragePortal.Razor/           # Web application (Razor Pages)
│   ├── Controllers/              # MVC controllers
│   ├── Views/                    # Razor views
│   ├── Models/                   # DTOs and view models
│   └── Interfaces/               # Service interfaces
├── GaragePortal.API/             # REST API application
│   └── Controllers/              # API controllers
└── GaragePortal.Test/            # Unit tests
```

## 🔧 API Endpoints

### Customers
- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID

### Service History
- `GET /api/servicehistory` - Get all service records
- `GET /api/servicehistory/car/{carId}` - Get services for specific car
- `GET /api/servicehistory/{id}` - Get specific service record

## 🔄 Workflow

### Service Creation Process
1. **Admin** creates service request with "Work to be done"
2. **Mechanic** receives assignment and views estimated work
3. **Mechanic** completes work and adds actual hours and description
4. **Customer** can view service history and status

### Service Status Flow
- **Pending** → Created by admin, waiting for mechanic
- **Completed** → Finished by mechanic with actual work details

## 🎨 UI Features

- **Responsive Design** - Works on desktop and mobile
- **Modern Interface** - Clean Bootstrap-based UI
- **Real-time Updates** - Dynamic content loading

## 🔒 Security Features

- **Session Management** - User authentication and authorization
- **Role-based Access** - Different views for different user types
- **Input Validation** - Form validation and sanitization
- **SQL Injection Protection** - Entity Framework parameterized queries

## 🧪 Testing

Run tests from the solution root:
```bash
dotnet test
```