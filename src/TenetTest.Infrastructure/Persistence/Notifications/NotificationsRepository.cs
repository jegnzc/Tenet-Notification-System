using Microsoft.EntityFrameworkCore;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Notifications;
using TenetTest.Infrastructure.Common.Persistence;

namespace TenetTest.Infrastructure.Persistence.Notifications;

public class NotificationsRepository : INotificationsRepository
{
    private readonly TenetTestDbContext _context;

    public NotificationsRepository(TenetTestDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<Notification?> GetByIdAsync(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.Content)
            .Include(n => n.NotificationChannel)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }
}