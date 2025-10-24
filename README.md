# 🌐 EventHub – Event Management System (ASP.NET Core MVC)

**EventHub** is a web-based event management system built using **ASP.NET Core MVC** and **Entity Framework Core**. The system allows users to browse and register for events, while administrators can manage events, users, and the overall website.

---

## 📌 Features

### 👤 User Features
- View a list of upcoming events
- Register and unregister for events
- Create a user account and log in
- Access multi-language support (English, Dutch, French)

### 🛠️ Administrator Features
- Create, edit, and delete events
- Manage registered users and their profiles
- Assign roles and control permissions
- Perform general site maintenance tasks

---

## ⚙️ Technical Overview

| Area                     | Description                                                                 |
|--------------------------|-----------------------------------------------------------------------------|
| **Framework**            | ASP.NET Core MVC (likely version 8)                                         |
| **Database**             | Entity Framework Core                                                       |
| **Asynchronous Access**  | All database operations use async/await for performance                     |
| **User Management**      | ASP.NET Core Identity Framework for roles, authentication & authorization  |
| **UI/UX**                | Clean and user-friendly interface with intuitive navigation                 |
| **AJAX**                 | For dynamic data fetching (e.g., event details without page reload)         |
| **Pagination**           | Event listings are paginated with sorting and filtering                     |
| **Validation**           | Data annotations for input validation (e.g., `[Required]`, `[Range]`)       |
| **Middleware**           | Cookie-based authentication, rate limiting implemented                     |
| **Localization**         | Supports English, Dutch, and French languages                               |

---

## 🧱 Project Structure

- `Controllers/` – Handles routing logic and page flow  
- `Views/` – Razor views for UI rendering  
- `Models/` – Entity and ViewModel classes  
- `Data/` – Database context and seed data  
- `wwwroot/` – Static assets like CSS, JS, and images  
- `Services/` – Custom logic (e.g., event registration)  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK or later](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022+ with ASP.NET and web development workload
- SQL Server or LocalDB
- Git (optional, for cloning)


