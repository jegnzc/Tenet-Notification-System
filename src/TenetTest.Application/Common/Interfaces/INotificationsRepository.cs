using TenetTest.Domain.Notifications;

namespace TenetTest.Application.Common.Interfaces;

public interface INotificationsRepository
{
    Task<Notification?> GetByIdAsync(Guid id);
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
}