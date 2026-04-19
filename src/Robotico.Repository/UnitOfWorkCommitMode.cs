namespace Robotico.Repository;

/// <summary>
/// Describes how <see cref="IUnitOfWork.CommitAsync(System.Threading.CancellationToken)"/> relates to persistence for a given adapter.
/// </summary>
public enum UnitOfWorkCommitMode
{
    /// <summary>
    /// Domain writes are staged until commit (e.g. EF Core <c>SaveChanges</c>, ADO.NET transaction commit).
    /// </summary>
    DeferredUntilCommit,

    /// <summary>
    /// A session-scoped transaction commits on <see cref="IUnitOfWork.CommitAsync(System.Threading.CancellationToken)"/> (e.g. MongoDB client session).
    /// </summary>
    SessionScopedTransaction,

    /// <summary>
    /// Commit returns success but does not coordinate writes that already reached the store, or no transactional batch is active.
    /// </summary>
    NoOpCommitSuccess,
}
