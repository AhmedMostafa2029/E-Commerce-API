# 🛍️ E-Commerce API

A production-ready **ASP.NET Core Web API** for an E-Commerce system built using **Onion Architecture** and **Clean Architecture principles**.

The project demonstrates how to build a scalable, maintainable, and extensible backend using modern .NET development practices.

---

## 🚀 Features

- User Registration & Login
- JWT Authentication & Authorization
- Product Management
- Product Brands & Types
- Shopping Cart
- Order Management
- Stripe Payment Integration
- Redis Caching
- Pagination
- Generic Repository Pattern
- Unit of Work Pattern
- Specification Pattern
- AutoMapper
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- Dependency Injection
- Global Exception Handling
- Clean Layered Architecture

---

# 🏛️ Architecture

This project follows **Onion Architecture**, where dependencies always point inward.

```text
                +---------------------------+
                |       Presentation        |
                |      ASP.NET Core API     |
                +-------------+-------------+
                              |
                              v
                +---------------------------+
                |        Application        |
                | Business Logic & Services |
                +-------------+-------------+
                              |
                              v
                +---------------------------+
                |          Domain           |
                | Entities & Contracts      |
                +-------------+-------------+
                              ^
                              |
                +---------------------------+
                |      Infrastructure       |
                | EF Core - Identity        |
                | Redis - Stripe            |
                +---------------------------+
```

---

# 🛠️ Tech Stack

### Backend

- ASP.NET Core Web API
- C#
- .NET 10

### Database

- SQL Server
- Entity Framework Core

### Authentication

- ASP.NET Identity
- JWT

### Architecture

- Onion Architecture
- Repository Pattern
- Unit Of Work
- Specification Pattern
- Dependency Injection

### Other

- Redis
- Stripe
- AutoMapper

---

# 📂 Project Structure

```text
E-Commerce.API
│
├── E-Commerce.API
├── E-Commerce.Application
├── E-Commerce.Domain
└── E-Commerce.Infrastructure
```

### Domain

Contains:

- Entities
- Contracts
- Interfaces

---

### Application

Contains:

- DTOs
- Services
- Specifications
- Business Logic
- AutoMapper Profiles

---

### Infrastructure

Contains:

- DbContext
- Repositories
- Identity
- Redis
- Stripe
- Data Seeding

---

### API

Contains:

- Controllers
- Program.cs
- Dependency Injection
- Middleware
- Configuration

---

# 🔐 Authentication

Authentication is implemented using **JWT Bearer Token**.

Flow:

```
Register

↓

Login

↓

Generate JWT Token

↓

Send Token

↓

Authorized API Access
```

---

# 💳 Payment

Stripe is used as the payment gateway.

Features:

- Payment Intent
- Secure Payment Processing
- Order Payment Update

---

# ⚡ Redis

Redis is used for caching shopping carts to improve performance and reduce database access.

---

# 🗄️ Database

Main modules:

- Products
- Product Brands
- Product Types
- Cart
- Orders
- Delivery Methods
- Identity

---

# 🚀 Getting Started

## Clone Repository

```bash
git clone https://github.com/AhmedMostafa2029/E-Commerce-API.git
```

---

## Navigate

```bash
cd E-Commerce-API
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Update Database

```bash
dotnet ef database update
```

---

## Run

```bash
dotnet run
```

---

# ⚙️ Configuration

Create your own configuration values before running the project.

```json
{
  "ConnectionStrings": {
    "StoreDbConnection": "YOUR_CONNECTION_STRING",
    "IdentityConnection": "YOUR_CONNECTION_STRING"
  },

  "JWT": {
    "SecretKey": "YOUR_SECRET_KEY"
  },

  "Stripe": {
    "SecretKey": "YOUR_STRIPE_SECRET_KEY"
  }
}
```

---

# 📬 Main API Modules

- Authentication
- Products
- Cart
- Orders
- Payments

---

# 🔮 Future Improvements

- Refresh Tokens
- Email Verification
- Docker Support
- CI/CD Pipeline
- Unit Testing
- Integration Testing
- API Versioning
- Health Checks
- Serilog Logging
- Rate Limiting

---

# 👨‍💻 Author

Ahmed Mostafa

Backend .NET Developer

GitHub:

https://github.com/AhmedMostafa2029

LinkedIn:

https://www.linkedin.com/in/ahmed-mostafa-mohamed-720b20342

---

# 📄 License

This project is available for learning and portfolio purposes.