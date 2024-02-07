using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class ElectronicAdministrativeServiceHeaderVM
    {
        public ApplicationType ApplicationType
        {
            get;
            set;
        }

        public string DocumentTypeName
        {
            get;
            set;
        }

        public DocumentTypeURI DocumentTypeURI
        {
            get;
            set;
        }

        public DocumentURI DocumentURI
        {
            get;
            set;
        }

        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
        {
            get;
            set;
        }
         
        public string SUNAUServiceName
        {
            get;
            set;
        }

        public string SUNAUServiceURI
        {
            get;
            set;
        }

        public string AdmStructureUnitName
        {
            get;
            set;
        }
    }
}
