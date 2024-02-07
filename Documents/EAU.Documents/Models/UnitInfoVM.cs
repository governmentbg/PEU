namespace EAU.Documents.Models
{
    public class UnitInfoVM
    {
        public bool HasChildUnits { get; set; }
        public string Name { get; set; }
        public UnitInfoVM ParentUnit { get; set; }
        public int ParentUnitInfoID { get; set; }
        public int unitInfoID { get; set; }
    }
}
