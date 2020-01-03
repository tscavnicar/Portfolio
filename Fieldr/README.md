 <img align="left" width="116" height="116" src="https://github.com/tscavnicar/Portfolio/blob/master/Fieldr/logo/fieldr-logo.png" />
 
 # Fieldr - a field record making app for farmers

<br/>

This is a solution template for creating a Single Page App (SPA) with Angular 8 and ASP.NET Core 3 following the principles of Clean Architecture. Starter project is provided by [NuGet package](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) and adapted accordingly.


## Technologies
* .NET Core 3
* ASP .NET Core 3
* Entity Framework Core 3
* Angular 8

## Getting Started

1. Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download)
2. Navigate to `src/WebUI` and run `dotnet run` to launch the project

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 8 and ASP.NET Core 3. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/tscavnicar/Portfolio/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
