# Robotico.Repository

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![GitHub Packages](https://img.shields.io/badge/GitHub%20Packages-Robotico.Repository-blue?logo=github)](https://github.com/robotico-dev/robotico-repository-csharp/packages)
[![Build](https://github.com/robotico-dev/robotico-repository-csharp/actions/workflows/publish.yml/badge.svg)](https://github.com/robotico-dev/robotico-repository-csharp/actions/workflows/publish.yml)

Reference **Robotico.Repository** when you use the **Repository pattern** (Repository + Unit of Work). Interfaces: `IEntity<TId>`, `IRepository<TEntity,TId>` (GetById returns `Result<TEntity>`, Add, Update, Remove), `IUnitOfWork` (CommitAsync returns `Result`).

## Robotico dependencies

```mermaid
flowchart LR
  A[Robotico.Repository] --> B[Robotico.Result]
```

## Installation

```bash
dotnet add package Robotico.Repository
```

## License

See repository license file.
