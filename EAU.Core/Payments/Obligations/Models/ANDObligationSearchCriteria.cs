namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Критерии за търсене на задължения към АНД.
    /// </summary>
    public class ANDObligationSearchCriteria
    {
        /// <summary>
        /// Режим за търсене на задължения към АНД: 1 = По задължено лице; 2 = По задължение;
        /// </summary>
        public ANDObligationSearchMode? Mode { get; set; }
        
        /// <summary>
        /// Идентификатор на задължено лице.
        /// </summary>
        public string ObligedPersonIdent { get; set; }

        /// <summary>
        /// Идентификатор на свидетелство за управление на МПС.
        /// </summary>
        public string DrivingLicenceNumber { get; set; }

        /// <summary>
        /// Номер на личен картов документ.
        /// </summary>
        public string PersonalDocumentNumber { get; set; }

        /// <summary>
        /// Чуждестранен номер на МПС.
        /// </summary>
        public string ForeignVehicleNumber { get; set; }

        /// <summary>
        /// ЕИК/Булстат
        /// </summary>
        public string Uic { get; set; }

        #region KAT_AND_A-X51

        /// <summary>
        /// Тип на документ при търсене на задължения към КАТ АНД А-Х51: 1 = Фиш; 2 = Наказателно постановление; 3 = Споразумение
        /// </summary>
        public KATDocumentTypes? DocumentType { get; set; }

        /// <summary>
        /// Серия на документ.
        /// </summary>
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Номер на документ.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дължима сума.
        /// </summary>
        public double? Amount { get; set; }

        #endregion
    }

    /// <summary>
    /// Режим за търсене на задължения към АНД: 1 = По задължено лице; 2 = По задължение;
    /// </summary>
    public enum ANDObligationSearchMode
    {
        /// <summary>
        /// По задължено лице;
        /// </summary>
        ObligedPerson = 1,

        /// <summary>
        /// По задължение;
        /// </summary>
        Document = 2,
    }

}