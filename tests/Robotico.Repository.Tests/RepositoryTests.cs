using Robotico.Domain;
using Robotico.Repository;
using Xunit;

namespace Robotico.Repository.Tests;

public sealed class RepositoryTests
{
    [Fact]
    public void IEntity_exposes_Id()
    {
        Guid id = Guid.NewGuid();
        IEntity<Guid> entity = new SampleEntity { Id = id };
        Assert.Equal(id, entity.Id);
    }

    [Fact]
    public void GetById_after_Add_returns_same_entity()
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        SampleEntity entity = new() { Id = Guid.NewGuid() };
        Robotico.Result.Result addResult = repo.Add(entity);
        Assert.True(addResult.IsSuccess());

        Robotico.Result.Result<SampleEntity> getResult = repo.GetById(entity.Id);
        Assert.True(getResult.IsSuccess(out SampleEntity? retrieved));
        Assert.Same(entity, retrieved);
    }

    [Fact]
    public void GetById_for_missing_id_returns_failed_result()
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        Robotico.Result.Result<SampleEntity> result = repo.GetById(Guid.NewGuid());
        Assert.True(result.IsError(out _));
    }

    [Fact]
    public void Update_then_GetById_returns_updated_entity()
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        SampleEntity entity = new() { Id = Guid.NewGuid() };
        repo.Add(entity);
        Robotico.Result.Result updateResult = repo.Update(entity);
        Assert.True(updateResult.IsSuccess());
        Robotico.Result.Result<SampleEntity> getResult = repo.GetById(entity.Id);
        Assert.True(getResult.IsSuccess(out SampleEntity? retrieved));
        Assert.Same(entity, retrieved);
    }

    [Fact]
    public void Remove_then_GetById_returns_not_found()
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        SampleEntity entity = new() { Id = Guid.NewGuid() };
        repo.Add(entity);
        Robotico.Result.Result removeResult = repo.Remove(entity);
        Assert.True(removeResult.IsSuccess());
        Robotico.Result.Result<SampleEntity> getResult = repo.GetById(entity.Id);
        Assert.True(getResult.IsError(out _));
    }

    [Fact]
    public void Add_throws_on_null_entity()
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        Assert.Throws<ArgumentNullException>(() => repo.Add(null!));
    }

    [Fact]
    public async Task CommitAsync_returns_success()
    {
        InMemoryUnitOfWork uow = new();
        Robotico.Result.Result result = await uow.CommitAsync();
        Assert.True(result.IsSuccess());
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Repository_law_Add_then_GetById_yields_same_instance(bool removeBeforeGet)
    {
        InMemoryRepository<SampleEntity, Guid> repo = new();
        SampleEntity entity = new() { Id = Guid.NewGuid() };
        repo.Add(entity);
        if (removeBeforeGet)
        {
            repo.Remove(entity);
        }

        Robotico.Result.Result<SampleEntity> getResult = repo.GetById(entity.Id);
        Assert.Equal(!removeBeforeGet, getResult.IsSuccess(out _));
    }
}
