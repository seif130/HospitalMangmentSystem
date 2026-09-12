
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand
{
    public sealed class CreatePurchaseRequestHandler(IPurchaseRequestRepository requests, IProcurementUnitOfWork uow) : ICommandHandler<CreatePurchaseRequestCommand, PurchaseRequestId>
    {
        public async Task<Result<PurchaseRequestId>> Handle(CreatePurchaseRequestCommand request, CancellationToken ct)
        {
            var entity = PurchaseRequest.Create(request.DepartmentId, request.Reason);
            await requests.AddAsync(entity, ct); await uow.SaveChangesAsync(ct); return Result.Success(entity.Id);
        }
    }

}
