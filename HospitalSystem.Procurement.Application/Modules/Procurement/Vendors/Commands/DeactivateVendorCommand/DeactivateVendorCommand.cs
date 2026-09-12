using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand
{
    public sealed record DeactivateVendorCommand(VendorId VendorId) : ICommand;

}
