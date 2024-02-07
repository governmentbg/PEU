using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;

namespace EAU.COD.Documents.Domain.Validations.XSDSchemas
{
    public class EAUCODDocumentsXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUCODDocumentsXmlSchemaValidator>
    {
        #region Constructors

        protected EAUCODDocumentsXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUCODDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUCODDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUCODDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUCODDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUCODDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}
