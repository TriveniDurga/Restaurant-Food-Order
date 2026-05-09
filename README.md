# 🍽️ FoodOrder – Fullstack Restaurant Website

FoodOrder is a simple ASP.NET MVC (.NET Framework 4.7.2) web application that allows users to register, log in, browse food items, and place food orders.

This project demonstrates:
- ASP.NET MVC architecture
- Entity Framework Database-First approach (EDMX)
- SQL Server integration
- User authentication
- Food ordering workflow

---

# 📌 Project Structure

## Project
- `TestProject`

## Controllers
- `UserController`
  - User Registration
  - User Login

- `RestaurantController`
  - Display Food Items
  - Place Orders
  - Order Success

## Database Model
- `Models/Model1.edmx`
  - Entity Framework Database-First EDMX model

## Connection String
- `AssignmentEntities`
  - Configured in `Web.config`

## Client Libraries
- Managed using `LibMan`
- CDN fallbacks included in `_Layout.cshtml`

---

# 🚀 Technologies Used

- ASP.NET MVC (.NET Framework 4.7.2)
- C#
- Entity Framework (EDMX / Database First)
- SQL Server
- Razor Views
- Bootstrap
- jQuery
- LibMan
- NUglify

---

# ✅ Prerequisites

Make sure the following tools are installed before running the project:

- Visual Studio 2022
- ASP.NET and Web Development workload
- SQL Server / SQL Server Express
- SQL Server Management Studio (SSMS) *(optional)*
- NuGet Package Manager

---

# ⚙️ Setup Instructions

## 1️⃣ Clone Repository

```bash
git clone https://github.com/TriveniDurga/Restaurant-Food-Order
```

Or open the existing local repository directly in Visual Studio.

---

## 2️⃣ Restore NuGet Packages

In Visual Studio:

- Right-click Solution
- Click **Restore NuGet Packages**

Or use Package Manager Console:

```powershell
Update-Package -reinstall
```

---

## 3️⃣ Restore Client-Side Libraries

- Right-click `libman.json`
- Select **Restore Client-Side Libraries**

> If LibMan restore fails, CDN fallbacks are already configured in `_Layout.cshtml`.

---

## 4️⃣ Install NUglify Package

Open **Package Manager Console** and run:

```powershell
Install-Package NUglify
```

This package is required to avoid WebGrease JavaScript minification issues.

---

# 🗄️ Database Setup

## Update Connection String

Open `Web.config` and update the SQL Server name:

```xml
<connectionStrings>
  <add name="AssignmentEntities"
       connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;
       provider=System.Data.SqlClient;
       provider connection string=&quot;
       data source=YOUR_SERVER_NAME;
       initial catalog=Assignment;
       integrated security=True;
       MultipleActiveResultSets=True;
       App=EntityFramework&quot;"
       providerName="System.Data.EntityClient" />
</connectionStrings>
```

Replace:

```text
YOUR_SERVER_NAME
```

with your SQL Server instance name.

Example:

```text
LAPTOP-TT04QL0P
```

---

# 🧾 Create Database and Tables

Run the following SQL script in SQL Server Management Studio (SSMS):

```sql
CREATE DATABASE Assignment;
GO

USE Assignment;
GO

CREATE TABLE Users
(
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    Password NVARCHAR(100)
);

CREATE TABLE FoodItems
(
    FoodItemId INT PRIMARY KEY IDENTITY(1,1),
    ItemName NVARCHAR(100),
    ItemType NVARCHAR(50)
);

CREATE TABLE Orders
(
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    OrderDate DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE OrderItems
(
    OrderItemId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT,
    FoodItemId INT,
    Quantity INT,

    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (FoodItemId) REFERENCES FoodItems(FoodItemId)
);
```

---

# 🍔 Insert Sample Food Items

```sql
INSERT INTO FoodItems (ItemName, ItemType)
VALUES
('Idli', 'Breakfast'),
('Dosa', 'Breakfast'),
('Poori', 'Breakfast'),
('Vada', 'Breakfast'),
('Upma', 'Breakfast'),
('Kesari', 'Breakfast'),

('Veg Meals', 'Lunch'),
('Chicken Biryani', 'Lunch'),
('Paneer Curry', 'Lunch'),
('Dal Rice', 'Lunch'),
('Fried Rice', 'Lunch'),
('Curd Rice', 'Lunch'),

('Samosa', 'Snacks'),
('Puff', 'Snacks'),
('Mirchi Bajji', 'Snacks'),
('French Fries', 'Snacks'),
('Sandwich', 'Snacks'),

('Tea', 'Beverages'),
('Coffee', 'Beverages'),
('Badam Milk', 'Beverages'),
('Lemon Juice', 'Beverages'),
('Mango Shake', 'Beverages'),

('Ice Cream', 'Dessert'),
('Gulab Jamun', 'Dessert'),
('Rasgulla', 'Dessert'),
('Chocolate Cake', 'Dessert'),
('Brownie', 'Dessert');
```

---

# ▶️ Run the Application

1. Build the solution
2. Press `F5`
3. Register a new account
4. Login
5. Place food orders

---

# ✨ Features

- User Registration
- User Login
- Food Item Management
- Food Ordering
- Order Success Page
- Entity Framework EDMX Integration
- SQL Server Database Connectivity

---

# 📷 Application Flow

1. User Registration  
2. User Login  
3. View Food Items  
4. Add Food Order  
5. Order Confirmation  

---

# 👨‍💻 Author

Developed as a sample Fullstack ASP.NET MVC Restaurant Ordering Application.
