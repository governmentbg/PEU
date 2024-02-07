using EAU.Documents.Models.Forms;

namespace EAU.KAT.Documents.Models.Forms.DocumentForms
{
    /// <summary>
    /// Заявяване на услуга за ИЗДАВАНЕ НА УДОСТОВЕРЕНИЯ ЗА ПРАВАТА И НАЛОЖЕНИТЕ НАКАЗАНИЯ НА ВОДАЧ НА МПС
    /// </summary>
    public class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM Circumstances { get; set; }        
    }
}
