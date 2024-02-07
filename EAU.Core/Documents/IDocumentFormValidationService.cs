using CNSys;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Documents
{
    public interface IDocumentFormValidationService
    {
        Task<IErrorCollection> ValidateAsync(DocumentFormData form, CancellationToken cancellationToken);

        IErrorCollection ValidateByXSD(XmlDocument formXml, bool useWeakenedSchema);

        IErrorCollection ValidateDomainForm(object domainForm);
    }
}
