using HospitalSystem.Application.Modules.Procurement.Abstractions;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand
{
    public sealed class ActivateVendorContractHandler(IVendorContractRepository contracts, IProcurementUnitOfWork uow) : ICommandHandler<ActivateVendorContractCommand>
    {
        public async Task<Result> Handle(ActivateVendorContractCommand request, CancellationToken ct)
        {
            var contract = await contracts.GetByIdAsync(request.VendorContractId, ct);
            if (contract is null) return Result.Failure(Error.NotFound("VendorContract.NotFound", "Vendor contract was not found."));
            contract.Activate(); await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
