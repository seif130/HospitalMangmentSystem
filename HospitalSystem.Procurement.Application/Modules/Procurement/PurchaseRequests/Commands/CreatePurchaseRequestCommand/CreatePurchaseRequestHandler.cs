using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
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
