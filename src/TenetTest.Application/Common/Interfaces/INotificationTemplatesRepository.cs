using TenetTest.Domain.Templates;

namespace TenetTest.Application.Common.Interfaces;

public interface ITemplatesRepository
{
    Task<NotificationTemplate?> GetTemplateByIdAndChannelAsync(Guid id, int channelId);
    Task<IEnumerable<NotificationTemplate>> GetAllTemplatesAsync();
    Task AddTemplateAsync(NotificationTemplate template);
    Task UpdateTemplateAsync(NotificationTemplate template);
    Task DeleteTemplateAsync(Guid id);
}