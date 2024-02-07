namespace EAU.Emails.Models
{
    /// <summary>
    /// Получател на имейл
    /// </summary>
    public class EmailRecipient
    {
        /// <summary>
        /// Адрес на електронна поща.
        /// </summary>      
        public string Address { get; set; }

        /// <summary>
        /// Име на получател.
        /// </summary>     
        public string DisplayName { get; set; }

        /// <summary>
        /// Тип на адрес. To - 1, Cc - 2, Bcc - 3
        /// </summary>     
        public AddressTypes? Type { get; set; }
    }

    /// <summary>
    /// Вид адрес
    /// </summary>
    public enum AddressTypes : byte
    {
        /// <summary>
        /// To.
        /// </summary>
        To = 1,

        /// <summary>
        /// Cc.
        /// </summary>
        Cc = 2,

        /// <summary>
        /// Bcc.
        /// </summary>
        Bcc = 3
    }
}
