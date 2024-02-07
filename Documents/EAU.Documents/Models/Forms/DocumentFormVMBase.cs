using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models.Forms
{
    public abstract class DocumentFormVMBase
    {
        #region Properties
        
        public List<string> EditStrategySelectedList
        {
            get;
            set;
        }

        public virtual DocumentTypeURI DocumentTypeURI
        {
            get;
            set;
        }

        public virtual string DocumentTypeName
        {
            get;
            set;
        }

        public virtual DocumentURI DocumentURI
        {
            get;
            set;
        }

        #endregion
    }
       
    public abstract class SigningDocumentFormVMBase<TSignatureConteiner> : DocumentFormVMBase
       where TSignatureConteiner : DigitalSignatureContainerVM
    {
        public List<TSignatureConteiner> DigitalSignatures
        {
            get;
            set;
        }
    }
}
