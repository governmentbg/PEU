using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ServiceApplicantReceiptDataValidator : EAUValidator<ServiceApplicantReceiptData>
    {
        public ServiceApplicantReceiptDataValidator()
        {
            EAURuleFor(m => m.ServiceResultReceiptMethod).RequiredFieldFromSection();
            EAURuleFor(m => m.ItemServiceApplicantReceiptDataAddress)
                .RequiredFieldFromSection()
                .EAUInjectValidator()
                .When(m => m.ServiceResultReceiptMethod == ServiceResultReceiptMethods.CourierToOtherAddress); //ServiceApplicantReceiptDataAddressValidator
            EAURuleFor(m => m.ItemString)
                .RequiredFieldFromSection()
                .When(m => m.ServiceResultReceiptMethod == ServiceResultReceiptMethods.CourierToMailBox); //string
            EAURuleFor(m => m.ItemServiceApplicantReceiptDataMunicipalityAdministrationAdress)
                .RequiredFieldFromSection()
                .EAUInjectValidator()
                .When(m => m.ServiceResultReceiptMethod == ServiceResultReceiptMethods.DeskInAdministration); //ServiceApplicantReceiptDataMunicipalityAdministrationAdressValidator
            EAURuleFor(m => m.ItemServiceApplicantReceiptDataUnitInAdministration)
                .RequiredFieldFromSection()
                .EAUInjectValidator()
                .When(m => m.ServiceResultReceiptMethod == ServiceResultReceiptMethods.UnitInAdministration); //ServiceApplicantReceiptDataUnitInAdministrationValidator
        }
    }
}