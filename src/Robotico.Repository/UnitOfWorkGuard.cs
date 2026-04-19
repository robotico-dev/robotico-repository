namespace Robotico.Repository;

/// <summary>
/// Validates a concrete <see cref="IUnitOfWork"/> against <see cref="UnitOfWorkRequirement"/> at composition root or startup.
/// </summary>
public static class UnitOfWorkGuard
{
    /// <summary>
    /// Throws <see cref="InvalidOperationException"/> when <paramref name="unitOfWork"/> does not satisfy <paramref name="required"/>.
    /// </summary>
    /// <param name="unitOfWork">The unit of work resolved from DI.</param>
    /// <param name="required">Required semantics; <see cref="UnitOfWorkRequirement.None"/> is a no-op.</param>
    /// <exception cref="InvalidOperationException">Thrown when requirements are not met or capabilities cannot be inspected.</exception>
    public static void Require(IUnitOfWork unitOfWork, UnitOfWorkRequirement required)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        if (required == UnitOfWorkRequirement.None)
        {
            return;
        }

        if (unitOfWork is not IUnitOfWorkCapabilities capabilities)
        {
            throw new InvalidOperationException(
                "Unit of work validation failed: the concrete type does not implement IUnitOfWorkCapabilities, so adapter semantics cannot be verified. " +
                "Use an adapter that exposes UnitOfWorkProfile or remove UnitOfWorkGuard.Require for this host.");
        }

        UnitOfWorkProfile profile = capabilities.Capabilities;
        if (required.HasFlag(UnitOfWorkRequirement.DeferredUntilCommit))
        {
            if (!profile.CommitCoordinatesDomainWrites || profile.CommitMode == UnitOfWorkCommitMode.NoOpCommitSuccess)
            {
                throw new InvalidOperationException(
                    "Unit of work validation failed: DeferredUntilCommit was required, but this adapter commits per operation or uses a no-op CommitAsync. " +
                    DescribeProfile(profile));
            }
        }

        if (required.HasFlag(UnitOfWorkRequirement.CrossAggregateAtomicity))
        {
            if (!profile.SupportsTransactions || !profile.CommitCoordinatesDomainWrites)
            {
                throw new InvalidOperationException(
                    "Unit of work validation failed: CrossAggregateAtomicity was required, but this adapter does not expose coordinated transactional commits. " +
                    DescribeProfile(profile));
            }
        }
    }

    private static string DescribeProfile(UnitOfWorkProfile profile)
    {
        return string.Format(
            null,
            "Actual profile: CommitMode={0}, CommitCoordinatesDomainWrites={1}, SupportsTransactions={2}.",
            profile.CommitMode,
            profile.CommitCoordinatesDomainWrites,
            profile.SupportsTransactions);
    }
}
