using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand
{
    public sealed class RejectPurchaseRequestHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<RejectPurchaseRequestCommand>
    {
        public async Task<Result> Handle(RejectPurchaseRequestCommand request, CancellationToken ct)
        {
            var entity = await requests.GetByIdAsync(request.PurchaseRequestId, ct);
            if (entity is null)
                return Result.Failure(Error.NotFound("PurchaseRequest.NotFound", "Purchase request was not found."));
            entity.Reject(request.Reason);
            await uow.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
