# Robotico.Repository

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![GitHub Packages](https://img.shields.io/badge/GitHub%20Packages-Robotico.Repository-blue?logo=github)](https://github.com/robotico-dev/robotico-repository-csharp/packages)
[![Build](https://github.com/robotico-dev/robotico-repository-csharp/actions/workflows/publish.yml/badge.svg)](https://github.com/robotico-dev/robotico-repository-csharp/actions/workflows/publish.yml)

Reference **Robotico.Repository** when you use the **Repository pattern** (Repository + Unit of Work). Interfaces: `IRepository<TEntity,TId>` (uses **Robotico.Domain** `IEntity<TId>`), `IUnitOfWork` (CommitAsync returns `Result`). Entity types come from **Robotico.Domain** (e.g. `Entity<TId>`).

## Robotico dependencies

```mermaid
flowchart LR
  A[Robotico.Repository] --> B[Robotico.Domain]
  A --> C[Robotico.Result]
```

## Related packages (reuse where it fits)

| Package | Use with Repository |
|---------|----------------------|
| **Robotico.Domain** | Required: entities implement `IEntity<TId>` and can extend `Entity<TId>`. |
| **Robotico.Option** | `TryGetById(id)` returning `Option<TEntity>` for "maybe present" semantics; clearer than overloading Result for "not found". |
| **Robotico.Specification** | Query by criteria: use `ISpecification<TEntity>` for Find-style operations. |
| **Robotico.Validation** | Validate entities before Add/Update (e.g. `IValidator<TEntity>`). |

## Quick start

Define an entity (using Robotico.Domain) and inject `IRepository` and `IUnitOfWork`:

```csharp
public sealed class Order : Entity<Guid> { public Order(Guid id) : base(id) { } }

// In your application code:
Result addResult = _repository.Add(order);
if (addResult.IsError(out _)) { /* handle */ }
Result commitResult = await _unitOfWork.CommitAsync(cancellationToken);
```

GetById returns a failed result when the entity is not found. See `docs/design.adoc` for design and related packages.

## Installation

```bash
dotnet add package Robotico.Repository
```

## Building and testing

```bash
dotnet restore
dotnet build -c Release
dotnet test -c Release --verbosity normal
```

With coverage (Coverlet):

```bash
dotnet test -c Release --collect:"XPlat Code Coverage" --results-directory ./coverage
```

CI enforces 90% line coverage (or passes when the library has no executable lines) and runs a trim-validate build. Versioning: SemVer.

## License

See repository license file.
