namespace PipilikaUI2.Models.Domain
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string Property_Name { get; set; } = default!;
        public string Property_ShortName { get; set; } = default!;
        public string Property_Code { get; set; } = default!;
        public int Property_OrderNo { get; set; }
        public int Property_NumberOfFloor { get; set; }
        public Property_TypeEnumId? Property_TypeEnumId { get; set; }
        public Property_StatusEnumId? Property_StatusEnumId { get; set; }
        public int Property_CampusId { get; set; }
        public int Property_Address { get; set; }
        public int Property_MapEmbedCode { get; set; }
        public int Property_Remarks { get; set; }



    }



    public enum Property_TypeEnumId
    {
        A = 1,
        B = 2,
        C = 3


    }


    public enum Property_StatusEnumId
    {
        Active =1,
        Inactive =2


    }





}
