namespace EAU.COD.Documents.Domain
{
    public static class DocumentTypeUrisCOD
    {
        /// <summary>
        /// Искане за издаване на лиценз за извършване на частна охранителна дейност(чл. 15,ал. 1 от ЗЧОД)
        /// </summary>
        public const string RequestForIssuingLicenseForPrivateSecurityServices = "0010-003108";

        /// <summary>
        /// Уведомление за сключване или прекратяване на трудов договор между лице, получило лиценз за извършване на частна охранителна дейност, и служител от неговия специализиран персонал
        /// </summary>
        public const string NotificationForConcludingOrTerminatingEmploymentContract = "0010-003151";

        /// <summary>
        /// Уведомление за поемане или снемане от охрана на обект при извършване на частна охранителна дейност
        /// </summary>
        public const string NotificationForTakingOrRemovingFromSecurity = "0010-003152";
    }
}
