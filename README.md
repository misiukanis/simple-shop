# A simple SHOP

![shop](screen.PNG)

## Architecture

- Domain-driven design (DDD)
- Event Sourcing
- Clean Architecture
- CQRS

![shop](diagram_transparent.PNG)

## Technologies
- .NET 7.0
- C# 11
- Blazor WebAssembly App
- ASP.NET Core Web API
- MongoDB
- Event Store
- AutoMapper
- NLog
- MediatR
- FluentValidation


## Main layers

| Layer | Description |
| ------ | ------ |
| Shop.Client | Blazor application |
| Shop.Server | API |
| Shop.Application | Communication with Domain Layer |
| Shop.Infrastructure | Persistence |
| Shop.Domain | Core business logic |


## How to run the application
1. Download and run Event Store.
2. Create a cloud database on https://www.mongodb.com/ (it is free) and fill in appsettings.js in Shop.Server:
```MongoDbConnectionString```
```MongoDbDatabaseName```
3. Launch the application!


## About the Author
Michał Misiukanis
[Linkedin](https://www.linkedin.com/in/micha%C5%82-misiukanis-875129119/)