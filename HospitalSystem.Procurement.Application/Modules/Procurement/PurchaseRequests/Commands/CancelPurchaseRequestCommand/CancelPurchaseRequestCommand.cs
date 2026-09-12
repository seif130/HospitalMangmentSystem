using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand
{
    public sealed record CancelPurchaseRequestCommand(PurchaseRequestId PurchaseRequestId) : ICommand;

}
