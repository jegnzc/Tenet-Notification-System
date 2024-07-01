using FirebaseAdmin;

using FluentEmail.MailKitSmtp;

using Google.Apis.Auth.OAuth2;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RazorLight;
using RazorLight.Extensions;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Infrastructure.Common.Persistence;
using TenetTest.Infrastructure.Messaging;
using TenetTest.Infrastructure.Options;
using TenetTest.Infrastructure.Persistence.Notifications;
using TenetTest.Infrastructure.Persistence.Orders;
using TenetTest.Infrastructure.Persistence.Templates;
using TenetTest.Infrastructure.Services.EmailService;
using TenetTest.Infrastructure.Services.PushNotificationService;
using TenetTest.Infrastructure.Services.SmsService;
using TenetTest.Infrastructure.Services.TemplateService;

namespace TenetTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddPersistence(configuration)
            .AddEmailService(configuration)
            .AddSmsService(configuration)
            .AddPushNotificationService(configuration)
            .AddTemplateService()
            .AddMassTransitWithRabbitMQ(configuration);

        FirebaseInitializer.InitializeFirebase(configuration);
        return services;
    }

    public static IServiceCollection AddTemplateService(this IServiceCollection services)
    {
        services.AddScoped<ITemplatesRepository, NotificationTemplatesRepository>();
        services.AddRazorLight(() =>
            new RazorLightEngineBuilder()
                   .UseEmbeddedResourcesProject(typeof(TemplateService))
                   .SetOperatingAssembly(typeof(TemplateService).Assembly)
                   .UseMemoryCachingProvider()
                   .Build()
            );

        services.AddTransient<ITemplateService, TemplateService>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TenetTestDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<INotificationsRepository, NotificationsRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }

    public static class FirebaseInitializer
    {
        public static void InitializeFirebase(IConfiguration configuration)
        {
            var credentialPath = configuration["Firebase:ServiceAccountKeyPath"];
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialPath)
            });
        }
    }

    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        // Get email settings into EmailClientSettings
        var emailClientSettings = new EmailClientSettings();
        configuration.GetSection("EmailClientSettings").Bind(emailClientSettings);

        services.AddFluentEmail(configuration["EmailClientSettings:HostEmail"])
                .AddMailKitSender(new SmtpClientOptions
                {
                    Server = configuration["EmailClientSettings:HostAddress"],
                    Port = int.Parse(configuration["EmailClientSettings:HostPort"]!),
                    User = configuration["EmailClientSettings:HostUsername"],
                    Password = configuration["EmailClientSettings:HostPassword"],
                    UseSsl = false, // Disable UseSsl
                    SocketOptions = MailKit.Security.SecureSocketOptions.StartTls,
                    RequiresAuthentication = true
                });

        services.AddTransient<IEmailService, EmailService>();
        return services;
    }


    public static IServiceCollection AddPushNotificationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IPushNotificationService, PushNotificationService>();
        return services;
    }

    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ProcessSendNotificationConsumer>();
            x.AddConsumer<ProcessNotificationTemplateConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                //cfg.Host(configuration["RabbitMQ:Host"]!, h =>
                cfg.Host("localhost", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]!);
                    h.Password(configuration["RabbitMQ:Password"]!);
                });

                cfg.ReceiveEndpoint("send_notification_queue", e => e.ConfigureConsumer<ProcessSendNotificationConsumer>(context));
                cfg.ReceiveEndpoint("send_template_notification_queue", e => e.ConfigureConsumer<ProcessNotificationTemplateConsumer>(context));
            });
        });

        return services;
    }

    public static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
        services.AddTransient<ISmsService, SmsService>();
        return services;
    }
}