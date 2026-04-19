namespace Robotico.Repository.Tests;

public sealed class InMemoryUnitOfWork : IUnitOfWork, IUnitOfWorkCapabilities
{
    private static readonly UnitOfWorkProfile ProfileValue = new(
        UnitOfWorkCommitMode.NoOpCommitSuccess,
        CommitCoordinatesDomainWrites: false,
        SupportsTransactions: false);

    public UnitOfWorkProfile Capabilities => ProfileValue;

    public Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Robotico.Result.Result.Success());
    }
}
