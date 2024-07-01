using FluentEmail.Core;

using TenetTest.Application.Common.Interfaces;

namespace TenetTest.Infrastructure.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var response = await _fluentEmail
            .To(toEmail)
            .Subject(subject)
            .Body(body, isHtml: true)
            .SendAsync();

        if (!response.Successful)
        {
            throw new Exception($"Failed to send email: {response.ErrorMessages.FirstOrDefault()}");
        }

        await Task.CompletedTask;
    }
}