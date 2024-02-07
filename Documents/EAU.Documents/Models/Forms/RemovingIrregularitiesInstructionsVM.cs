using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    public class RemovingIrregularitiesInstructionsVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData 
        { 
            get; 
            set; 
        }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant 
        { 
            get; 
            set; 
        }

        public DocumentURI IrregularityDocumentURI 
        { 
            get; 
            set; 
        }

        public string RemovingIrregularitiesInstructionsHeader 
        { 
            get; 
            set; 
        }

        public DocumentURI ApplicationDocumentURI 
        { 
            get; 
            set; 
        }

        public DateTime? ApplicationDocumentReceiptOrSigningDate
        {
            get;
            set;
        }

        public DateTime? IrregularityDocumentReceiptOrSigningDate
        {
            get;
            set;
        }

        public AISCaseURIVM AISCaseURI 
        { 
            get; 
            set; 
        }
        
        public List<RemovingIrregularitiesInstructionsIrregularitiesVM> Irregularities 
        { 
            get;
            set; 
        }

        public DeadlineVM DeadlineCorrectionIrregularities 
        { 
            get; 
            set; 
        }

        public string AdministrativeBodyName 
        { 
            get; 
            set; 
        }
        
        //TODO
        //public List<SelectListItem> IrregularityTypeNomenclatures { get; set; }          
    }
}
