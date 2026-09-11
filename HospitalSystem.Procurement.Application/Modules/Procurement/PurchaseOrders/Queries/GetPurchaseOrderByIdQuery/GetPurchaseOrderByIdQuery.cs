using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery
{
    public sealed record GetPurchaseOrderByIdQuery(PurchaseOrderId PurchaseOrderId) : IQuery<PurchaseOrderDto>;

}
