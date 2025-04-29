# Motorcycle Rent
This repository contains a __C#__ __.NET Core__ application to demonstrate the use of __RabbitMq__ with __MassTransit__ implemented with __ASP.NET Core__ utilizing best practices.

## Technology Stack
  -	ASP.NET Core -v9
  - Entity Framework Core -v9
  - Minio
  - DDD (Domain-Driven Design)
  - EDD (Event-Driven Design)
  - Clean Architecture
  - Clean Code
  - Repository Design Pattern
  - CQRS Design Pattern
  - MassTransit
  - PostgreSQL Database
  - RabbitMq
  - Docker

#### Nuget Packages
  - __FluentValidation__ for server-side validation
  - __AutoMapper__ for object mapping
  - __MassTransit__ for bus management
  - __Minio__ for file management
  - __Swashbuckle__ for documentation


This repository is intended for demonstrating best practices in software development. In real-world applications, these practices should be selected based on the specific requirements of each project.


      
## Run with Docker

#### 1. Start with Docker compose

Run the following command in project directory:

```
docker-compose up -d
```

Docker compose in this application includes 4 services:

- __ASP.NET Core API__ will be listening at `http://localhost:5000`

- __Swagger__ will be listening at `http://localhost:5000/swagger/index.html`
