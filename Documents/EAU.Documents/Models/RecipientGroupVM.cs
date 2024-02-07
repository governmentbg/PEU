using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models
{   
    public class RecipientGroupVM 
    {
        public AuthorWithQualityVM AuthorWithQuality
        {
            get;
            set;
        }

        /// <summary>
        /// Получател на електронната административна услуга.
        /// </summary>  
        public ElectronicServiceRecipientVM Recipient
        {
            get;
            set;
        }
    }

    public class AuthorWithQualityVM
    {
        #region Constructor

        public AuthorWithQualityVM()
        {
        }

        #endregion

        #region Properties

        public ElectronicServiceAuthorQualityType SelectedAuthorQuality
        {
            get;
            set;
        }

        /// <summary>
        /// Автор на електронно изявление.
        /// </summary>  
        public ElectronicStatementAuthorVM Author
        {
            get;
            set;
        }

        #endregion
    }
}
