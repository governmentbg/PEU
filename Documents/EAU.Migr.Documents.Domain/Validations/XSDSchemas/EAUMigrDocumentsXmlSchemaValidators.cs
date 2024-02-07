using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.Migr.Documents.Domain.Validations.XSDSchemas
{
    public class EAUMigrDocumentsXmlSchemaValidators : CachedXmlSchemaStoreValidator<EAUMigrDocumentsXmlSchemaValidators>
    {
        #region Constructors

        protected EAUMigrDocumentsXmlSchemaValidators()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUMigrDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUMigrDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUMigrDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUMigrDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUMigrDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}