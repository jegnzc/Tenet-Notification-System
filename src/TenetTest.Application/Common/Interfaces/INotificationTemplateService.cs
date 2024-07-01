using ErrorOr;

using TenetTest.Application.Common.Services;
using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Application.Common.Interfaces;

public interface ITemplateService
{
    Task<ErrorOr<NotificationTemplateContent>> GetTemplateContentAsync(Guid TemplateId, NotificationChannelType Channel, Dictionary<string, string> model);
}