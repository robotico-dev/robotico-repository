namespace Robotico.Repository;

/// <summary>
/// Declares what the application requires from a concrete <see cref="IUnitOfWork"/> implementation.
/// Pass to <see cref="UnitOfWorkGuard.Require"/> at startup when sagas, outbox, or multi-step consistency depend on specific semantics.
/// </summary>
[System.Flags]
public enum UnitOfWorkRequirement
{
    /// <summary>No validation (no-op).</summary>
    None = 0,

    /// <summary>
    /// Domain writes must remain non-durable until <see cref="IUnitOfWork.CommitAsync(System.Threading.CancellationToken)"/> coordinates persistence
    /// (EF-style deferred flush, Mongo session transaction, etc.). Fails for no-op or immediate-per-call adapters.
    /// </summary>
    DeferredUntilCommit = 1,

    /// <summary>
    /// Store-level transactions must coordinate multiple steps (same session / transaction). Requires both transaction support and coordinated commits.
    /// </summary>
    CrossAggregateAtomicity = 2,
}
