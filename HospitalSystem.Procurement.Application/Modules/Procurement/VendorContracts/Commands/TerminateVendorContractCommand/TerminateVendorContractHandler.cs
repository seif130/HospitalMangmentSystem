
using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand
{
    public sealed class TerminateVendorContractHandler(IVendorContractRepository contracts, IProcurementUnitOfWork uow) : ICommandHandler<TerminateVendorContractCommand>
    {
        public async Task<Result> Handle(TerminateVendorContractCommand request, CancellationToken ct)
        {
            var contract = await contracts.GetByIdAsync(request.VendorContractId, ct);
            if (contract is null) return Result.Failure(Error.NotFound("VendorContract.NotFound", "Vendor contract was not found."));
            contract.Terminate(request.Reason); await uow.SaveChangesAsync(ct); return Result.Success();
        }
    }

}
