using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Commands
{
    internal static class PurchaseRequestCommandHelper
    {
        public static async Task<Result> Execute(PurchaseRequestId id, IPurchaseRequestRepository requests, IProcurementUnitOfWork uow, Action<PurchaseRequest> action, CancellationToken ct)
        {
            var entity = await requests.GetByIdAsync(id, ct);
            if (entity is null) return Result.Failure(Error.NotFound("PurchaseRequest.NotFound", "Purchase request was not found."));
            action(entity); await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
