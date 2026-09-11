using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Procurement.Application.Modules.Procurement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery
{
    public sealed record GetVendorContractByIdQuery(VendorContractId VendorContractId) : IQuery<VendorContractDto>;

}
