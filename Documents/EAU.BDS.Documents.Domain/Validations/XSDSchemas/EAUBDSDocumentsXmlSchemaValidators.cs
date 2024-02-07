using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.BDS.Documents.Domain.Validations.XSDSchemas
{
    public class EAUBDSDocumentsXmlSchemaValidators : CachedXmlSchemaStoreValidator<EAUBDSDocumentsXmlSchemaValidators>
    {
        #region Constructors

        protected EAUBDSDocumentsXmlSchemaValidators()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUBDSDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUBDSDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUBDSDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUBDSDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUBDSDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}