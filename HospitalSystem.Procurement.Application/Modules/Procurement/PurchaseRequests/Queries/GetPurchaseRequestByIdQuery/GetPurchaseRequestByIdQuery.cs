using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery
{
    public sealed record GetPurchaseRequestByIdQuery(PurchaseRequestId PurchaseRequestId) : IQuery<PurchaseRequestDto>;

}
