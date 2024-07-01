using Ardalis.SmartEnum;

using TenetTest.Domain.NotificationChannels;

namespace TenetTest.Domain.Templates;

public class NotificationTemplateType : SmartEnum<NotificationTemplateType, string>
{
    public static readonly NotificationTemplateType WelcomeEmail = new NotificationTemplateType(nameof(WelcomeEmail), "cbc3395c-e3bb-4d55-908c-4c7ce53c0478", NotificationChannelType.Email);
    public static readonly NotificationTemplateType PasswordResetEmail = new NotificationTemplateType(nameof(PasswordResetEmail), "bd51fa5f-5476-4d14-84ec-498d6a8f530d", NotificationChannelType.Email);
    public static readonly NotificationTemplateType PromotionEmail = new NotificationTemplateType(nameof(PromotionEmail), "a93c032a-22f1-419d-a659-8af289e30c0d", NotificationChannelType.Email);
    public static readonly NotificationTemplateType AccountVerificationEmail = new NotificationTemplateType(nameof(AccountVerificationEmail), "46d5edfa-ab20-4be5-87c9-d2299f8ff034", NotificationChannelType.Email);
    public static readonly NotificationTemplateType OrderPlacedEmail = new NotificationTemplateType(nameof(OrderPlacedEmail), "e31d8c06-4ab0-4934-bb5a-8b460d00de24", NotificationChannelType.Email);

    public static readonly NotificationTemplateType WelcomeSMS = new NotificationTemplateType(nameof(WelcomeSMS), "558df13a-dcbf-4f07-b127-36dbe5307e22", NotificationChannelType.SMS);
    public static readonly NotificationTemplateType PasswordResetSMS = new NotificationTemplateType(nameof(PasswordResetSMS), "18d059b8-ea89-447e-8737-770bfc39c460", NotificationChannelType.SMS);
    public static readonly NotificationTemplateType PromotionSMS = new NotificationTemplateType(nameof(PromotionSMS), "183a09c2-ba42-469d-891b-e4ae83fe4fa3", NotificationChannelType.SMS);
    public static readonly NotificationTemplateType AccountVerificationSMS = new NotificationTemplateType(nameof(AccountVerificationSMS), "fcc69863-e92f-40d8-9e3d-18bc5af18845", NotificationChannelType.SMS);

    public static readonly NotificationTemplateType WelcomePush = new NotificationTemplateType(nameof(WelcomePush), "b8619fd4-0acf-4937-8f1a-573519f22ee0", NotificationChannelType.Push);
    public static readonly NotificationTemplateType PasswordResetPush = new NotificationTemplateType(nameof(PasswordResetPush), "fe883949-da99-4892-84ff-0686da1adbf0", NotificationChannelType.Push);
    public static readonly NotificationTemplateType PromotionPush = new NotificationTemplateType(nameof(PromotionPush), "9b31debf-45c4-4d98-8811-898d9c1b938f", NotificationChannelType.Push);
    public static readonly NotificationTemplateType AccountVerificationPush = new NotificationTemplateType(nameof(AccountVerificationPush), "2e681b67-2920-4e33-8ed0-da492b70aa21", NotificationChannelType.Push);

    public Guid TemplateId { get; }
    public NotificationChannelType Channel { get; }

    private NotificationTemplateType(string name, string templateId, NotificationChannelType channel)
        : base(name, templateId)
    {
        TemplateId = Guid.Parse(templateId);
        Channel = channel;
    }
}