using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.KOS.Documents.Domain.Validations.XSDSchemas
{
    public class EAUKOSDocumentsXmlSchemaValidators : CachedXmlSchemaStoreValidator<EAUKOSDocumentsXmlSchemaValidators>
    {
        #region Constructors

        protected EAUKOSDocumentsXmlSchemaValidators()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUKOSDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUKOSDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUKOSDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUKOSDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUKOSDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}