using EAU.Documents.XSLT;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace EAU.PBZN.Documents.XSLT
{
    public class PBZNEmbeddedXmlResourceResolver : XmlUrlResolver
    {
        public override object GetEntity(System.Uri absoluteUri,
            string role, Type ofObjectToReturn)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            object o = assembly.GetManifestResourceStream(this.GetType(),
                Path.GetFileName(absoluteUri.AbsolutePath));

            if (o == null)
                o = (new CommonEmbeddedXmlResourceResolver()).GetEntity(absoluteUri, role, ofObjectToReturn);

            return o;
        }
    }
}
