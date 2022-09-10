 # LobsterInk Choose Your Own Adventure
 
<br/>

This is back-end assignment for LobsterInk.

## Learn about Clean Architecture

![alt text](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)
You can find more information in this [page](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [DbUp](https://dbup.readthedocs.io/en/latest/)
* [Dapper](https://github.com/DapperLib/Dapper)
* [FluentValidation](https://fluentvalidation.net/)
* [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)
* [Docker](http://docker.com)
* [Powershell](https://docs.microsoft.com/en-us/powershell/)

## Getting Started

The easiest way to get started is to run `build-all.ps1` in the root folder. After execution of the script, the application should be ready on `http://localhost:5044/swagger/index.html`.

In case you might get a security error when running the PowerShell script, you should run the following;

```
Set-ExecutionPolicy RemoteSigned
```

### Database Configuration

When you run the application the database will be automatically created and all migration scripts will be applied.

### Database Migrations

No need to run any additional command to perform migration. The application handles it by itself.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers.

### Infrastructure

This layer contains external resources such as database, external services, and so on. These classes should be based on interfaces defined within the application layer.

### WebAPI

This layer is a presentation layer that handles HTTP calls, and depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection.

## How to test
The application could be tested via Swagger or any other rest-client tools.

***