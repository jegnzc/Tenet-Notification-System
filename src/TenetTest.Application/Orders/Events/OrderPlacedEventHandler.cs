﻿using MassTransit;

using MediatR;

using TenetTest.Application.Common.Interfaces;
using TenetTest.Domain.NotificationChannels;
using TenetTest.Domain.Notifications;
using TenetTest.Domain.Orders.Events;
using TenetTest.Domain.Templates;

namespace TenetTest.Application.Orders.Events;

public class OrderPlacedEventHandler(IPublishEndpoint _publishEndpoint) : INotificationHandler<OrderPlacedEvent>
{
    // Created to showcase Event Sourcing
    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        var processNotificationTemplateMessage = new
        {
            TemplateId = Guid.Parse(NotificationTemplateType.OrderPlacedEmail),
            Model = new Dictionary<string, string> { { "FirstName", notification.CustomerName }, { "OrderNumber", notification.OrderNumber } },
            Recipient = notification.CustomerEmail,
            Channel = NotificationChannelType.Email,
            Priority = NotificationPriority.High
        };

        await _publishEndpoint.Publish<IProcessNotificationTemplateMessage>(processNotificationTemplateMessage, cancellationToken);
    }
}