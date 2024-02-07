using CNSys.Xml.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EAU.Documents.Domain.Validations.XSDSchemas
{
    public abstract class EmbededResourceFileSchemaStore : FileXmlSchemaStore
    {
        private bool _getWeakenedSchemasChanges;

        #region Constructors

        public EmbededResourceFileSchemaStore(bool getWeakenedSchemasChanges)
            : base(new NullXmlSchemaResolver())
        {
            _getWeakenedSchemasChanges = getWeakenedSchemasChanges;
        }

        #endregion

        #region Overriden Methods

        protected override IEnumerable<string> GetXsdSchemaFileNames()
        {
            List<Tuple<string, string>> schemaNames = new List<Tuple<string, string>>();
            List<Tuple<string, string>> weakenedSchemasChanges = new List<Tuple<string, string>>();

            Regex schemaRegex = new Regex(SchemasRegex);
            Regex weakenedSchemaRegex = new Regex(WeakenedSchemasRegex);

            foreach (var item in GetType().Assembly.GetManifestResourceNames())
            {
                var match = schemaRegex.Matches(item);

                if (match.Count > 0)
                {
                    schemaNames.Add(new Tuple<string, string>(item, match[0].Groups[1].Value));
                }

                match = weakenedSchemaRegex.Matches(item);

                if (match.Count > 0)
                {
                    weakenedSchemasChanges.Add(new Tuple<string, string>(item, match[0].Groups[1].Value));
                }
            }

            //GET Schemas
            if (!_getWeakenedSchemasChanges)
                return (from item in schemaNames
                        select item.Item1);
            else
            {
                List<string> ret = new List<string>();
                foreach (var schemaName in schemaNames)
                {
                    var weakendItem = weakenedSchemasChanges.FindAll(t => string.Compare(t.Item2, schemaName.Item2) == 0).SingleOrDefault();

                    if (weakendItem == null)
                    {
                        ret.Add(schemaName.Item1);
                    }
                    else
                    {
                        ret.Add(weakendItem.Item1);
                    }
                }

                return ret;
            }
        }

        protected override Stream GetXsdSchema(string xsdSchemaFileName)
        {
            return GetType().Assembly.GetManifestResourceStream(xsdSchemaFileName);
        }

        #endregion

        protected abstract string SchemasRegex { get; }

        protected abstract string WeakenedSchemasRegex { get; }
    }
}
