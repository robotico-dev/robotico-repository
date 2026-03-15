using Robotico.Repository;
using Xunit;

namespace Robotico.Repository.Tests;

public sealed class SampleEntity : IEntity<Guid>
{
    public Guid Id { get; init; }
}

public sealed class RepositoryTests
{
    [Fact]
    public void IEntity_exposes_Id()
    {
        var id = Guid.NewGuid();
        IEntity<Guid> entity = new SampleEntity { Id = id };
        Assert.Equal(id, entity.Id);
    }
}
