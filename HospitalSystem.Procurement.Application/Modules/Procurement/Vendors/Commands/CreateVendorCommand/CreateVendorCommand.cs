using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed record CreateVendorCommand(string Name, string? ContactEmail = null, string? ContactPhone = null) : ICommand<VendorId>;

}
