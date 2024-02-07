using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation.Results;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class DataForPrintSRMPSDataValidator : EAUValidator<DataForPrintSRMPSData>
    {
        public DataForPrintSRMPSDataValidator()
        {
            EAURuleFor(m => m.HolderData).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.UserData).EAUInjectValidator();           
        }
        public override ValidationResult Validate(ValidationContext<DataForPrintSRMPSData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.HolderData != null && instance.UserData !=null)
            {
                if (instance.HolderData.Item != null && instance.UserData.Item != null)
                {
                    if (instance.HolderData.Item is PersonData holderPerson && instance.UserData.Item is PersonData userPerson 
                        && holderPerson.PersonBasicData.Identifier.Item == userPerson.PersonBasicData.Identifier.Item)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_holderSameAsUser_E");                       
                    }

                    if (instance.HolderData.Item is EntityData holderEntity && instance.UserData.Item is EntityData userEntity
                        && holderEntity.Identifier == userEntity.Identifier)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_holderSameAsUser_E");
                    }

                }                
            }
            if (instance.UserData != null && instance.NewOwners != null && instance.NewOwners.Count > 0)
            {
                if (instance.UserData.Item is PersonData userPerson)
                {
                    foreach (var owner in instance.NewOwners)
                    {
                        if(owner.Item is PersonData ownerPerson 
                            && ownerPerson.PersonBasicData.Identifier.Item == userPerson.PersonBasicData.Identifier.Item)
                        {
                            AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_UserMustBeDifferentFromAllOwners_E");
                            break;
                        }                        
                    }
                }
                if (instance.UserData.Item is EntityData userEntity)
                {
                    foreach (var owner in instance.NewOwners)
                    {
                        if (owner.Item is EntityData ownerEntity
                            && ownerEntity.Identifier == userEntity.Identifier)
                        {
                            AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_UserMustBeDifferentFromAllOwners_E");
                            break;
                        }
                    }
                }               
            }
            if (instance.UserData !=null)
            {
                if (instance.UserData.Item is PersonData userPerson)
                {
                    if(userPerson.PersonBasicData.Names == null)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_checkedUserData_E");
                    }
                }
                if (instance.UserData.Item is EntityData userEntity)
                {
                    if (userEntity.Name == null)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_checkedUserData_E");
                    }
                }
            }

            if (instance.HolderData != null)
            {
                if (instance.HolderData.Item is PersonData holderPerson)
                {
                    if (holderPerson.PersonBasicData.Names == null)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_checkedHolderData_E");
                    }
                }
                if (instance.HolderData.Item is EntityData holderEntity)
                {
                    if (holderEntity.Name == null)
                    {
                        AddValidationFailure(validationRes, "DOC_KAT_DataForPrintSRMPSData_checkedHolderData_E");
                    }
                }                
            }            

            return validationRes;
        }
    }
}