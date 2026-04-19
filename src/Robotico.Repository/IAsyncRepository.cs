using Robotico.Domain;

namespace Robotico.Repository;

/// <summary>
/// Asynchronous repository for entities of type <typeparamref name="TEntity"/> with identifier <typeparamref name="TId"/>.
/// </summary>
/// <remarks>
/// <para>Prefer this interface in async hosts (ASP.NET Core, workers) to avoid blocking thread-pool threads on I/O. The synchronous <see cref="IRepository{TEntity, TId}"/> remains supported for legacy code and in-memory paths.</para>
/// <para>Semantics match <see cref="IRepository{TEntity, TId}"/>; only the execution model differs.</para>
/// </remarks>
/// <typeparam name="TEntity">The entity type (must implement <see cref="IEntity{TId}"/> from Robotico.Domain).</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IAsyncRepository<TEntity, TId>
    where TEntity : IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets an entity by its identifier. Returns a failed result if not found.
    /// </summary>
    /// <param name="id">The entity identifier. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Success with the entity when found; otherwise a failed result (e.g. not found).</returns>
    Task<Robotico.Result.Result<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the entity.
    /// </summary>
    /// <param name="entity">The entity to add. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Success when the entity was added; otherwise a failed result (e.g. constraint violation).</returns>
    Task<Robotico.Result.Result> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the entity.
    /// </summary>
    /// <param name="entity">The entity to update. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Success when the entity was updated; otherwise a failed result (e.g. not found, concurrency).</returns>
    Task<Robotico.Result.Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the entity.
    /// </summary>
    /// <param name="entity">The entity to remove. Must not be null. Implementations must throw <see cref="ArgumentNullException"/> if null.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Success when the entity was removed; otherwise a failed result (e.g. not found).</returns>
    Task<Robotico.Result.Result> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
}
