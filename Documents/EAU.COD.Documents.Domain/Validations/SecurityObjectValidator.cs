using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.COD.Documents.Domain.Validations
{
    public class SecurityObjectValidator : EAUValidator<SecurityObject>
    {
        public SecurityObjectValidator()
        {
            RuleSet("New", () => {
                EAURuleFor(m => m.PointOfPrivateSecurityServicesLaw).RequiredField();
                EAURuleFor(m => m.SelfProtectionPersonsProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.EntityPropertySelfProtection).InjectValidator("New");
                EAURuleFor(m => m.ProtectionOfAgriculturalProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.AgriculturalAndPropertyProtection).InjectValidator("New");
                EAURuleFor(m => m.ProtectionPersonsProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.PropertySecurityServices).InjectValidator("New");
                EAURuleFor(m => m.SecurityOfEvents).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.EventsSecurityServices).InjectValidator("New");
                EAURuleFor(m => m.AlarmAndSecurityActivity).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.AlarmAndSecurityActivity).InjectValidator("New");
                EAURuleFor(m => m.SecurityOfSitesRealEstate).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.RealEstatSecurity).InjectValidator("New");
                EAURuleFor(m => m.SecurityTransportingCargo).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.ValuablesAndCargoesSecurityServices).InjectValidator("New");
                EAURuleFor(m => m.PersonalSecurity).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.PersonalSecurityServicesForPersons).InjectValidator("New");
            });
            RuleSet("Termination", () => {
                EAURuleFor(m => m.PointOfPrivateSecurityServicesLaw).RequiredField();
                EAURuleFor(m => m.SelfProtectionPersonsProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.EntityPropertySelfProtection).InjectValidator("Termination");
                EAURuleFor(m => m.ProtectionOfAgriculturalProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.AgriculturalAndPropertyProtection).InjectValidator("Termination");
                EAURuleFor(m => m.ProtectionPersonsProperty).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.PropertySecurityServices).InjectValidator("Termination");
                EAURuleFor(m => m.SecurityOfEvents).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.EventsSecurityServices).InjectValidator("Termination");
                EAURuleFor(m => m.AlarmAndSecurityActivity).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.AlarmAndSecurityActivity).InjectValidator("Termination");
                EAURuleFor(m => m.SecurityOfSitesRealEstate).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.RealEstatSecurity).InjectValidator("Termination");
                EAURuleFor(m => m.SecurityTransportingCargo).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.ValuablesAndCargoesSecurityServices).InjectValidator("Termination");
                EAURuleFor(m => m.PersonalSecurity).RequiredSection().When(m => m.PointOfPrivateSecurityServicesLaw == PointOfPrivateSecurityServicesLaw.PersonalSecurityServicesForPersons).InjectValidator("Termination");
            });
        }
    }
}
