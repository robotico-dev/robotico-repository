using Robotico.Repository;

namespace Robotico.Repository.Tests;

/// <summary>Test double with an arbitrary <see cref="UnitOfWorkProfile"/>.</summary>
public sealed class FakeConfigurableUnitOfWork : IUnitOfWork, IUnitOfWorkCapabilities
{
    public FakeConfigurableUnitOfWork(UnitOfWorkProfile capabilities)
    {
        Capabilities = capabilities;
    }

    public UnitOfWorkProfile Capabilities { get; }

    public Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Robotico.Result.Result.Success());
    }
}
