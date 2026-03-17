using Robotico.Domain;

namespace Robotico.Repository;

/// <summary>
/// Repository for entities of type <typeparamref name="TEntity"/> with identifier <typeparamref name="TId"/>.
/// </summary>
/// <remarks>
/// <para>Uses <see cref="IEntity{TId}"/> from Robotico.Domain so that domain entities (e.g. <see cref="Robotico.Domain.Entity{TId}"/>) work with repositories without an extra abstraction.</para>
/// </remarks>
/// <typeparam name="TEntity">The entity type (must implement <see cref="IEntity{TId}"/> from Robotico.Domain).</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets an entity by its identifier. Returns a failed result if not found.
    /// </summary>
    /// <param name="id">The entity identifier. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <returns>Success with the entity when found; otherwise a failed result (e.g. not found).</returns>
    Robotico.Result.Result<TEntity> GetById(TId id);

    /// <summary>
    /// Adds the entity.
    /// </summary>
    /// <param name="entity">The entity to add. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <returns>Success when the entity was added; otherwise a failed result (e.g. constraint violation).</returns>
    Robotico.Result.Result Add(TEntity entity);

    /// <summary>
    /// Updates the entity.
    /// </summary>
    /// <param name="entity">The entity to update. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <returns>Success when the entity was updated; otherwise a failed result (e.g. not found, concurrency).</returns>
    Robotico.Result.Result Update(TEntity entity);

    /// <summary>
    /// Removes the entity.
    /// </summary>
    /// <param name="entity">The entity to remove. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <returns>Success when the entity was removed; otherwise a failed result (e.g. not found).</returns>
    Robotico.Result.Result Remove(TEntity entity);
}
