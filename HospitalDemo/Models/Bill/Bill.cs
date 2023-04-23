namespace HospitalDemo.Models.Bill
{
    public enum printed_or_drafted_enum
    {

    }
    public class Bill
    {
        public int id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int patient_id { get; set; }
        public string patient_name { get; set; }
        public string patient_phone { get; set; }
        public int total_amount { get; set; }
        public int created_user_id { get; set; }
        public int updated_user_id { get; set; }
        public string printed_or_drafted { get; set; }
        public string patient_address { get; set; }
        public Boolean is_cancelled { get; set; }
    }
}
