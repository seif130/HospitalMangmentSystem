using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record PurchaseOrderLineDto(string ItemName, int Quantity, decimal UnitPrice,
        decimal Total, string Currency);

}
