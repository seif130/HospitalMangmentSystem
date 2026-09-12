
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand
{
    public sealed class CancelPurchaseRequestHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<CancelPurchaseRequestCommand>
    {
        public async Task<Result> Handle(CancelPurchaseRequestCommand request, CancellationToken ct) => await PurchaseRequestCommandHelper.Execute(request.PurchaseRequestId, requests, uow, x => x.Cancel(), ct);
    }

}
