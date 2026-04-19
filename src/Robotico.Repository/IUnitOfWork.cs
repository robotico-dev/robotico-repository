namespace Robotico.Repository;

/// <summary>
/// Unit of Work: commits pending changes as a single transaction.
/// </summary>
/// <remarks>
/// <para>Use with one or more repositories; call CommitAsync once after all changes. If CommitAsync fails, the whole transaction should be rolled back (implementation-defined).</para>
/// <para>Transactional semantics differ by adapter. Inspect <see cref="IUnitOfWorkCapabilities"/> on concrete unit-of-work types for <see cref="UnitOfWorkProfile"/>.</para>
/// </remarks>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits all pending changes. Returns a failed result if the commit fails.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>Success when the transaction was committed; otherwise a failed result.</returns>
    Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default);
}
