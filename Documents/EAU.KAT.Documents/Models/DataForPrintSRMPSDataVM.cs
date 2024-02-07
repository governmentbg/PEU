using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class DataForPrintSRMPSDataVM
    {
        public PersonEntityDataVM HolderData
        {
            get;
            set;
        }

        public PersonEntityDataVM UserData
        {
            get;
            set;
        }

        public List<PersonEntityDataVM> NewOwners
        {
            get;
            set;
        }

        public string SelectedNewOwner
        {
            get;
            set;
        }

        public bool CheckedHolderData
        {
            get;
            set;
        }

        public bool CheckedUserData
        {
            get;
            set;
        }

        public bool HolderNotSameAsUser
        {
            get;
            set;
        }

        public PoliceDepartment IssuingPoliceDepartment
        {
            get;
            set;
        }

        public List<PoliceDepartment> PossiblePoliceDepartments
        {
            get;
            set;
        }
    }
}
