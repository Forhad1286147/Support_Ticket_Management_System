# 🎫 Support Ticket Management System

An enterprise-grade, full-stack **Support Ticket Management System** built with **.NET 10 Web API** and **Angular 19**. Designed following **Clean Architecture** principles, strict **Role-Based Access Control (RBAC)**, and modern **Glassmorphic UI/UX design**.

---

## 🌟 Key Features & Highlights

### 🛡️ 1. Strict Role-Based Panel Isolation (RBAC)
- **1-to-1 Access Control**: Custom Angular Route Guards (`RoleGuard`) ensure users can only access their designated panel.
  - 👤 **Customer**: `/customer`
  - 🛠️ **Agent**: `/agent`
  - 👑 **Admin**: `/admin`
- Automatic redirection prevents cross-role panel access attempts.

### 🔑 2. Demo Account 1-Click Quick Login
Instant login buttons on the login screen for easy evaluation:
- 👑 **Admin**: `admin@gmail.com` | `Admin@123`
- 🛠️ **Agent**: `agent@gmail.com` | `Agent@123`
- 👤 **Customer**: `customer@gmail.com` | `Customer@123`

### 👑 3. Admin Management Panel
- **Complete CRUD Operations**:
  - 👥 **Users**: Create, Edit (Username/Email/Password reset), and Soft-Delete users with ASP.NET Identity validation.
  - 🔑 **Roles**: Create, Edit, and Delete system roles.
  - 📁 **Categories**: Create, Edit (Name & Active/Inactive toggle), and Delete support categories.
  - 🎫 **Tickets**: View all tickets, update status, edit details, or soft-delete.

### 🛠️ 4. Agent Support Panel
- Real-time ticket management queue.
- Filter tickets by status (*Open*, *In Progress*, *Resolved*) and priority (*Low*, *Medium*, *High*).
- Interactive messaging thread to reply directly to customer ticket comments.

### 👤 5. Customer Portal
- Simplified ticket submission wizard with priority selection.
- Active ticket status tracker.
- Real-time conversation thread with assigned support agents.

### 🗑️ 6. Soft Delete Architecture (`IsDeleted`)
- Global EF Core Query Filters (`HasQueryFilter(x => !x.IsDeleted)`) across all entities: `Ticket`, `Category`, `TicketComment`, `Notification`.
- Deleting any record updates `IsDeleted = true` in SQL Server, preserving data integrity while automatically hiding deleted records from API endpoints and UI lists.

---

## 🏗️ Architecture & Technology Stack

```
Support_Ticket_Management_System/
├── backend/
│   └── src/
│       ├── Support_Ticket_Management_System_Domain/        # Core Entities & Value Objects
│       ├── Support_Ticket_Management_System_Application/   # DTOs, Service Contracts & Interfaces
│       ├── Support_Ticket_Management_System_Infrastucture/ # EF Core, DB Context, Repositories, Migrations
│       └── Support_Ticket_Management_System_Api/            # Web API Controllers, JWT & Middleware
└── frontend/                                                # Angular 19 Single Page Application
```

### 💻 Tech Stack
- **Backend Framework**: .NET 10 (ASP.NET Core Web API)
- **Database ORM**: Entity Framework Core 10 (SQL Server)
- **Authentication**: ASP.NET Core Identity & JWT Bearer Tokens
- **API Documentation**: Scalar API Reference / OpenAPI (`/scalar/v1`)
- **Frontend Framework**: Angular 19 (TypeScript, RxJS)
- **Styling**: Vanilla CSS (Custom Design Tokens, Glassmorphism, Dark Aesthetic)

---

## 🚀 Getting Started & How to Run

### 📋 Prerequisites
1. [.NET 10 SDK](https://dotnet.microsoft.com/download)
2. [Node.js (v18 or higher)](https://nodejs.org/) & `npm`
3. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or Full Instance)

---

### ⚙️ 1. Backend Setup (.NET 10 Web API)

1. Navigate to the API directory:
   ```bash
   cd backend/src/Support_Ticket_Management_System_Api
   ```

2. Configure your SQL Server Connection String in `appsettings.json` (if needed):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SupportTicketDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. Run the backend API:
   ```bash
   dotnet run
   ```
   > ℹ️ **Note**: On startup, `DbInitializer` will automatically create the database, apply pending EF Core migrations, and seed default roles and demo accounts (`Admin`, `Agent`, `Customer`).

4. Access Interactive API Documentation:
   - Scalar API UI: `https://localhost:7085/scalar/v1` or `http://localhost:5043/scalar/v1`

---

### 💻 2. Frontend Setup (Angular 19)

1. Open a new terminal and navigate to the `frontend` folder:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the Angular development server:
   ```bash
   npm start
   # or
   ng serve
   ```

4. Open your browser and navigate to:
   ```
   http://localhost:4200
   ```

---

## 🧪 Verification & Testing Checklist

| Test Scenario | Action / Expected Result | Status |
| :--- | :--- | :---: |
| **Demo Quick Login** | Click "👑 Admin Quick Login" on `/login` to auto-fill & sign in | ✅ Pass |
| **Strict Role Isolation** | Login as Customer and try navigating to `/admin`. Auto-redirects to `/customer` | ✅ Pass |
| **Admin User Creation** | Navigate to Users tab in Admin panel -> Add User with password (e.g., `Test@123`) | ✅ Pass |
| **Messaging & Comments** | Customer sends message on ticket; Agent sees comment in Agent Panel thread | ✅ Pass |
| **Soft Delete** | Admin deletes a ticket or category -> Row remains in DB with `IsDeleted=1`, hidden from UI | ✅ Pass |

---

## 📄 License
This project is developed for technical evaluation and interview assessment purposes.
