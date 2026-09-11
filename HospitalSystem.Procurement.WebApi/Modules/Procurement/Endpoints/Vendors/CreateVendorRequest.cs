using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.WebApi.Modules.Procurement.Endpoints.Vendors
{
    public sealed record CreateVendorRequest(
        string Name,
        string? ContactEmail,
        string? ContactPhone);

    public sealed record RenameVendorRequest(
        string Name);

    public sealed record UpdateContactRequest(
        string? ContactEmail,
        string? ContactPhone);
}
