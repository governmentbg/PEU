using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.Documents.Models
{
    public class CitizenshipRegistrationBasicDataVM
    {
        public PersonBasicData PersonBasicData
        {
            get;
            set;
        }

        public string GenderCode
        {
            get;
            set;
        }

        public string GenderName
        {
            get;
            set;
        }

        public DateTime? BirthDate
        {
            get;
            set;
        }

        public PlaceOfBirthToggler PlaceOfBirthToggler
        {
            get;
            set;
        }

        public List<CitizenshipVM> Citizenships
        {
            get;
            set;
        }
    }
}
