
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand
{
    public sealed class SubmitPurchaseRequestHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<SubmitPurchaseRequestCommand>
    {
        public async Task<Result> Handle(SubmitPurchaseRequestCommand request, CancellationToken ct)
            => await PurchaseRequestCommandHelper.Execute(request.PurchaseRequestId, requests, uow, x => x.Submit(), ct);
    }

}
