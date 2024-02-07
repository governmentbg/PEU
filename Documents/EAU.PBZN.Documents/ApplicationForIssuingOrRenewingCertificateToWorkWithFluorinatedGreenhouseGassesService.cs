using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Nomenclatures;
using EAU.PBZN.Documents.Domain;
using EAU.PBZN.Documents.Domain.Models;
using EAU.PBZN.Documents.Domain.Models.Forms;
using EAU.PBZN.Documents.Models;
using EAU.PBZN.Documents.Models.Forms;
using EAU.PBZN.Documents.XSLT;
using EAU.Services.Nomenclatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;

namespace EAU.PBZN.Documents
{
    internal class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesService :
       ApplicationFormServiceBase<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisPBZN.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3107_ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses.xslt",
                    Resolver = new PBZNEmbeddedXmlResourceResolver()
                };
            }
        }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Personal, ElectronicServiceAuthorQualityType.LegalRepresentative };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person };
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);
            var servingUnitsInfoService = GetService<IServingUnitsInfo>();

            var app = (ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM)request.Form;

            if (app != null)
            {
                if (app.Circumstances == null)
                {
                    app.Circumstances = new Models.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM();

                    //Структура на МВР, предоставяща услугата
                    app.Circumstances.IssuingPoliceDepartment = new PoliceDepartment();
                    var issuingPoliceDepartment = service.AdditionalConfiguration["issuingPoliceDepartment"];
                    if (!string.IsNullOrWhiteSpace(issuingPoliceDepartment))
                    {
                        await servingUnitsInfoService.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);
                        var servingUnits = servingUnitsInfoService.Search(request.ServiceID.Value, out DateTime? lastModifiedDate);
                        var unitInfo = servingUnits.FirstOrDefault(s => s.UnitID == int.Parse(issuingPoliceDepartment));

                        if (unitInfo != null)
                        {
                            app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentCode = unitInfo.UnitID.ToString();
                            app.Circumstances.IssuingPoliceDepartment.PoliceDepartmentName = unitInfo.Name;
                        }
                    }

                    //Данни за получател на услугата 
                    var recipientGroup = app.ElectronicServiceApplicant.RecipientGroup;
                    app.Circumstances.PersonDataPermanentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.PermanentАddress });
                    app.Circumstances.PersonDataCurrentAddress = this.getPersonAdress(request.ApplicantInfo, new AddresType[] { AddresType.CurrentАddress });

                    if (recipientGroup != null)
                    {
                        //лично качество (за собствени нужди)
                        if (recipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal)
                        {
                            app.Circumstances.EntityOrPerson = EntityOrPerson.Person;

                            if (app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData == null)
                            {
                                app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData = new Models.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM();
                            }
                            app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData.CurrentAddress = app.Circumstances.PersonDataCurrentAddress;
                            app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData.PermanentAddress = app.Circumstances.PersonDataPermanentAddress;
                            app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData.CertificateType = CertificateType.Issuing;

                            app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData = null;
                        }
                        //в качеството на законен представител на юридическо лице
                        else if (recipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.LegalRepresentative)
                        {
                            app.Circumstances.EntityOrPerson = EntityOrPerson.Entity;
                            if (app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData == null)
                            {
                                app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData = new Models.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM();
                            }

                            //Правим проверката за да не се зачистват данните при подаване на коригиращо заявление. MVREAU2020-1100
                            if (app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData.AvailableCertifiedPersonnel == null
                                || app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData.AvailableCertifiedPersonnel.Count == 0)
                            {
                                app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData.AvailableCertifiedPersonnel = new List<CertifiedPersonelVM>();
                                app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData.AvailableCertifiedPersonnel.Add(new CertifiedPersonelVM());
                            }

                            app.Circumstances.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData = null;
                        }
                    }
                }
            }

            return result;
        }

        protected override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            return base.ValidateApplicationFormInternalAsync(request, cancellationToken, true);
        }

        public override string SignatureXpath
        {
            get
            {
                return "aipsgap:ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses/aipsgap:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"aipsgap", "http://ereg.egov.bg/segment/R-3107"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }

    }
}