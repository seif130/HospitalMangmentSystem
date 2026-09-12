
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.ApprovePurchaseRequestCommand
{
    public sealed class ApprovePurchaseRequestHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<ApprovePurchaseRequestCommand>
    {
        public async Task<Result> Handle(ApprovePurchaseRequestCommand request, CancellationToken ct) => await PurchaseRequestCommandHelper.Execute(request.PurchaseRequestId, requests, uow, x => x.Approve(), ct);
    }

}
