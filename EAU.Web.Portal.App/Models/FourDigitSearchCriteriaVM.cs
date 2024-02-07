using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KAT.SPRKRTCO.Models;

namespace EAU.Web.Portal.App.Models
{
    public class FourDigitSearchCriteriaVM
    {
        /// <summary>
        /// Код на структурно звено „Пътна полиция“ (ПП) към ОДМВР/СДВР.
        /// </summary>        
        public int? PoliceDepartment { get; set; }

        /// <summary>
        /// Вид МПС
        /// </summary>
        public VehicleTypeCode? VehicleTypeCode { get; set; }

        /// <summary>
        /// Брой табели Тип1.
        /// </summary>
        public int? Type1PlatesCount { get; set; }

        /// <summary>
        /// Брой табели Тип2.
        /// </summary>
        public int? Type2PlatesCount { get; set; }

        /// <summary>
        /// Начин на търсене.
        /// </summary>
        public FourDigitSearchTypes FourDigitSearchType { get; set; }

        /// <summary>
        /// Формат на регистрационен номер.
        /// </summary>
        public NumberFormat? NumberFormat { get; set; }

        /// <summary>
        /// От регистрационнен номер.
        /// </summary>
        public string FromRegNumber { get; set; }

        /// <summary>
        /// До регистрационнен номер.
        /// </summary>
        public string ToRegNumber { get; set; }

        /// <summary>
        /// Конкретен регистрационнен номер.
        /// </summary>
        public string SpecificRegNumber { get; set; }

        /// <summary>
        /// Статус на табела.
        /// </summary>        
        public PlateStatus? PlateStatus { get; set; }
    }

    public enum FourDigitSearchTypes 
    {
        ByRegNumber = 0,
        ByInterval = 1
    }
}
