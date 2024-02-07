﻿using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataItemCollectionValidator : EAUValidator<VehicleDataItemCollection>
    {
        public VehicleDataItemCollectionValidator()
        {
            EAURuleFor(m => m.Items).RequiredField();
            EAURuleForEach(m => m.Items).EAUInjectValidator();
        }
    }
}