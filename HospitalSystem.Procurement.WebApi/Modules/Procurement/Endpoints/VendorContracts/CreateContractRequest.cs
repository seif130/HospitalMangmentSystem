using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints.VendorContracts
{
    public sealed record CreateContractRequest(
        Guid VendorId,
        VendorServiceCategory Category,
        DateTime StartUtc,
        DateTime EndUtc,
        decimal Amount,
        string Currency = "USD");

    public sealed record TerminateRequest(
        string Reason);
}
