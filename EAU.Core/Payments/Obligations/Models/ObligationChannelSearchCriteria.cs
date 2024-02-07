namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Критерии за търсене на задължения.
    /// </summary>
    public class ObligationChannelSearchCriteria
    {
        /// <summary>
        /// Идентифкатор за задължение.
        /// </summary>
        public string ObligationIdentifier { get; set; }

        #region AND

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

        /// <summary>
        /// Система, в която да се търси
        /// </summary>
        public ANDSourceIds? ANDSourceId { get; set; }

        #endregion

        #region KAT_AND_A-X51

        /// <summary>
        /// Тип на документ при търсене на задължения към КАТ АНД А-Х51: 1 = Фиш; 2 = Наказателно постановление; 3 = Споразумение
        /// </summary>
        public KATDocumentTypes? DocumentType { get; set; }

        /// <summary>
        /// Серия на документ
        /// </summary>
        public string DocumentSeries { get; set; }

        /// <summary>
        /// Номер на документ
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Сума за плащане
        /// </summary>
        public double? InitialAmount { get; set; }

        #endregion

        #region Service Instance

        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        public long? ServiceInstanceID { get; set; }

        /// <summary>
        /// УРИ на указания за плащане във wais.
        /// </summary>
        public string PaymentInstructionURI { get; set; }

        #endregion
    }   
}