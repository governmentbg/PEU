namespace EAU.Signing.Configuration
{
    public class SignModuleGlobalOptions
    {
        /// <summary>
        /// Позволява тестово подписване на заявления и прикачени документи към тях (показва - 1/скрива - 0 бутона за тестово подписване) със сървърния сертификат, 
        /// който се ползва при криптиране на комуникацията с BISS.
        /// </summary>
        public int? SIGN_ALLOW_TEST_SIGN { get; set; }

        /// <summary>
        /// Тhumbprint на сървърен сертификат за криптиране на комуникацията между ПЕАУ и BISS - локална услуга за подписване в браузер.
        /// </summary>
        public string SIGN_BISS_CERT_THUMBPRINT { get; set; }

        /// <summary>
        /// Базов адрес за достъп до услугата BSecure DSSL на B-Trust.
        /// </summary>
        public string SIGN_BSECURE_DSSL_API { get; set; }

        #region BTrust

        /// <summary>
        /// Базов адрес за достъп до услугата за отдалечено подписване на B-Trust.
        /// </summary>
        public string SIGN_BTRUST_REMOTE_CLIENT_API { get; set; }

        /// <summary>
        /// Сертификат за ауторизация пред услугата за отдалечено подписване на B-Trust.
        /// </summary>
        public string SIGN_BTRUST_REMOTE_CLIENT_CERT { get; set; }

        /// <summary>
        /// Идентификатор на публичната част на ТР във системата на B-Trust.
        /// </summary>
        public string SIGN_BTRUST_REMOTE_RELYINGPARTY_ID { get; set; }

        #endregion

        #region Evrotrust

        /// <summary>
        /// Базов адрес, на който Evrotrust да извести за промяна в статуса на документ за подписване.
        /// </summary>
        public string SIGN_EVROTRUST_CALLBACK_BASE_URL { get; set; }

        /// <summary>
        /// Базов адрес за достъп до услугата за отдалечено подписване на Evrotrust.
        /// </summary>
        public string SIGN_EVROTRUST_CLIENT_API { get; set; }

        /// <summary>
        /// Ключ за ауторизация пред услугата за отдалечено подписване на Evrotrust.
        /// </summary>
        public string SIGN_EVROTRUST_VENDOR_API_KEY { get; set; }

        /// <summary>
        /// Номер на продавача (публичната част на ТР) във системата на Evrotrust.
        /// </summary>
        public string SIGN_EVROTRUST_VENDOR_NUM { get; set; }

        #endregion

        /// <summary>
        /// Конфигурира какво ниво на подпис да използва модула за подписване.Възможни стойности: BASELINE_B, BASELINE_T, BASELINE_LT и BASELINE_LTA.
        /// </summary>
        public string SIGN_SIGNATURE_LEVEL { get; set; }

        /// <summary>
        /// Флаг, указващ дали модула за подписване да валидира след полагане на подпис в документ. При 0 прави валидация, при > 0 не прави.
        /// </summary>
        public int? SIGN_SKIP_VALIDATION_AFTER_SIGN { get; set; }
    }
}
