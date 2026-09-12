using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand
{
    public sealed class RenameVendorHandler(IVendorRepository vendors, IProcurementUnitOfWork uow) : ICommandHandler<RenameVendorCommand>
    {
        public async Task<Result> Handle(RenameVendorCommand request, CancellationToken ct)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId, ct);
            if (vendor is null) return Result.Failure(Error.NotFound("Vendor.NotFound", "Vendor was not found."));
            var normalized = request.Name.Trim().ToUpperInvariant();
            if (!string.Equals(vendor.NormalizedName, normalized, StringComparison.Ordinal) && await vendors.ExistsByNormalizedNameAsync(normalized, vendor.Id, ct))
                return Result.Failure(Error.Conflict("Vendor.DuplicateName", "A vendor with the same name already exists."));
            vendor.Rename(request.Name);
            await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
