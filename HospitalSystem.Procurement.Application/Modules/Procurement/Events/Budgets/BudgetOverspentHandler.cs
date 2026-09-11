using HospitalSystem.Application.Modules.Procurement.Events;
using HospitalSystem.Domain.Modules.Procurement.Budgets.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Events.Budgets
{
    public sealed class BudgetOverspentHandler(ILogger<BudgetOverspentHandler> logger) : INotificationHandler<DomainEventNotification<BudgetOverspentDomainEvent>>
    {
        public Task Handle(DomainEventNotification<BudgetOverspentDomainEvent> notification, CancellationToken ct)
        {
            logger.LogWarning
                ("Budget {BudgetId} for department {DepartmentId} is overspent. Spent: {Spent}, Allocated: {Allocated}", notification.Event.BudgetId.Value, notification.Event.DepartmentId.Value, notification.Event.SpentAmount, notification.Event.AllocatedAmount);
            return Task.CompletedTask;
        }
    }

}
