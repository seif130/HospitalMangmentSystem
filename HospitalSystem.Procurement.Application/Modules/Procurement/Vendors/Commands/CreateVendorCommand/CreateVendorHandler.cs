using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.Modules.Procurement.Vendors;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand
{
    public sealed class CreateVendorHandler(IVendorRepository vendors, IProcurementUnitOfWork uow) : ICommandHandler<CreateVendorCommand, VendorId>
    {
        public async Task<Result<VendorId>> Handle(CreateVendorCommand request, CancellationToken ct)
        {
            var normalized = request.Name.Trim().ToUpperInvariant();
            if (await vendors.ExistsByNormalizedNameAsync(normalized, null, ct))
                return Result.Failure<VendorId>(Error.Conflict("Vendor.DuplicateName", "A vendor with the same name already exists."));
            var vendor = Vendor.Create(request.Name, request.ContactEmail, request.ContactPhone);
            await vendors.AddAsync(vendor, ct); await uow.SaveChangesAsync(ct);
            return Result.Success(vendor.Id);
        }
    }

}
