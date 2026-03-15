namespace Robotico.Repository;

/// <summary>
/// Unit of Work: commits pending changes as a single transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits all pending changes. Returns a failed result if the commit fails.
    /// </summary>
    Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default);
}
