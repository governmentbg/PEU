namespace EAU.Signing.Models
{
    /// <summary>
    /// Обекта капсулира данни за заявка за подписване на документ към приложението BISS.
    /// </summary>
    public class BissSignRequest
    {
        /// <summary>
        /// Съдържа версията, която се подържа от HTML приложението и се очаква от BISS.
        /// </summary>
        public string Version  {get; set;}

        /// <summary>
        /// Съдържа формата на подписване. По подразбиране стойността е “signature”, което означава, че е налице базово подписване. Поддържаните стойности са “signature”.
        /// </summary>
        public string SignatureType {get; set;}

        /// <summary>
        /// Съдържа типа на данните за подписване. По подразбиране има стойност “digest”. Подържаните стойности са “data” и “digest”. 
        /// Стойност “data“ роказва, че параметъра “contents” съдържа информацията, която ще бъде хеширана(с указаната стойност в параметъра hashAlgorithm) преди да се подпише.
        /// Стойност “digest” показва, че параметъра “contents” съдържа информация(например hash), която трябва да се подпише без да се хешира. 
        /// Ако стойността е “data” то signedContents не се валидира!
        /// </summary>
        public string ContentType {get; set;}

        /// <summary>
        /// Съдържа алгоритъма, който трябва да бъде използван за подписване. По подразбиране стойността е “SHA256”. 
        /// Подържаните стойности са “SHA1”, “SHA256” и “SHA512”. 
        /// Да се обърне внимание, че ако стойността на параметъра “contentType” е “digest” то параметъра трябва да съдържа хеш, който е направен с хеш алгоритъма посочен в полето “hashAlgorithm”.
        /// </summary>
        public string HashAlgorithm {get; set;}

        /// <summary>
        /// Съдържа base64 кодирани данни, които трябва да бъдат подписани.
        /// </summary>
        public string[] Contents {get; set;}

        /// <summary>
        /// Съдържа base64 кодирани данни, представляващи подписа на хеша на съответния елемент от атрибута contents.
        /// </summary>
        public string[] SignedContents {get; set;}

        /// <summary>
        /// Съдържа base64 кодиран сертификат, който е подписал съответният елемент от атрибута signedContents.
        /// </summary>
        public string[] SignedContentsCert {get; set;}

        /// <summary>
        /// Base64 кодиран избраният сертификат за подписване.
        /// </summary>
        public string SignerCertificateB64 {get; set;}

        /// <summary>
        /// Допълнителна информация в прозореца за потвърждение на хеша.
        /// </summary>
        public string AdditonalConfirmText {get; set;}

        /// <summary>
        /// Управлява дали да се извежда диалоговия прозорец за потвърждаване на подписване на съдържанието и какъв текст да визуализара в него.
        /// </summary>
        public string[] ConfirmText {get; set;}
    }

    /// <summary>
    /// Разширение на BissSignRequest, което включва допълнително поле с времето на създаване хеш на документа за подписване.
    /// Разширените данни са необходими на UI-a.
    /// </summary>
    public class BissSignRequestExtended
    {
        /// <summary>
        /// Заявка за подписване.
        /// </summary>
        public BissSignRequest SignRequest { get; set; }

        /// <summary>
        /// Времето на създаване хеш на документа за подписване.
        /// </summary>
        public long[] DocumentHashTime { get; set; }
    }
}
