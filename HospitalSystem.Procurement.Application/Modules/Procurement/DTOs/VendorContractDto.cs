using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record VendorContractDto(Guid Id, Guid VendorId, DateTime Start, DateTime? End, decimal ContractValue, string Currency, VendorServiceCategory Category, VendorContractStatus Status);

}
