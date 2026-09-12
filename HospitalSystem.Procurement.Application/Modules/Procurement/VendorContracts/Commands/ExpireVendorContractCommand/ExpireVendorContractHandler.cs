using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Abstractions.Persistence;
using HospitalSystem.Application.Abstractions.Time;
using HospitalSystem.Application.Common;
using HospitalSystem.Procurement.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.VendorContracts.Commands.ExpireVendorContractCommand
{
    public sealed class ExpireVendorContractCommandHandler
      : ICommandHandler<ExpireVendorContractCommand>
    {
        private readonly IVendorContractRepository _contracts;
        private readonly IProcurementUnitOfWork _uow;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ExpireVendorContractCommandHandler(
            IVendorContractRepository contracts,
            IProcurementUnitOfWork uow,
            IDateTimeProvider dateTimeProvider)
        {
            _contracts = contracts;
            _uow = uow;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(
            ExpireVendorContractCommand request,
            CancellationToken cancellationToken)
        {
            var contract = await _contracts.GetByIdAsync(
                request.VendorContractId,
                cancellationToken);

            if (contract is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "VendorContract.NotFound",
                        "Vendor contract was not found."));
            }

            contract.ExpireIfPastEndDate(
                _dateTimeProvider.UtcNow);

            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}
