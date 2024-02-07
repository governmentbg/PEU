using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models
{
    public class EkatteAddress
    {       
        public string DistrictCode { get; set; }
                
        public string DistrictName { get; set; }

        public string MunicipalityCode { get; set; }
              
        public string MunicipalityName { get; set; }
               
        public string SettlementCode { get; set; }

        public string SettlementName { get; set; }

        public string AreaCode { get; set; }

        public string AreaName { get; set; }

        public string PostCode { get; set; }
        
        /// <summary>
        /// Квартал.
        /// </summary>
        public string HousingEstate { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Номер на улица.
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// Блок.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Вход.
        /// </summary>
        public string Entrance { get; set; }

        /// <summary>
        /// Етаж.
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// Апартамент.
        /// </summary>
        public string Apartment { get; set; }
    }
}
