using System;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Заявка за тестово подписване Biss.
    /// </summary>
    public class TestBissSignRequest
    {
        /// <summary>
        /// Потребителски сертификат в Base64.
        /// </summary>
        public string UserCertBase64 { get; set; }
    }

    /// <summary>
    /// Заявка за създаване на подписване с Biss.
    /// </summary>
    public class CreateBissSignRequest
    {
        /// <summary>
        /// Идентификатор на процес.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Потребителски сертификат в Base64.
        /// </summary>
        public string UserCertBase64 { get; set; }
    }

    /// <summary>
    /// Заявка за отдалечено подписване с BTrust.
    /// </summary>
    public class BtrustRemoteSignRequest
    {
        /// <summary>
        /// Идентификатор на процес.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public long? SignerID { get; set; }

        /// <summary>
        /// Данни за подадения потребителски идентификатор и вида му.
        /// </summary>
        public BtrustUserInputRequest UserData { get; set; }
    }

    /// <summary>
    /// Обект за завършване на отдалечено подписване с BTrust.
    /// </summary>
    public class BtrustCompleteRemoteSigning
    {
        /// <summary>
        /// Идентификатор на профил.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public long? SignerID { get; set; }
    }

    public class EvrotrustSignRequest
    {
        /// <summary>
        /// Идентификатор на профил.
        /// </summary>
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        public long? SignerID { get; set; }

        /// <summary>
        /// Типове идентификатори на потребител за Евротръст.
        /// 0 = Идентификатор.; 1 = Имейл.; 2 = Телефон.;
        /// </summary>
        public EvrotrustUserIdentTypes? IdentType { get; set; }

        /// <summary>
        /// Идентификатор на потребител.
        /// </summary>
        public string UserIdent { get; set; }
    }

    /// <summary>
    /// Типове идентификатори на потребител за Евротръст.
    /// 0 = Идентификатор.; 1 = Имейл.; 2 = Телефон.;
    /// </summary>
    public enum EvrotrustUserIdentTypes
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        Ident = 0,

        /// <summary>
        /// Имейл.
        /// </summary>
        Email = 1,

        /// <summary>
        /// Телефон.
        /// </summary>
        Phone = 2
    }
}
