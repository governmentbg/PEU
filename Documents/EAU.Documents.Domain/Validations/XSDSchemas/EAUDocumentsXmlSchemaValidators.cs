using CNSys.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Validations.XSDSchemas
{
    public interface IWeakenedXmlSchemaValidator : IXmlSchemaValidator
    {

    }

    public class EAUDocumentsXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUDocumentsXmlSchemaValidator>
    {
        #region Constructors

        protected EAUDocumentsXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUDocumentsSchemaStore(false);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }

    public class EAUDocumentsWeakenedXmlSchemaValidator : CachedXmlSchemaStoreValidator<EAUDocumentsWeakenedXmlSchemaValidator>, IWeakenedXmlSchemaValidator
    {
        #region Constructors

        protected EAUDocumentsWeakenedXmlSchemaValidator()
        {
        }

        #endregion

        #region Overriden Methods

        protected override IXmlSchemaStore CreateStore()
        {
            var store = new EAUDocumentsSchemaStore(true);

            store.InitStore(true);
            store.SchemaSet.Compile();

            return store;
        }

        #endregion
    }
}
