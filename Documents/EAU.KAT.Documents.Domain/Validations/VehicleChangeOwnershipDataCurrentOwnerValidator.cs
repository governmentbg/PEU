using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleChangeOwnershipDataCurrentOwnerValidator : EAUValidator<VehicleChangeOwnershipDataCurrentOwner>
    {
        public VehicleChangeOwnershipDataCurrentOwnerValidator()
        {
            When(m => m.IsFarmer.HasValue && m.IsFarmer.Value, () =>
            {
                EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
                {
                    v.EAUAdd(new EntityDataValidator(), ruleSets: "Farmer");
                });

            })
            .Otherwise(() =>
            {
                When(m => m.VehicleOwnerAdditionalCircumstances.HasValue, () =>
                {
                    EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
                    {

                        v.EAUAdd(new PersonDataValidator(), ruleSets: "PersonIdentifier");
                        v.EAUAdd(new EntityDataValidator(), ruleSets: "Entity");
                    });
                })
                .Otherwise(() =>
                {
                    EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
                    {

                        v.EAUAdd(new PersonDataValidator(), ruleSets: "PersonIdentifierDocument");
                        v.EAUAdd(new EntityDataValidator(), ruleSets: "Entity");
                    });
                });               
            });

            EAURuleFor(m => m.SuccessorData).RequiredField().When(m => m.VehicleOwnerAdditionalCircumstances == VehicleOwnerAdditionalCircumstances.DeadPerson);
        }        
    }
}