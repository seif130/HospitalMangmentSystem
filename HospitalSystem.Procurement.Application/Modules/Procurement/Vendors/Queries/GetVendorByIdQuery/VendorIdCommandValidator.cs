using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Procurement.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery
{
    public sealed class VendorIdCommandValidator<T> : AbstractValidator<T> where T : notnull { }

}
