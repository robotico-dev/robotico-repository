namespace Robotico.Repository;

/// <summary>
/// Describes transactional and coordination semantics for a unit-of-work implementation.
/// </summary>
/// <param name="CommitMode">High-level commit behavior.</param>
/// <param name="CommitCoordinatesDomainWrites">When <see langword="true"/>, a successful commit is required for prior repository writes in this scope to become durable together. When <see langword="false"/>, writes may already be persisted per operation.</param>
/// <param name="SupportsTransactions">Whether the backing implementation can participate in transactions meaningful for the store.</param>
/// <param name="RequiresExplicitCommitForReads">When <see langword="true"/>, readers may depend on commit ordering for consistency (rare); default is <see langword="false"/>.</param>
public readonly record struct UnitOfWorkProfile(
    UnitOfWorkCommitMode CommitMode,
    bool CommitCoordinatesDomainWrites,
    bool SupportsTransactions,
    bool RequiresExplicitCommitForReads = false);
