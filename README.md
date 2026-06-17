# E-Commerce Book Shop Web Application

A full-featured e-commerce web application built with ASP.NET Core 8.0 MVC, featuring user authentication, role-based authorization, shopping cart functionality, order management, and payment processing.

## 🚀 Features

### Customer Features
- **User Registration & Authentication**
  - Email/Password registration with email confirmation
  - Facebook OAuth integration
  - Role-based user management (Customer, Company, Admin)
- **Product Browsing**
  - Browse products by categories
  - Detailed product views with images
  - Rich text descriptions
- **Shopping Cart**
  - Add/remove products
  - Quantity management
  - Real-time cart updates
- **Order Management**
  - Place orders with multiple items
  - Order tracking and history
  - Stripe payment integration

### Admin Features
- **Content Management**
  - Category management (CRUD operations)
  - Product management with image upload
  - Company management
- **User Management**
  - Create special accounts (Admin, Company users)
  - Manage user roles
  - View and manage all users
- **Order Management**
  - View all orders
  - Update order status
  - Payment tracking

## 🛠️ Technologies Used

- **Framework:** ASP.NET Core 8.0
- **Language:** C# 12.0
- **UI:** Razor Pages, Bootstrap 5
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Payment Processing:** Stripe
- **External Authentication:** Facebook OAuth
- **Rich Text Editor:** TinyMCE
- **Data Tables:** DataTables.js
- **Notifications:** Toastr, SweetAlert2

## 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- Stripe Account (for payment processing)
- Facebook Developer Account (for OAuth)

## 🔧 Installation & Setup

### 1. Clone the Repository

### 2. Configure Database Connection
Update the connection string in `appsettings.json`:
{ "ConnectionStrings": { "DefaultConnection": "Server=(localdb)\mssqllocaldb;Database=ASPRichDb;Trusted_Connection=true;TrustServerCertificate=true" } }

### 3. Configure Stripe Settings
Add your Stripe API keys to `appsettings.json`:

### 4. Configure Facebook Authentication
Update Facebook OAuth settings in `Program.cs` or use User Secrets:

## 🏗️ Architecture

The application follows a **layered architecture** with:
- **Presentation Layer:** MVC
- **Business Logic Layer:** Controllers and Services
- **Data Access Layer:** Repository Pattern with Unit of Work
- **Domain Layer:** Model classes

### Key Patterns Used
- **Repository Pattern:** Abstraction over data access
- **Unit of Work Pattern:** Manages transactions across repositories
- **Dependency Injection:** Built-in ASP.NET Core DI
- **Identity Framework:** User authentication and authorization

## 👥 User Roles

The application supports three user roles:

1. **Customer** - Regular users who can browse and purchase products
2. **Company** - Business users with special pricing/features
3. **Admin** - Full system access for management

## 🔐 Security Features

- Password hashing with ASP.NET Core Identity
- Email confirmation for new accounts
- Role-based authorization
- HTTPS enforcement
- CSRF protection
- Session management
- Secure cookie configuration

## 📧 Email Configuration

Configure email settings in `EmailSender.cs` to enable:
- Email confirmation
- Password reset
- Order confirmations

## 🧪 Database Seeding

The application automatically seeds:
- Default admin user
- User roles (Admin, Company, Customer)
- Sample categories and products (optional)

Database initialization occurs in `DbInitializer.cs` on application startup.

## 🚀 Deployment

### Prerequisites for Production
- Update connection strings
- Set production Stripe keys
- Configure SMTP for emails
- Set `ASPNETCORE_ENVIRONMENT` to `Production`
- Enable HTTPS
- Configure proper logging

## 📝 Usage

### Admin Access
1. Register or use seeded admin account
2. Navigate to Content Management dropdown
3. Manage Categories, Products, Companies, and Users

### Customer Flow
1. Register/Login
2. Browse products
3. Add items to cart
4. Proceed to checkout
5. Complete payment via Stripe
6. View order confirmation

- GitHub: [@Rich182182](https://github.com/Rich182182)


