using FluentValidation;

using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Application.Notifications.Commands.SendTemplatedNotification;

public class SendTemplatedNotificationCommandValidator : AbstractValidator<SendTemplatedNotificationCommand>
{
    public SendTemplatedNotificationCommandValidator()
    {
        RuleFor(x => x.Recipient)
            .NotEmpty()
            .WithMessage("Recipient is required.")
            .Must(BeValidRecipient)
            .WithMessage("Recipient format is invalid for the specified notification channel.");

        RuleFor(x => x.TemplateId)
            .NotEmpty()
            .WithMessage("TemplateId is required.");
    }

    private bool BeValidRecipient(SendTemplatedNotificationCommand command, string recipient)
    {
        if (command.Channel == NotificationChannelType.Email)
        {
            return BeValidEmail(recipient);
        }
        else if (command.Channel == NotificationChannelType.SMS)
        {
            return BeValidPhoneNumber(recipient);
        }
        else if (command.Channel == NotificationChannelType.Push)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool BeValidPhoneNumber(string phoneNumber)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+\d{11,15}$");
    }

    private bool BeValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}