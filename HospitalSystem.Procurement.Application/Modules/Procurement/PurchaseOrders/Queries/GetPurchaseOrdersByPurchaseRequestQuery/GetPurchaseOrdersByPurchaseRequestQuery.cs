using HospitalSystem.Application.Models;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery
{
    public sealed record GetPurchaseOrdersByPurchaseRequestQuery(PurchaseRequestId PurchaseRequestId, int PageNumber = 1, int PageSize = 20) : IQuery<PaginatedList<PurchaseOrderDto>>;

}
