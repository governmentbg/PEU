namespace EAU.Payments.RegistrationsData.Models
{
    /// <summary>
    /// Типове на регистрационните данни: 1 = ePay; 2 = ПЕП на ДАЕУ, 
    /// </summary>
    public enum RegistrationDataTypes
    {
        /// <summary>
        /// ePay.
        /// </summary>
        ePay = 1,

        /// <summary>
        /// ПЕП на ДАЕУ.
        /// </summary>
        PepOfDaeu = 2,
    }
}
