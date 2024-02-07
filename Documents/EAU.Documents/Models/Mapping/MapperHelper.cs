using AutoMapper;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EAU.Documents.Models.Mapping
{
    public static class MapperHelper
    {
        #region Application Mappers

        public static void MapApplicationDomainToViewModel(IMapperBase mapper, IApplicationForm domainModel, ApplicationFormVMBase viewModel)
        {
            viewModel.ServiceTermTypeAndApplicantReceipt = new ServiceTermTypeAndApplicantReceiptVM();
            viewModel.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData = mapper.Map<ServiceApplicantReceiptDataVM>(domainModel.ServiceApplicantReceiptData);
            viewModel.ServiceTermTypeAndApplicantReceipt.ServiceTermType = domainModel.ServiceTermType;

            viewModel.ElectronicServiceApplicant = mapper.Map<ElectronicServiceApplicantVM>(domainModel.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant);
            viewModel.ElectronicServiceApplicantContactData = domainModel.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData;
            viewModel.ElectronicServiceApplicant.SendApplicationWithReceiptAcknowledgedMessage = domainModel.ElectronicAdministrativeServiceHeader.SendApplicationWithReceiptAcknowledgedMessage;

            viewModel.ElectronicAdministrativeServiceHeader = mapper.Map<ElectronicAdministrativeServiceHeaderVM>(domainModel.ElectronicAdministrativeServiceHeader);

            if (domainModel.Declarations != null && domainModel.Declarations.Count > 0)
            {
                viewModel.Declarations = new DeclarationsVM();
                viewModel.Declarations.Declarations = mapper.Map<List<DeclarationVM>>(domainModel.Declarations);
            }
        }

        public static void MapApplicationViewModelToDomain(IMapperBase mapper, ApplicationFormVMBase viewModel, IApplicationForm domainModel)
        {
            if (viewModel.ServiceTermTypeAndApplicantReceipt != null)
            {
                domainModel.ServiceApplicantReceiptData = mapper.Map<ServiceApplicantReceiptData>(viewModel.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData);
                domainModel.ServiceTermType = viewModel.ServiceTermTypeAndApplicantReceipt.ServiceTermType;
            }
            domainModel.ElectronicAdministrativeServiceHeader = mapper.Map<ElectronicAdministrativeServiceHeader>(viewModel.ElectronicAdministrativeServiceHeader);
            domainModel.ElectronicAdministrativeServiceHeader.SendApplicationWithReceiptAcknowledgedMessage = viewModel.ElectronicServiceApplicant.SendApplicationWithReceiptAcknowledgedMessage;
            domainModel.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant = mapper.Map<ElectronicServiceApplicant>(viewModel.ElectronicServiceApplicant);
            domainModel.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData = viewModel.ElectronicServiceApplicantContactData;

            if (viewModel.Declarations != null && viewModel.Declarations.Declarations != null && viewModel.Declarations.Declarations.Count > 0)
            {
                domainModel.Declarations = mapper.Map<List<Declaration>>(viewModel.Declarations.Declarations);
            }
        }

        public static void MapDocumentWithOfficialViewModelToDomain<TOfficial>(SigningDocumentFormVMBase<OfficialVM> viewModel, dynamic domainModel)
        {
            try
            {
                Type domainOfficialType = domainModel.GetType().GetProperty("Official").PropertyType;
                var interfaces = domainOfficialType.GetInterfaces();
                bool isOfficialPropertyIList = interfaces.Length > 0 && interfaces.Contains(typeof(System.Collections.IList));

                if (isOfficialPropertyIList)
                {
                    domainModel.Official = viewModel.DigitalSignatures?.Select(ds =>
                    {
                        dynamic official = Activator.CreateInstance(typeof(TOfficial));

                        if (typeof(TOfficial).GetProperty("Position") != null)
                        {
                            official.Position = ds.Position;
                        }

                        official.Item = ds.PersonNames;
                        official.ElectronicDocumentAuthorQuality = ds.ElectronicDocumentAuthorQuality;
                        official.SignatureUniqueID = ds.SignatureUniqueID;

                        return official;
                    }).ToList();
                }
                else
                {
                    domainModel.Official = viewModel.DigitalSignatures?.Select(ds =>
                    {
                        dynamic official = Activator.CreateInstance(typeof(TOfficial));

                        if (typeof(TOfficial).GetProperty("Position") != null)
                        {
                            official.Position = ds.Position;
                        }

                        official.Item = ds.PersonNames;

                        return official;
                    }).SingleOrDefault();
                }
            }
            catch
            { }
        }

        public static void MapDocumentWithOfficialDomainToViewModel<TOfficial>(dynamic domainModel, SigningDocumentFormVMBase<OfficialVM> viewModel)
        {
            try
            {
                Type domainOfficialType = domainModel.GetType().GetProperty("Official").PropertyType;
                var interfaces = domainOfficialType.GetInterfaces();
                bool isOfficialPropertyIList = interfaces.Length > 0 && interfaces.Contains(typeof(System.Collections.IList));

                if (isOfficialPropertyIList)
                {
                    if (domainModel.Official != null)
                    {
                        viewModel.DigitalSignatures = (domainModel.Official as List<dynamic>).Select(official => new OfficialVM()
                        {
                            Position = official.Position != null ? (string)official.Position : null,
                            PersonNames = (PersonNames)official.Item,
                            ElectronicDocumentAuthorQuality = official.ElectronicDocumentAuthorQuality,
                            SignatureUniqueID = official.SignatureUniqueID,
                            Choise = OfficialVM.OfficialChoiceType.PersonNames
                        }).ToList();
                    }
                }
                else
                {
                    if (domainModel.Official != null)
                    {
                        viewModel.DigitalSignatures = new List<OfficialVM>();

                        var officalVM = new OfficialVM();
                        officalVM.PersonNames = (PersonNames)domainModel.Official.Item;
                        officalVM.Choise = OfficialVM.OfficialChoiceType.PersonNames;

                        if (domainModel.Official.GetType().GetProperty("Position") != null)
                        {
                            officalVM.Position = (string)domainModel.Official.Position;
                        }

                        viewModel.DigitalSignatures.Add(officalVM);
                    }
                }
            }
            catch
            { }
        }

        public static void MapOfficialDomainToViewModel(dynamic domainModel, OfficialVM viewModel)
        {
            viewModel.Choise = domainModel.Item is PersonNames || domainModel.Item == null ? OfficialVM.OfficialChoiceType.PersonNames : OfficialVM.OfficialChoiceType.ForeignCitizenNames;
            viewModel.PersonNames = domainModel.Item is PersonNames ? (PersonNames)domainModel.Item : null;
            viewModel.ForeignCitizenNames = domainModel.Item is ForeignCitizenNames ? (ForeignCitizenNames)domainModel.Item : null;

            try
            {
                viewModel.HasAuthorQuality = true;
                viewModel.ElectronicDocumentAuthorQuality = domainModel.ElectronicDocumentAuthorQuality;
            }
            catch
            {
                viewModel.HasAuthorQuality = false;
            }

            try
            {
                viewModel.SignatureUniqueID = domainModel.SignatureUniqueID;
            }
            catch
            { }
        }

        public static void MapOfficialViewModelToDomain(OfficialVM viewModel, dynamic domainModel)
        {
            try
            {
                if (viewModel.HasAuthorQuality)
                    domainModel.ElectronicDocumentAuthorQuality = viewModel.ElectronicDocumentAuthorQuality;
            }
            catch
            { }

            try
            {
                domainModel.SignatureUniqueID = viewModel.SignatureUniqueID;
            }
            catch
            { }

            switch (viewModel.Choise)
            {
                case OfficialVM.OfficialChoiceType.PersonNames:
                    domainModel.Item = viewModel.PersonNames;
                    break;
                case OfficialVM.OfficialChoiceType.ForeignCitizenNames:
                    domainModel.Item = viewModel.ForeignCitizenNames;
                    break;
                default:
                    domainModel.Item = null;
                    break;
            }
        }

        #endregion
    }
}
