using ErrorOr;

using RazorLight;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Application.Common.Services;
using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Infrastructure.Services.TemplateService;

public class TemplateService : ITemplateService
{
    private readonly ITemplatesRepository _templateRepository;
    private readonly IRazorLightEngine _razorEngine;

    public TemplateService(ITemplatesRepository templateRepository, IRazorLightEngine razorEngine)
    {
        _templateRepository = templateRepository;
        _razorEngine = razorEngine;
    }

    public async Task<ErrorOr<NotificationTemplateContent>> GetTemplateContentAsync(Guid templateId, NotificationChannelType channel, Dictionary<string, string> model)
    {
        var templateResult = await _templateRepository.GetTemplateByIdAndChannelAsync(templateId, channel);

        if (templateResult is null)
        {
            return Error.NotFound(description: $"Template for channel {channel.Name} not found.");
        }

        string? subject = templateResult.Subject;
        string body = templateResult.Body;

        if (model is not null)
        {
            try
            {
                if (subject != null)
                {
                    subject = await _razorEngine.CompileRenderStringAsync($"{templateId}-subject", subject, model);
                }

                body = await _razorEngine.CompileRenderStringAsync($"{templateId}-body", body, model);
            }
            catch (KeyNotFoundException ex)
            {
                return Error.NotFound(code: "Template.RequiredFieldsMissing", description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(description: $"Failed to render template for channel {channel.Name}: {ex.Message}");
            }
        }

        return new NotificationTemplateContent(subject, body);
    }
}