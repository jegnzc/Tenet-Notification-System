using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TenetTest.Domain.Templates;

namespace TenetTest.Infrastructure.Persistence.Templates;

public class NotificationTemplatesConfigurations : IEntityTypeConfiguration<NotificationTemplate>
{
    private const string BaseHtml = @"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <style>
            body {{ font-family: Arial, sans-serif; }}
            .container {{ padding: 20px; }}
            .header, .footer {{ text-align: center; }}
            .header {{ background-color: #f8f8f8; padding: 10px; }}
            .footer {{ margin-top: 20px; color: #aaa; }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <img src='https://via.placeholder.com/150' alt='Company Logo' />
            </div>
            <div class='content'>
                {0}
            </div>
            <div class='footer'>
                <p>Company Name | <a href='#' target='_blank'>Unsubscribe</a></p>
            </div>
        </div>
    </body>
    </html>";

    public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.NotificationChannelId)
            .IsRequired();

        builder.Property(t => t.Subject)
            .HasMaxLength(255);

        builder.Property(t => t.Body)
            .IsRequired();

        builder.HasOne(t => t.NotificationChannel)
            .WithMany(c => c.Templates)
            .HasForeignKey(t => t.NotificationChannelId)
            .IsRequired();

        builder.HasIndex(nt => nt.NotificationChannelId);

        builder.HasData(
            new NotificationTemplate(NotificationTemplateType.WelcomeEmail.Name, NotificationTemplateType.WelcomeEmail.Channel, "Welcome to Our Service", string.Format(BaseHtml, @"
                <h1>Welcome, @Model[""FirstName""]!</h1>
                <p>We are excited to have you on board. Explore our services and enjoy the benefits we offer.</p>"), NotificationTemplateType.WelcomeEmail.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PasswordResetEmail.Name, NotificationTemplateType.PasswordResetEmail.Channel, "Reset Your Password", string.Format(BaseHtml, @"
                <h1>Password Reset</h1>
                <p>Hello, @Model[""FirstName""],</p>
                <p>You can reset your password using this <a href=""#"" target=""_blank"">link</a>.</p>
                <p>If you did not request a password reset, please ignore this email.</p>"), NotificationTemplateType.PasswordResetEmail.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PromotionEmail.Name, NotificationTemplateType.PromotionEmail.Channel, "Special Promotion", string.Format(BaseHtml, @"
                <h1>Special Promotion Just for You!</h1>
                <p>Hello, @Model[""FirstName""],</p>
                <p>We are offering a special promotion exclusively for you. Don't miss out!</p>
                <table border='1' cellspacing='0' cellpadding='10'>
                    <tr>
                        <th>Product</th>
                        <th>Discount</th>
                        <th>Link</th>
                    </tr>
                    <tr>
                        <td>Product A</td>
                        <td>20% off</td>
                        <td><a href='#' target='_blank'>Buy Now</a></td>
                    </tr>
                    <tr>
                        <td>Product B</td>
                        <td>30% off</td>
                        <td><a href='#' target='_blank'>Buy Now</a></td>
                    </tr>
                </table>"), NotificationTemplateType.PromotionEmail.TemplateId),
            new NotificationTemplate(NotificationTemplateType.AccountVerificationEmail.Name, NotificationTemplateType.AccountVerificationEmail.Channel, "Verify Your Account", string.Format(BaseHtml, @"
                <h1>Account Verification</h1>
                <p>Hello, @Model[""FirstName""],</p>
                <p>Please verify your account using this <a href=""#"" target=""_blank"">link</a>.</p>
                <p>Thank you for joining us!</p>"), NotificationTemplateType.AccountVerificationEmail.TemplateId),

            new NotificationTemplate(NotificationTemplateType.WelcomeSMS.Name, NotificationTemplateType.WelcomeSMS.Channel, null, "Hello, @Model[\"FirstName\"], welcome to our service!", NotificationTemplateType.WelcomeSMS.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PasswordResetSMS.Name, NotificationTemplateType.PasswordResetSMS.Channel, null, "Hello, @Model[\"FirstName\"], you can reset your password using this link.", NotificationTemplateType.PasswordResetSMS.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PromotionSMS.Name, NotificationTemplateType.PromotionSMS.Channel, null, "Hello, @Model[\"FirstName\"], check out our special promotion!", NotificationTemplateType.PromotionSMS.TemplateId),
            new NotificationTemplate(NotificationTemplateType.AccountVerificationSMS.Name, NotificationTemplateType.AccountVerificationSMS.Channel, null, "Hello, @Model[\"FirstName\"], please verify your account using this link.", NotificationTemplateType.AccountVerificationSMS.TemplateId),

            new NotificationTemplate(NotificationTemplateType.WelcomePush.Name, NotificationTemplateType.WelcomePush.Channel, "Welcome to Our Service", "Hello, @Model[\"FirstName\"], welcome to our service!", NotificationTemplateType.WelcomePush.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PasswordResetPush.Name, NotificationTemplateType.PasswordResetPush.Channel, "Reset Your Password", "Hello, @Model[\"FirstName\"], you can reset your password using this link.", NotificationTemplateType.PasswordResetPush.TemplateId),
            new NotificationTemplate(NotificationTemplateType.PromotionPush.Name, NotificationTemplateType.PromotionPush.Channel, "Special Promotion", "Hello, @Model[\"FirstName\"], check out our special promotion!", NotificationTemplateType.PromotionPush.TemplateId),
            new NotificationTemplate(NotificationTemplateType.AccountVerificationPush.Name, NotificationTemplateType.AccountVerificationPush.Channel, "Verify Your Account", "Hello, @Model[\"FirstName\"], please verify your account using this link.", NotificationTemplateType.AccountVerificationPush.TemplateId),

            // New Order Placed Template
            new NotificationTemplate(NotificationTemplateType.OrderPlacedEmail.Name, NotificationTemplateType.OrderPlacedEmail.Channel, "Order Confirmation", string.Format(BaseHtml, @"
                <h1>Order Confirmation</h1>
                <p>Hello, @Model[""FirstName""],</p>
                <p>Your order with order number @Model[""OrderNumber""] has been placed successfully.</p>
                <p>We will notify you once the order is shipped.</p>
                <p>Thank you for shopping with us!</p>"), NotificationTemplateType.OrderPlacedEmail.TemplateId)
        );
    }
}