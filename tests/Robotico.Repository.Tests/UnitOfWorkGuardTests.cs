using Robotico.Repository;
using Xunit;

namespace Robotico.Repository.Tests;

public sealed class UnitOfWorkGuardTests
{
    [Fact]
    public void Require_None_does_not_throw_for_in_memory()
    {
        InMemoryUnitOfWork uow = new();
        UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.None);
    }

    [Fact]
    public void Require_DeferredUntilCommit_throws_for_in_memory()
    {
        InMemoryUnitOfWork uow = new();
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.DeferredUntilCommit));
        Assert.Contains("DeferredUntilCommit", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void Require_DeferredUntilCommit_succeeds_for_deferred_profile()
    {
        FakeConfigurableUnitOfWork uow = new(
            new UnitOfWorkProfile(
                UnitOfWorkCommitMode.DeferredUntilCommit,
                CommitCoordinatesDomainWrites: true,
                SupportsTransactions: true));
        UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.DeferredUntilCommit);
    }

    [Fact]
    public void Require_DeferredUntilCommit_succeeds_for_session_scoped_transaction()
    {
        FakeConfigurableUnitOfWork uow = new(
            new UnitOfWorkProfile(
                UnitOfWorkCommitMode.SessionScopedTransaction,
                CommitCoordinatesDomainWrites: true,
                SupportsTransactions: true));
        UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.DeferredUntilCommit);
    }

    [Fact]
    public void Require_CrossAggregateAtomicity_throws_when_no_transactions()
    {
        FakeConfigurableUnitOfWork uow = new(
            new UnitOfWorkProfile(
                UnitOfWorkCommitMode.DeferredUntilCommit,
                CommitCoordinatesDomainWrites: true,
                SupportsTransactions: false));
        Assert.Throws<InvalidOperationException>(() =>
            UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.CrossAggregateAtomicity));
    }

    [Fact]
    public void Require_CrossAggregateAtomicity_succeeds_when_supported()
    {
        FakeConfigurableUnitOfWork uow = new(
            new UnitOfWorkProfile(
                UnitOfWorkCommitMode.DeferredUntilCommit,
                CommitCoordinatesDomainWrites: true,
                SupportsTransactions: true));
        UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.CrossAggregateAtomicity);
    }

    [Fact]
    public void Require_throws_when_capabilities_missing()
    {
        UnitOfWorkWithoutCapabilities uow = new();
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            UnitOfWorkGuard.Require(uow, UnitOfWorkRequirement.DeferredUntilCommit));
        Assert.Contains("IUnitOfWorkCapabilities", ex.Message, StringComparison.Ordinal);
    }
}
