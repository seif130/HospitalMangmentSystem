using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand
{
    public sealed record ActivateVendorContractCommand(VendorContractId VendorContractId) : ICommand;

}
