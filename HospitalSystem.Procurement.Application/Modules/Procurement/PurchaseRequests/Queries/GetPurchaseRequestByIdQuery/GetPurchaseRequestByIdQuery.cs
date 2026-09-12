using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery
{
    public sealed record GetPurchaseRequestByIdQuery(PurchaseRequestId PurchaseRequestId) : IQuery<PurchaseRequestDto>;

}
