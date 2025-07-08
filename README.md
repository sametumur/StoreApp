# Store App

A modern e-commerce web application built with ASP.NET Core 6.0 and C# 10.0.

## Overview

Store App is a full-featured e-commerce platform that allows customers to browse products, add them to cart, and place orders. The application includes a comprehensive admin panel for managing products, categories, users, and orders.

## Features

### Customer Features
- Product browsing and searching
- Category-based product filtering
- Shopping cart functionality
- User registration and authentication
- User profile management
- Order placement and tracking

### Admin Features
- Dashboard with key metrics
- Product management (create, read, update, delete)
- Category management
- Order processing and fulfillment
- User and role management
- Access control based on roles

## Technology Stack

- **Framework**: ASP.NET Core 6.0
- **Language**: C# 10.0
- **Frontend**: Razor Views, Bootstrap, Font Awesome
- **Authentication**: ASP.NET Core Identity
- **Data Access**: Entity Framework Core with SQLite
- **Architecture**: MVC (Model-View-Controller)

## Project Structure

- **StoreApp**: Main application project
  - **Areas**: Contains admin area with dedicated controllers and views
  - **Controllers**: Application controllers for handling requests
  - **Views**: Razor views for rendering UI
  - **Models**: View models and data transfer objects
  - **Components**: View components for reusable UI parts
- **Entities**: Domain entities and DTOs
- **Repositories**: Data access layer
- **Services**: Business logic layer

## Getting Started

### Prerequisites

- .NET 6.0 SDK
- SQLite (already included in the project)
- Visual Studio 2022 or JetBrains Rider (recommended)

### Installation

1. Clone the repository
   ```
   git clone https://github.com/sametumur/StoreApp.git
   ```

2. Navigate to the project directory
   ```
   cd StoreApp
   ```

3. Restore dependencies
   ```
   dotnet restore
   ```

4. The application uses SQLite by default, so no additional database setup is required

5. Apply database migrations
   ```
   dotnet ef database update
   ```

6. Run the application
   ```
   dotnet run
   ```

7. Navigate to the URL shown in the console output (typically something like `https://localhost:<port>` where the port number is dynamically assigned)

## User Roles

The application has two main user roles:

- **User**: Regular customers who can browse products, make purchases, and manage their profiles
- **Admin**: Administrators who have access to the admin panel for managing the store

## Authentication

The application uses ASP.NET Core Identity for authentication and authorization. Users can:

- Register a new account
- Login with username and password
- Access their profile information
- Logout securely

Admins have additional privileges and can access the admin area.

## Admin Panel

To access the admin panel, navigate to the Admin Dashboard by clicking on the Dashboard link when logged in as an Admin user, or by adding `/Admin/Dashboard` to your application's base URL. Only users with the Admin role can access this area.

The admin panel includes:

- Dashboard with store statistics
- Product management
- Category management
- Order processing
- User and role management

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## GitHub Repository

This project is hosted on GitHub at https://github.com/sametumur/StoreApp.git

## Contact

For questions or support, please contact [umursamet@gmail.com].
