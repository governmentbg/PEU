using EAU.Documents.Models.Forms;

namespace EAU.COD.Documents.Models.Forms
{
    /// <summary>
    /// Издаване на лиценз за извършване на частна охранителна дейност
    /// </summary>
    public class RequestForIssuingLicenseForPrivateSecurityServicesVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public RequestForIssuingLicenseForPrivateSecurityServicesDataVM Circumstances { get; set; }
    }
}