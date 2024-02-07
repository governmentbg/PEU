namespace EAU.Emails.Models
{
    /// <summary>
    /// Приоритет на имейл: 1 = Нисък приоритет/The email has low priority.; 2 = Нормален приоритет/The email has normal priority.; 3 = Висок приоритет/The email has high priority.
    /// </summary>
    public enum EmailPriority
    {
        /// <summary>
        /// The email has low priority.
        /// </summary>
        Low = 1,
        /// <summary>
        /// The email has normal priority.
        /// </summary>
        Normal = 2,
        /// <summary>
        /// The email has high priority.
        /// </summary>
        High = 3
    }
}
