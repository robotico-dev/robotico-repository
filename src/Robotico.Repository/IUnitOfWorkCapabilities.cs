namespace Robotico.Repository;

/// <summary>
/// Exposes static semantics for a concrete <see cref="IUnitOfWork"/> so hosts can validate sagas, outbox pairing, or tests can assert expectations.
/// </summary>
/// <remarks>
/// <para>Implement this interface on unit-of-work types together with <see cref="IUnitOfWork"/>. Cast <see cref="IUnitOfWork"/> instances to <see cref="IUnitOfWorkCapabilities"/> at composition root when you need to branch on capabilities.</para>
/// </remarks>
public interface IUnitOfWorkCapabilities
{
    /// <summary>
    /// Gets the capability profile for this unit-of-work implementation.
    /// </summary>
    UnitOfWorkProfile Capabilities { get; }
}
