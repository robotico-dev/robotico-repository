namespace Robotico.Repository;

/// <summary>
/// Repository for entities of type <typeparamref name="TEntity"/> with identifier <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets an entity by its identifier. Returns a failed result if not found.
    /// </summary>
    Robotico.Result.Result<TEntity> GetById(TId id);

    /// <summary>
    /// Adds the entity.
    /// </summary>
    Robotico.Result.Result Add(TEntity entity);

    /// <summary>
    /// Updates the entity.
    /// </summary>
    Robotico.Result.Result Update(TEntity entity);

    /// <summary>
    /// Removes the entity.
    /// </summary>
    Robotico.Result.Result Remove(TEntity entity);
}
