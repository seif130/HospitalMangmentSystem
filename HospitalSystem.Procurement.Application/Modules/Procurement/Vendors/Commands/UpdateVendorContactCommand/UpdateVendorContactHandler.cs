using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand
{
    public sealed class UpdateVendorContactHandler(IVendorRepository vendors, IProcurementUnitOfWork uow) : ICommandHandler<UpdateVendorContactCommand>
    {
        public async Task<Result> Handle(UpdateVendorContactCommand request, CancellationToken ct)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId, ct);
            if (vendor is null) return Result.Failure(Error.NotFound("Vendor.NotFound", "Vendor was not found."));
            vendor.UpdateContact(request.ContactEmail, request.ContactPhone);
            await uow.SaveChangesAsync(ct);
            return Result.Success();
        }
    }

}
