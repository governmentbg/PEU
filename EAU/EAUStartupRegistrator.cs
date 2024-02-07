using Dapper;
using EAU.Audit.Models;
using EAU.CMS.Models;
using EAU.Common.Models;
using EAU.Emails.Models;
using EAU.Nomenclatures.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Reports.NotaryInterestsForPersonOrVehicle.Models;
using EAU.Reports.PaymentsObligations.Models;
using EAU.ServiceLimits.Models;
using EAU.Users.Models;
using EAU.Utilities;

namespace EAU
{
    public static class EAUStartupBootstrapper
    {
        public static void Run()
        {
            EAUStartupRegistrator.Current.Register();
        }
    }

    public class EAUStartupRegistrator : StartupRegistrator<EAUStartupRegistrator>
    {
        protected override void RegisterInternal()
        {
            SqlMapper.AddTypeHandler<AdditionalData>(new AdditionalDataDapperMapHandler());
            SqlMapper.AddTypeHandler(new DateTimeOffsetTypeHandler());

            SqlMapper.SetTypeMap(typeof(EmailMessage), DataContextHelper.ColumnMap<EmailMessage>());
            SqlMapper.SetTypeMap(typeof(EmailTemplate), DataContextHelper.ColumnMap<EmailTemplate>());

            SqlMapper.SetTypeMap(typeof(LogAction), DataContextHelper.ColumnMap<LogAction>());
            SqlMapper.SetTypeMap(typeof(ActionType), DataContextHelper.ColumnMap<ActionType>());
            SqlMapper.SetTypeMap(typeof(ObjectType), DataContextHelper.ColumnMap<ObjectType>());

            SqlMapper.SetTypeMap(typeof(AppParameter), DataContextHelper.ColumnMap<AppParameter>());
            SqlMapper.SetTypeMap(typeof(Functionality), DataContextHelper.ColumnMap<Functionality>()); 

            SqlMapper.SetTypeMap(typeof(DataServiceLimit), DataContextHelper.ColumnMap<DataServiceLimit>());
            SqlMapper.SetTypeMap(typeof(DataServiceUserLimit), DataContextHelper.ColumnMap<DataServiceUserLimit>());

            SqlMapper.SetTypeMap(typeof(Page), DataContextHelper.ColumnMap<Page>());
            SqlMapper.SetTypeMap(typeof(RegistrationData), DataContextHelper.ColumnMap<RegistrationData>());

            SqlMapper.SetTypeMap(typeof(User), DataContextHelper.ColumnMap<User>());
            SqlMapper.SetTypeMap(typeof(UserAuthentication), DataContextHelper.ColumnMap<UserAuthentication>());
            SqlMapper.SetTypeMap(typeof(UserLoginAttempt), DataContextHelper.ColumnMap<UserLoginAttempt>());
            SqlMapper.SetTypeMap(typeof(UserLoginSession), DataContextHelper.ColumnMap<UserLoginSession>());
            SqlMapper.SetTypeMap(typeof(Certificate), DataContextHelper.ColumnMap<Certificate>());
            SqlMapper.SetTypeMap(typeof(UserProcess), DataContextHelper.ColumnMap<UserProcess>());
            SqlMapper.SetTypeMap(typeof(UserPermission), DataContextHelper.ColumnMap<UserPermission>());

            #region reports 

            SqlMapper.SetTypeMap(typeof(PaymentsObligationsData), DataContextHelper.ColumnMap<PaymentsObligationsData>());
            SqlMapper.SetTypeMap(typeof(DocumentAccessedDataGroupedRow), DataContextHelper.ColumnMap<DocumentAccessedDataGroupedRow>());

            #endregion

            #region nomenclatures

            SqlMapper.SetTypeMap(typeof(Language), DataContextHelper.ColumnMap<Language>());
            SqlMapper.SetTypeMap(typeof(Label), DataContextHelper.ColumnMap<Label>());
            SqlMapper.SetTypeMap(typeof(Declaration), DataContextHelper.ColumnMap<Declaration>());
            SqlMapper.SetTypeMap(typeof(DeliveryChannel), DataContextHelper.ColumnMap<DeliveryChannel>());
            SqlMapper.SetTypeMap(typeof(DocumentType), DataContextHelper.ColumnMap<DocumentType>());
            SqlMapper.SetTypeMap(typeof(Service), DataContextHelper.ColumnMap<Service>());
            SqlMapper.SetTypeMap(typeof(ServiceGroup), DataContextHelper.ColumnMap<ServiceGroup>());
            SqlMapper.SetTypeMap(typeof(ServiceTerm), DataContextHelper.ColumnMap<ServiceTerm>());
            SqlMapper.SetTypeMap(typeof(Ekatte), DataContextHelper.ColumnMap<Ekatte>());
            SqlMapper.SetTypeMap(typeof(Grao), DataContextHelper.ColumnMap<Grao>());
            SqlMapper.SetTypeMap(typeof(ServiceDeclaration), DataContextHelper.ColumnMap<ServiceDeclaration>());
            SqlMapper.SetTypeMap(typeof(ServiceDeliveryChannel), DataContextHelper.ColumnMap<ServiceDeliveryChannel>());
            SqlMapper.SetTypeMap(typeof(ServiceDocumentType), DataContextHelper.ColumnMap<ServiceDocumentType>());
            SqlMapper.SetTypeMap(typeof(DocumentTemplate), DataContextHelper.ColumnMap<DocumentTemplate>());
            SqlMapper.SetTypeMap(typeof(DocumentTemplateField), DataContextHelper.ColumnMap<DocumentTemplateField>());
            SqlMapper.SetTypeMap(typeof(Country), DataContextHelper.ColumnMap<Country>());

            #endregion
        }
    }
}