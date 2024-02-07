
using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.KAT.Documents.Domain.Validations.XSDSchemas
{
    public class EAUKATDocumentsXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUKATDocumentsXmlSchemaValidator>
    {
        #region Constructors

        protected EAUKATDocumentsXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUKATDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUKATDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUKATDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUKATDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUKATDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}
