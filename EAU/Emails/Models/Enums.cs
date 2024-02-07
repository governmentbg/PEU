namespace EAU.Emails.Models
{
    /// <summary>
    /// Статус на имейл
    /// </summary>
    public enum EmailStatues : byte
    {
        /// <summary>
        /// Pending.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Send.
        /// </summary>
        Sent = 2,

        /// <summary>
        /// Failed.
        /// </summary>
        Failed = 3
    }   
}
