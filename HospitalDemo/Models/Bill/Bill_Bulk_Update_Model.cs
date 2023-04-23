namespace HospitalDemo.Models.Bill
{
    public class Bill_Bulk_Update_Model
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_phone { get; set; }
        public int total_amount { get; set; }
        public string printed_or_drafted { get; set; }
        public string patient_address { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
