using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using HospitalSystem.Domain.Modules.Procurement.Vendors.Enums;
using HospitalSystem.Domain.ValueObjects;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using HospitalSystem.Procurement.Domain.identfires;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand
{
    public sealed class CreateVendorContractHandler(IVendorRepository vendors, IVendorContractRepository contracts, IProcurementUnitOfWork uow) : ICommandHandler<CreateVendorContractCommand, VendorContractId>
    {
        public async Task<Result<VendorContractId>> Handle(CreateVendorContractCommand request, CancellationToken ct)
        {
            var vendor = await vendors.GetByIdAsync(request.VendorId, ct);
            if (vendor is null) return Result.Failure<VendorContractId>(Error.NotFound("Vendor.NotFound", "Vendor was not found."));
            if (vendor.Status != VendorStatus.Active) return Result.Failure<VendorContractId>(Error.Conflict("Vendor.Inactive", "Only active vendors can have contracts."));
            var contract = VendorContract.Draft(request.VendorId, request.Category, DateRange.Create(request.StartUtc, request.EndUtc), Money.Create(request.Amount, request.Currency));
            await contracts.AddAsync(contract, ct); await uow.SaveChangesAsync(ct); return Result.Success(contract.Id);
        }
    }

}
