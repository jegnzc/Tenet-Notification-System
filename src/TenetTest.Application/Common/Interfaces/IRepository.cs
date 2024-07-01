namespace TenetTest.Application.Common.Interfaces;

public interface IRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}