using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace EAU.Documents.XSLT
{
    public class CommonEmbeddedXmlResourceResolver : XmlUrlResolver
    {
        public override object GetEntity(System.Uri absoluteUri,
          string role, Type ofObjectToReturn)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            object o = assembly.GetManifestResourceStream(this.GetType(),
                Path.GetFileName(absoluteUri.AbsolutePath));

            return o;
        }
    }
}
