using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed record CreateVendorCommand(string Name, string? ContactEmail = null, string? ContactPhone = null) : ICommand<VendorId>;

}
