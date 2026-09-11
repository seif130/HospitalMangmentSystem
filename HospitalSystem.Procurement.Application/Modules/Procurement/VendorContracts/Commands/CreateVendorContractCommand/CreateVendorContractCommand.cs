using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed record CreateVendorContractCommand(VendorId VendorId, VendorServiceCategory Category, DateTime StartUtc, DateTime EndUtc, decimal Amount, string Currency = "USD") : ICommand<VendorContractId>;

}
