namespace Robotico.Repository.Tests;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Robotico.Result.Result.Success());
    }
}
