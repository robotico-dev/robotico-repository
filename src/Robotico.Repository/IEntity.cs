namespace Robotico.Repository;

/// <summary>
/// Marks an entity with an identifier of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TId Id { get; }
}
