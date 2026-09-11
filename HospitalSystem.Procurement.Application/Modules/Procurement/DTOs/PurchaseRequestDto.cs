using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record PurchaseRequestDto(Guid Id, Guid DepartmentId, string Reason, 
        PurchaseRequestStatus Status, IReadOnlyList<PurchaseRequestLineDto> Lines);

}
