# Service Oriented Architecture

## Overview

The program is developed in C#, utilizing the .NET 6 Web API template. The solution is structured into four distinct projects:

- **SOA**: Acts as the startup project. It includes the `Program.cs` file, which is the entry point of the project, along with all controllers and mapping profiles.
- **SOA.Common**: Contains all shared code used across other projects.
- **SOA.Services**: Houses all the service logic.
- **SOA.Data**: Comprises all database entities, migrations, and repositories.

To improve usability, Swagger has been integrated, providing a convenient interface to visualize and test the project's endpoints. The database is powered by MariaDB version 10.6 and is deployed using Docker, facilitating an efficient setup.

## Getting Started

### Prerequisites

- .NET 6 SDK
- Docker
- Visual Studio 2022 or later

### Setting Up the Environment

1. **Database Setup with Docker Compose**: Use the following `docker-compose.yaml` file to set up the MariaDB database:
   
  ```yaml
version: '3.8'

services:
  mysql:
    image: mysql:8.0
    container_name: mysql-container
    environment:
      MYSQL_ROOT_PASSWORD: root_password
      MYSQL_DATABASE: software-db
    ports:
      - "3306:3306"
    volumes:
      - mysql-data:/var/lib/mysql
    command: ["mysqld", "--lower_case_table_names=1"]

volumes:
  mysql-data:
  ```

Save this file as docker-compose.yaml in your project directory, and then run:

```bash
docker-compose up -d
```

### Running Migrations

1. **Open Visual Studio**: Launch Visual Studio 2022 and open the solution file.
2. **Apply Migrations**: Before running the application, apply the existing EF Core migrations to set up your database schema. This can be done using the Package Manager Console within Visual Studio, selecting **SOA.Data** project (since this contains all the migrations in it) and by running:

    ```bash
    update-database
    ```

    Alternatively, you can use the .NET CLI:

    ```bash
    dotnet ef database update
    ```

### Starting the Application

1. **Debug Mode**: Press `F5` in Visual Studio to start the application with debugging.
2. **Release Mode**: Use `Ctrl+F5` to start without debugging.

The application will launch in your default web browser, and you should be able to interact with the API through Swagger.

By following these steps, you should have a running instance of the Service Oriented Architecture application, ready for development and testing.

