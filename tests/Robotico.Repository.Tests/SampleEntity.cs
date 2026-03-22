using Robotico.Domain;

namespace Robotico.Repository.Tests;

/// <summary>
/// Test entity for in-memory repository tests.
/// </summary>
public sealed class SampleEntity : IEntity<Guid>
{
    /// <inheritdoc />
    public Guid Id { get; init; }
}
