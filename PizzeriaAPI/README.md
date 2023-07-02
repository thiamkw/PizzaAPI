# Prerequisites

Microsoft Visual Studio 2022
Microsoft SQL Server or Microsoft SQL Server Express

# Setup Instructions

Before running this project, please follow these steps:

1. Open appsettings.json and change the SQL server of "PizzaApiDatabase" to your SQL server.
2. Open the Package Manager Console and run the following command "Add-Migration InitialCreate", If successful, a Migrations folder should be created.
3. Still in the Package Manager Console, run:"Update-Database". This should create the "PizzaDB" database in your SQL Server. It will also insert default data into the tables if the environment is in development mode.


# Running the API Service Locally

Running the API Service Locally

# Using Postman

If you're using Postman for testing, the endpoint URL is https://localhost:44368.