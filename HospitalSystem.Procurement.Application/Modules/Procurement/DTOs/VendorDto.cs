using HospitalSystem.Domain.Modules.Procurement.Vendors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.DTOs
{
    public sealed record VendorDto(Guid Id, string Name, string? ContactEmail, string? ContactPhone, VendorStatus Status);

}
