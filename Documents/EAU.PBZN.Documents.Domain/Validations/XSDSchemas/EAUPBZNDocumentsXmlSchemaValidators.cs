using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.PBZN.Documents.Domain.Validations.XSDSchemas
{
    public class EAUPBZNDocumentsXmlSchemaValidators : CachedXmlSchemaStoreValidator<EAUPBZNDocumentsXmlSchemaValidators>
    {
        #region Constructors

        protected EAUPBZNDocumentsXmlSchemaValidators()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUPBZNDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUPBZNDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUPBZNDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUPBZNDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUPBZNDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}