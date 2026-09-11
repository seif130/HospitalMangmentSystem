using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record PurchaseRequestLineDto(string ItemName, int Quantity, 
        decimal EstimatedUnitPrice, decimal EstimatedTotal, string Currency);

}
