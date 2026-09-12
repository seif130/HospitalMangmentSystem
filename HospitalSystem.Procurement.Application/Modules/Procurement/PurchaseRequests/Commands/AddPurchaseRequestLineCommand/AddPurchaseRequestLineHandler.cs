
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand
{
    public sealed class AddPurchaseRequestLineHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<AddPurchaseRequestLineCommand>
    {
        public async Task<Result> Handle(AddPurchaseRequestLineCommand request, CancellationToken ct)
        {
            var entity = await requests.GetByIdAsync(request.PurchaseRequestId, ct);
            if (entity is null) return Result.Failure(Error.NotFound("PurchaseRequest.NotFound", "Purchase request was not found."));
            entity.AddLine(request.ItemName, request.Quantity, Money.Create(request.EstimatedUnitPrice, request.Currency));
            await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
