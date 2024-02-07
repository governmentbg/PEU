using CNSys.Xml.Schema;
using System.Collections.Generic;

namespace EAU.Documents.Domain.Validations.XSDSchemas
{
    public class EAUDocumentsSchemaStore : CompositeXmlSchemaStoreBase
    {
        private bool _getWeakenedSchemasChanges;

        #region Constructors

        public EAUDocumentsSchemaStore(bool getWeakenedSchemasChanges)
        {
            _getWeakenedSchemasChanges = getWeakenedSchemasChanges;
        }

        #endregion

        protected override IEnumerable<IXmlSchemaStore> GetStores()
        {
            yield return new SchemaEmbededResourceFileSchemaStore(_getWeakenedSchemasChanges);
        }

        #region Internal Types

        /// <summary>
        /// Load all schemas from the dll. 
        /// Prohibit all external resources from the schemas. 
        /// </summary>
        private class SchemaEmbededResourceFileSchemaStore : EmbededResourceFileSchemaStore
        {
            #region Constructors

            public SchemaEmbededResourceFileSchemaStore(bool getWeakenedSchemasChanges)
                : base(getWeakenedSchemasChanges)
            {
            }

            #endregion

            protected override string SchemasRegex
            {
                get { return "EAU.Documents.Domain.Validations.XSDSchemas.Schemas.(.*).xsd"; }
            }

            protected override string WeakenedSchemasRegex
            {
                get { return "EAU.Documents.Domain.Validations.XSDSchemas.WeakenedSchemas.(.*).xsd"; }
            }
        }

        #endregion
    }
}
