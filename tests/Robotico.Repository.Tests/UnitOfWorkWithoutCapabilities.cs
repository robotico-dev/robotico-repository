namespace Robotico.Repository.Tests;

/// <summary>Stub UoW without <see cref="Robotico.Repository.IUnitOfWorkCapabilities"/>.</summary>
public sealed class UnitOfWorkWithoutCapabilities : Robotico.Repository.IUnitOfWork
{
    public Task<Robotico.Result.Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Robotico.Result.Result.Success());
    }
}
