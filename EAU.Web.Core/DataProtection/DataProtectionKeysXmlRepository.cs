using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace EAU.Web.Protection
{
    public class DataProtectionKeysXmlRepository : IXmlRepository
    {
        private readonly string CreationDateElName = "creationDate";
        private readonly string ActivationDateElName = "activationDate";
        private readonly string ExpirationDateElName = "expirationDate";
        private IDataProtectionRepository DataProtectionEntity { get; set; }

        public DataProtectionKeysXmlRepository(IDataProtectionRepository dataProtectionEntity)
        {
            DataProtectionEntity = dataProtectionEntity;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return DataProtectionEntity.Search(null).Select(k => XElement.Parse(k.KeyXml)).ToList().AsReadOnly();
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            DateTime? creationDate = null;
            DateTime? activationDate = null;
            DateTime? expirationDate = null;

            foreach (var descendant in element.Descendants())
            {
                if (string.Compare(descendant.Name.LocalName, CreationDateElName) == 0)
                {
                    creationDate = DateTime.Parse(descendant.Value);
                }
                else if (string.Compare(descendant.Name.LocalName, ActivationDateElName) == 0)
                {
                    activationDate = DateTime.Parse(descendant.Value);
                }
                else if (string.Compare(descendant.Name.LocalName, ExpirationDateElName) == 0)
                {
                    expirationDate = DateTime.Parse(descendant.Value);
                }
            }

            DataProtectionEntity.Create(new DataProtectionKey()
            {
                ID = element.Attribute(XName.Get("id")).Value,
                KeyXml = element.ToString(),
                CreationDate = creationDate.Value,
                ActivationDate = activationDate.Value,
                ExpirationDate = expirationDate.Value
            });
        }
    }
}
