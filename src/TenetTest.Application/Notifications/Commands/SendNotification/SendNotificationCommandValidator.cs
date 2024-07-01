using FluentValidation;

using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Application.Notifications.Commands.SendNotification;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(x => x.Recipient)
            .NotEmpty()
            .WithMessage("Recipient is required.")
            .Must(BeValidRecipient)
            .WithMessage("Recipient format is invalid for the specified notification channel.");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .When(x => x.Channel == NotificationChannelType.Email || x.Channel == NotificationChannelType.Push)
            .WithMessage("Subject is required for email and push notifications.");

        RuleFor(x => x.Body)
            .NotEmpty()
            .When(x => x.Channel == NotificationChannelType.Email || x.Channel == NotificationChannelType.Push)
            .WithMessage("Body is required for email and push notifications.");
    }

    private bool BeValidRecipient(SendNotificationCommand command, string recipient)
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