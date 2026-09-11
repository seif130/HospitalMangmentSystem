using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
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
