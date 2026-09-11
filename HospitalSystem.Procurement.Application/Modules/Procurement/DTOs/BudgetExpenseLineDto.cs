using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record BudgetExpenseLineDto(string Description, decimal Amount, string Currency, DateTime IncurredOnUtc);

}
