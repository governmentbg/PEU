
namespace EAU.Documents.Models
{
    public class PlaceOfBirthVM
    {
        #region Properties

        public int? DistrictGraoID
        {
            get;
            set;
        }
        public int? MunicipalityGRAOID
        {
            get;
            set;
        }
        public int? SettlementGRAOID
        {
            get;
            set;
        }

        //public string DistrictGRAOName
        //{
        //    get
        //    {
        //        return DistrictGraoID.HasValue ?
        //            NomenclaturesManager.GraoDistricts[DistrictGraoID.Value].Name :
        //            null;
        //    }
        //}
        //public string MunicipalityGRAOName
        //{
        //    get
        //    {
        //        return DistrictGraoID.HasValue && MunicipalityGRAOID.HasValue ?
        //            NomenclaturesManager.GraoDistricts[DistrictGraoID.Value].Municipalities
        //            .Single(m => m.GraoID == MunicipalityGRAOID.Value).Name :
        //            null;
        //    }
        //}
        //public string SettlementGRAOName
        //{
        //    get
        //    {
        //        return DistrictGraoID.HasValue && MunicipalityGRAOID.HasValue && SettlementGRAOID.HasValue ?
        //            NomenclaturesManager.GraoDistricts[DistrictGraoID.Value].Municipalities
        //            .Single(m => m.GraoID == MunicipalityGRAOID.Value).Settlements
        //            .Single(s => s.GraoID == SettlementGRAOID.Value).Name :
        //            null;
        //    }
        //}

        #endregion
    }
}
