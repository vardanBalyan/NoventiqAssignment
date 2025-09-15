# NoventiqAssignment
Assignment project for Noventiq .Net developer

# 🚀 Getting Started

Follow the steps below to set up and run the project locally.

## 📦 Prerequisites
Make sure you have installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  
- EF Core CLI tools  
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the Repository
Clone this reposistory

### 2️⃣ Update Database Connection String
Open **`appsettings.json`** (or `appsettings.Development.json`) and configure your SQL Server connection string:

```json
"ConnectionStrings": {
  "Default": "Server=YOUR_SERVER_NAME;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

> 🔹 Replace `YOUR_SERVER_NAME` and `YourDatabaseName` with your local setup.

---

### 3️⃣ Run EF Core Migrations
Apply the migrations to create the database schema:

```bash
dotnet ef database update
```

---

### 4️⃣ Run the Project
Start the API:
```bash
dotnet run --project NoventiqAssignment.API
```

---

### 5️⃣ Test with Swagger
Once the project is running, open your browser at:

👉 [https://localhost:7037/swagger](https://localhost:7037/swagger)  

You can test all endpoints directly from Swagger UI.

---

## 🧪 Running Tests
To run the unit tests:
```bash
dotnet test
```

---

✅ That’s it! Your project should now be up and running.
