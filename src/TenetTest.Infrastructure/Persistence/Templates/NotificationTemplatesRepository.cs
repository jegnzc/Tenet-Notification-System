using Microsoft.EntityFrameworkCore;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.Templates;
using TenetTest.Infrastructure.Common.Persistence;

namespace TenetTest.Infrastructure.Persistence.Templates;
public class NotificationTemplatesRepository : ITemplatesRepository
{
    private readonly TenetTestDbContext _context;

    public NotificationTemplatesRepository(TenetTestDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationTemplate?> GetTemplateByIdAndChannelAsync(Guid id, int channelId)
    {
        return await _context.NotificationTemplates
            .FirstOrDefaultAsync(t => t.Id == id && t.NotificationChannelId == channelId);
    }

    public async Task<IEnumerable<NotificationTemplate>> GetAllTemplatesAsync()
    {
        return await _context.NotificationTemplates.ToListAsync();
    }

    public async Task AddTemplateAsync(NotificationTemplate template)
    {
        await _context.NotificationTemplates.AddAsync(template);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTemplateAsync(NotificationTemplate template)
    {
        _context.NotificationTemplates.Update(template);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTemplateAsync(Guid id)
    {
        var template = await _context.NotificationTemplates.FindAsync(id);
        if (template != null)
        {
            _context.NotificationTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }
    }
}