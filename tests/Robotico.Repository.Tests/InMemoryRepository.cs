using System.Collections.Concurrent;
using Robotico.Domain;
using Robotico.Result.Errors;

namespace Robotico.Repository.Tests;

public sealed class InMemoryRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
    private readonly ConcurrentDictionary<TId, TEntity> _store = new();

    public Robotico.Result.Result<TEntity> GetById(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if (_store.TryGetValue(id, out TEntity? entity))
        {
            return Robotico.Result.Result.Success(entity);
        }

        return Robotico.Result.Result.Error<TEntity>(new SimpleError($"Entity with id '{id}' not found.", "NOT_FOUND"));
    }

    public Robotico.Result.Result Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (!_store.TryAdd(entity.Id, entity))
        {
            return Robotico.Result.Result.Error(new SimpleError($"Entity with id '{entity.Id}' already exists.", "DUPLICATE"));
        }

        return Robotico.Result.Result.Success();
    }

    public Robotico.Result.Result Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _store[entity.Id] = entity;
        return Robotico.Result.Result.Success();
    }

    public Robotico.Result.Result Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (_store.TryRemove(entity.Id, out _))
        {
            return Robotico.Result.Result.Success();
        }

        return Robotico.Result.Result.Error(new SimpleError($"Entity with id '{entity.Id}' not found.", "NOT_FOUND"));
    }
}
