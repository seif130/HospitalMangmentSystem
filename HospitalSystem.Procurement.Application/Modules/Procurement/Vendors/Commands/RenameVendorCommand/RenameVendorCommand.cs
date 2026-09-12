using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed record RenameVendorCommand(VendorId VendorId, string Name) : ICommand;

}
