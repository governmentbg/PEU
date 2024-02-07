

namespace EAU.Documents.Models
{
    public class PaymentPortalChoiceVM
    {
        #region Properties

        public string CaseFileURI { get; set; }
        public string PaymentInstructionURI { get; set; }
        public short? SelectedPortalID { get; set; }
        public string ReturnURL { get; set; }
        public int? AdmServiceID { get; set; }

        #endregion

        //public static IEnumerable<PaymentPortal> GetPaymentPortals(int admSericeID)
        //{
        //    List<PaymentPortal> result = new List<PaymentPortal>();

        //    var admService = ESP.Core.BL.Cache.NomenclaturesManager.AdmServices[admSericeID];
        //    var admStruct = ESP.Core.BL.Cache.NomenclaturesManager.AdmStructures.Where(el => el.HistoryID == 0 && el.StructureId == admService.AdmStructureID).Single();

        //    //choose onli aktive portal fot this structure
        //    foreach (var portal in PaymentNomenclaturesManager.PaymentPortals)
        //    {
        //        var profile = PaymentNomenclaturesManager.PaymentPortalProfiles.GetProfileByAdmStructureID(portal.PaymentPortalId.Value, admStruct.StructureId.Value);

        //        if (profile != null && profile.IsActive)
        //            result.Add(portal);
        //    }

        //    return result;
        //}
    }
}
